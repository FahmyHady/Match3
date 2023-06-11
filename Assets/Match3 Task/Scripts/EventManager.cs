using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public struct Events
{
    public enum Gameplay
    {

    }
    public enum TimeScale
    {

    }
    public enum UI
    {

    }
    public enum GameState
    {

    }
    public enum MainMenu
    {

    }
    public enum General
    {
        GenerateNewGrid
    }
}

public class EventManager : MonoBehaviour
{


    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene. One Has been added");
                    //  eventManager = new GameObject("Event Manager").AddComponent<EventManager>();
                }

            }

            return eventManager;
        }
    }




    private Dictionary<Type, Dictionary<object, UnityEventBase>> events = new Dictionary<Type, Dictionary<object, UnityEventBase>>();

    private T GetEventListener<T>(object eventId) where T : UnityEventBase, new()
    {
        Type eventType = typeof(T);
        Dictionary<object, UnityEventBase> eventDict = null;

        if (events.ContainsKey(eventType))
        {
            eventDict = events[eventType];
        }
        else
        {
            eventDict = new Dictionary<object, UnityEventBase>();
            events[eventType] = eventDict;
        }

        UnityEventBase evt = null;
        if (eventDict.ContainsKey(eventId))
        {
            evt = eventDict[eventId];
        }
        else
        {
            evt = new T();
            eventDict[eventId] = evt;
        }

        return (T)evt;
    }

    public static void AddListener(object eventId, UnityAction action)
    {
        var listener = instance.GetEventListener<UnityEvent>(eventId);
        listener.AddListener(action);
    }

    public static void AddListener<T>(object eventId, UnityAction<T> action)
    {
        var listener = instance.GetEventListener<UnityEvent<T>>(eventId);
        listener.AddListener(action);
    }

    public static void AddListener<T0, T1>(object eventId, UnityAction<T0, T1> action)
    {
        var listener = instance.GetEventListener<UnityEvent<T0, T1>>(eventId);
        listener.AddListener(action);
    }

    public static void AddListener<T0, T1, T2>(object eventId, UnityAction<T0, T1, T2> action)
    {
        var listener = instance.GetEventListener<UnityEvent<T0, T1, T2>>(eventId);
        listener.AddListener(action);
    }

    public static void AddListener<T0, T1, T2, T3>(object eventId, UnityAction<T0, T1, T2, T3> action)
    {
        var listener = instance.GetEventListener<UnityEvent<T0, T1, T2, T3>>(eventId);
        listener.AddListener(action);
    }

    public static void Trigger(object eventId)
    {
        var listener = instance.GetEventListener<UnityEvent>(eventId);
        listener.Invoke();
        //   JustDice.Core.Log.Debug($"event triggered - `{eventId}");
    }

    public static void Trigger<T0>(object eventId, T0 t0)
    {
        var listener = instance.GetEventListener<UnityEvent<T0>>(eventId);
        listener.Invoke(t0);
        //  JustDice.Core.Log.Debug($"event triggered - `{eventId}`");
    }

    public static void Trigger<T0, T1>(object eventId, T0 t0, T1 t1)
    {
        var listener = instance.GetEventListener<UnityEvent<T0, T1>>(eventId);
        listener.Invoke(t0, t1);
        //   JustDice.Core.Log.Debug($"event triggered - `{eventId}`");
    }

    public static void Trigger<T0, T1, T2>(object eventId, T0 t0, T1 t1, T2 t2)
    {
        var listener = instance.GetEventListener<UnityEvent<T0, T1, T2>>(eventId);
        listener.Invoke(t0, t1, t2);
        //      JustDice.Core.Log.Debug($"event triggered - `{eventId}`");
    }

    public static void Trigger<T0, T1, T2, T3>(object eventId, T0 t0, T1 t1, T2 t2, T3 t3)
    {
        var listener = instance.GetEventListener<UnityEvent<T0, T1, T2, T3>>(eventId);
        listener.Invoke(t0, t1, t2, t3);
        //    JustDice.Core.Log.Debug($"event triggered - `{eventId}`");
    }

    public static void RemoveListener(object eventId, UnityAction action)
    {
        if (eventManager == null) return;
        var listener = instance.GetEventListener<UnityEvent>(eventId);
        listener.RemoveListener(action);
    }

    public static void RemoveListener<T>(object eventId, UnityAction<T> action)
    {
        if (eventManager == null) return;
        var listener = instance.GetEventListener<UnityEvent<T>>(eventId);
        listener.RemoveListener(action);
    }

    public static void RemoveListener<T0, T1>(object eventId, UnityAction<T0, T1> action)
    {
        if (eventManager == null) return;
        var listener = instance.GetEventListener<UnityEvent<T0, T1>>(eventId);
        listener.RemoveListener(action);
    }

    public static void RemoveListener<T0, T1, T2>(object eventId, UnityAction<T0, T1, T2> action)
    {
        if (eventManager == null) return;
        var listener = instance.GetEventListener<UnityEvent<T0, T1, T2>>(eventId);
        listener.RemoveListener(action);
    }

    public static void RemoveListener<T0, T1, T2, T3>(object eventId, UnityAction<T0, T1, T2, T3> action)
    {
        if (eventManager == null) return;
        var listener = instance.GetEventListener<UnityEvent<T0, T1, T2, T3>>(eventId);
        listener.RemoveListener(action);
    }
}