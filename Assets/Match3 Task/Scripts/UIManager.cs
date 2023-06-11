using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button generateGrid;
    [SerializeField] Slider gridSizeX;
    [SerializeField] Slider gridSizeY;
    [SerializeField] TextMeshProUGUI sizeX;
    [SerializeField] TextMeshProUGUI sizeY;
    private void Awake()
    {
        gridSizeX.onValueChanged.AddListener((a)=>sizeX.text=$"Size X: {a}");
        gridSizeY.onValueChanged.AddListener((a)=>sizeY.text=$"Size Y: {a}");
        generateGrid.onClick.AddListener(() => EventManager.Trigger(Events.General.GenerateNewGrid,(int) gridSizeX.value, (int)gridSizeY.value));
    }
}