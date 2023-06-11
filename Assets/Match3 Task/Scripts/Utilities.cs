using System;
using System.Collections;
using UnityEngine;

public static class Utilities
{



    public static Coroutine LerpFloatValue(this MonoBehaviour toRunOn, Action<float> actionOnValueChange, float from,
    float to, float duration = 1, bool scaled = true, Action onDone = null)
    {
        if (toRunOn == null || toRunOn.gameObject == null) return null;
        if (!toRunOn.gameObject.activeInHierarchy)
        {
            var gameObject = new GameObject("Lerping Float Value");
            toRunOn = gameObject.AddComponent<EmptyMonoBehaviour>();
            return toRunOn.StartCoroutine(LerpFloatValueRoutine(actionOnValueChange, from, to, duration, scaled, () =>
            {
                onDone?.Invoke();
                UnityEngine.Object.Destroy(toRunOn.gameObject);
            }));
        }
        else
        {
            return toRunOn.StartCoroutine(
                LerpFloatValueRoutine(actionOnValueChange, from, to, duration, scaled, onDone));
        }
    }

    private static IEnumerator LerpFloatValueRoutine(Action<float> actionOnValueChange, float from, float to,
        float duration = 1, bool scaled = true, Action onDone = null)
    {
        float elapsed = 0;
        float temp = from;
        while (elapsed < duration)
        {
            if (scaled)
                elapsed += Time.deltaTime;
            else
                elapsed += Time.unscaledDeltaTime;
            temp = Mathf.Lerp(from, to, elapsed / duration);
            actionOnValueChange?.Invoke(temp);
            yield return null;
        }

        actionOnValueChange?.Invoke(to);
        onDone?.Invoke();
    }
    public static Coroutine MoveFromToOverTime(MonoBehaviour toRunOn, Transform objectToMove, Vector3 from, Vector3 to, float duration)
    {
        return toRunOn.StartCoroutine(MoveFromToOverTimeRoutine(objectToMove, from, to, duration));
    }

    public static Coroutine MoveFromToOverTimeOverCurve(MonoBehaviour toRunOn, Transform objectToMove, Vector3 from, Vector3 to, float duration, AnimationCurve curve, float percentageOfDuration = 1)
    {
        return toRunOn.StartCoroutine(MoveFromToOverTimeOverCurveRoutine(objectToMove, from, to, duration, curve, percentageOfDuration));
    }
    public static Coroutine MoveFromToOverTimeOverCurveUsingPhysics(MonoBehaviour toRunOn, Rigidbody objectToMove, Vector3 from, Vector3 to, float duration, AnimationCurve curve, float percentageOfDuration = 1)
    {
        return toRunOn.StartCoroutine(MoveFromToOverTimeOverCurveUsingPhysicsRoutine(objectToMove, from, to, duration, curve, percentageOfDuration));
    }

    public static Coroutine RotateFromToOverTime(MonoBehaviour toRunOn, Transform objectToMove, Quaternion from, Quaternion to, float duration)
    {
        return toRunOn.StartCoroutine(RotateFromToOverTimeRoutine(objectToMove, from, to, duration));
    }

    static IEnumerator MoveFromToOverTimeRoutine(Transform objectToMove, Vector3 from, Vector3 to, float duration)
    {
        float e = 0;
        while (e < duration)
        {
            e += Time.deltaTime;
            objectToMove.position = Vector3.Lerp(from, to, e / duration);
            yield return null;
        }

        objectToMove.position = to;
    }

    static IEnumerator MoveFromToOverTimeOverCurveRoutine(Transform objectToMove, Vector3 from, Vector3 to, float duration, AnimationCurve curve, float percentageOfDuration = 1, bool assertLocationAtEnd = false)
    {
        float e = 0;
        while (e < duration * percentageOfDuration)
        {
            e += Time.deltaTime;
            objectToMove.position = Vector3.LerpUnclamped(from, to, curve.Evaluate(e / duration));
            yield return null;
        }

        if (assertLocationAtEnd && e > duration)
            objectToMove.position = to;
    }
    static IEnumerator MoveFromToOverTimeOverCurveUsingPhysicsRoutine(Rigidbody objectToMove, Vector3 from, Vector3 to, float duration, AnimationCurve curve, float percentageOfDuration = 1, bool assertLocationAtEnd = false)
    {
        float e = 0;
        while (e < duration * percentageOfDuration)
        {
            e += Time.fixedDeltaTime;
            objectToMove.MovePosition(Vector3.LerpUnclamped(from, to, curve.Evaluate(e / duration)));
            yield return new WaitForFixedUpdate();
        }

        if (assertLocationAtEnd && e > duration)
            objectToMove.position = to;
    }

    static IEnumerator RotateFromToOverTimeRoutine(Transform objectToMove, Quaternion from, Quaternion to, float duration)
    {
        float e = 0;
        while (e < duration)
        {
            e += Time.deltaTime;
            objectToMove.rotation = Quaternion.Lerp(from, to, e / duration);
            yield return null;
        }

        objectToMove.rotation = to;
    }

    public static void DoAfterDelay(Action toDo, float delay, MonoBehaviour toRunOn = null)
    {
        if (!toRunOn)
        {
            toRunOn = new GameObject("Coroutine Holder").AddComponent<EmptyMonoBehaviour>();
            toDo += () => GameObject.Destroy(toRunOn.gameObject);
        }

        toRunOn.StartCoroutine(DoAfterTimeRoutine(toDo, delay));
    }

    static IEnumerator DoAfterTimeRoutine(Action toDo, float delay)
    {
        yield return new WaitForSeconds(delay);
        toDo?.Invoke();
    }
}
public class EmptyMonoBehaviour : MonoBehaviour
{
}
