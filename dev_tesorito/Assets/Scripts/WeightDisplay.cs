using UnityEngine;
using TMPro;

public class WeightDisplay : MonoBehaviour
{
    // Drag the Weight Value TMP text object in here
    [Header("Weight Text")]
    [SerializeField] 
    private TextMeshProUGUI weightText;

    // The two colors the text switches between
    [Header("Colors")]
    [SerializeField] 
    private Color normalColor = Color.white;
    [SerializeField] 
    private Color overweightColor = Color.red;

    private void Start()
    {
        // Subscribe to equipment changes so we update whenever
        // anything is equipped/unequipped
        EquipmentManager.Instance.OnEquipmentChanged += Refresh;

        // Run once at start so the display is correct immediately
        Refresh();
    }

    private void OnDestroy()
    {
        if (EquipmentManager.Instance != null) {
            EquipmentManager.Instance.OnEquipmentChanged -= Refresh;
        }
    }

    private void Refresh()
    {
        // Get the current total weight from EquipmentManager
        float currentWeight = EquipmentManager.Instance.totalWeight;
        float maxWeight = EquipmentManager.Instance.maxWeight;

        // Update the text to show current weight
        weightText.text = currentWeight.ToString();

        // Check if we are over the limit and change color accordingly
        if (currentWeight > maxWeight) {
            weightText.color = overweightColor;
        }
        else {
            weightText.color = normalColor;
        }
    }
}