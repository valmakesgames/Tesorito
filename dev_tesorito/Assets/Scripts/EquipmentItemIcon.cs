using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemIcon : MonoBehaviour
{
    [Header("Item Data")]
    public EquipmentItem itemData;
    public EquipmentSlot slot;

    [Header("Selection States")]
    [SerializeField] 
    private Image selectionRing;
    
    [SerializeField] 
    private Color selectedColor = Color.white;
    
    [SerializeField] 
    private Color equippedColor = Color.yellow;

    [Header("Panel Reference")]
    [SerializeField] 
    private EquipmentItemPanelDisplay itemPanel;

    // Tracks whether this icon is currently selected
    private bool isSelected = false;

    private void Start()
    {
        // Ring starts hidden
        SetRingVisible(false);
        
        // Subscribe to Equipment Manager's OnEquipmentChanged event
        EquipmentManager.Instance.OnEquipmentChanged += RefreshVisualState;
    }

    private void OnDestroy() {
        // Unsubscribe when object is destroyed
        if(EquipmentManager.Instance != null) {
            EquipmentManager.Instance.OnEquipmentChanged -= RefreshVisualState; 
        }
    }
    
    // Called by the Button component when this icon is clicked
    public void OnIconClicked()
    {
        if (isSelected) {
            // Already selected — clicking again deselects and hides the panel
            Deselect();
            itemPanel.Hide();
        }
        else {
            // Not selected — select it and show the panel with this item's data
            Select();
            itemPanel.Populate(itemData, slot);
            itemPanel.Show();
        }
    }

    // Shows the ring in selected color
    public void Select()
    {
        isSelected = true;
        SetRingVisible(true);
        SetRingColor(selectedColor);
    }

    // Hides or changes the ring when deselected
    public void Deselect()
    {
        isSelected = false;

        // If this item is still equipped, keep showing the ring in equipped color
        // If not equipped, hide the ring entirely
        if (EquipmentManager.Instance.IsItemEquipped(slot, itemData)) {
            SetRingVisible(true);
            SetRingColor(equippedColor);
        }
        else {
            SetRingVisible(false);
        }
    }

    // Called whenever equipment changes — refreshes this icon's ring visual
    public void RefreshVisualState()
    {
        if (EquipmentManager.Instance.IsItemEquipped(slot, itemData)) {
            SetRingVisible(true);
            SetRingColor(equippedColor);
        }
        else if (isSelected) {
            SetRingVisible(true);
            SetRingColor(selectedColor);
        }
        else {
            SetRingVisible(false);
        }
    }

    private void SetRingVisible(bool visible) {
        selectionRing.gameObject.SetActive(visible);
    }

    private void SetRingColor(Color color) {
        selectionRing.color = color;
    }
}