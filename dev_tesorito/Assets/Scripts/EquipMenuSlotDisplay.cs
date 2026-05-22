using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipMenuSlotDisplay : MonoBehaviour
{
    [Header("Equipment Slot")]
    public EquipmentSlot slot;

    [Header("Equipment Card Info")]
    [SerializeField] private Image equippedItemImage;
    [SerializeField] private TextMeshProUGUI weightValueText;
    [SerializeField] private TextMeshProUGUI defenseValueText;
    [SerializeField] private Image[] perkIcons = new Image[3];

    [Header("Placeholder Sprite")]
    [SerializeField] private Sprite placeholderSprite;

    private void Start()
    {
        // Subscribe to equipment changes, same event as before
        EquipmentManager.Instance.OnEquipmentChanged += Refresh;

        // Run once at start so the card shows the correct state immediately
        // (in case something was already equipped when the scene loaded)
        Refresh();
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent errors if this object is destroyed
        if (EquipmentManager.Instance != null)
        {
            EquipmentManager.Instance.OnEquipmentChanged -= Refresh;
        }
    }

    // This runs every time any equipment changes anywhere.
    // It checks its own slot and updates the card accordingly.
    private void Refresh()
    {
        Debug.Log("Refresh() called on slot: " + slot);
        // Ask the EquipmentManager what is currently in the slot
        EquipmentItem equippedItem = EquipmentManager.Instance.GetEquippedItem(slot);
        
        Debug.Log("Equipped item in slot " + slot + ": ");

        if (equippedItem != null) {
            // Something is equipped, populate the card with data
            ShowEquippedItem(equippedItem);
        }
        else {
            // Nothing equipped — show the empty placeholder state
            ShowEmptyState();
        }
    }

    private void ShowEquippedItem(EquipmentItem item)
    {
        // Set the item's artwork
        equippedItemImage.sprite = item.equippedItemSprite;

        // Update stat text
        weightValueText.text = item.weightValue.ToString();
        defenseValueText.text = item.defenseValue.ToString();

        // Update perk icons — same loop pattern from before
        for (int i = 0; i < perkIcons.Length; i++)
        {
            if (i < item.perks.Length && item.perks[i] != null) {
                perkIcons[i].gameObject.SetActive(true);
                perkIcons[i].sprite = item.perks[i].perkIcon;
            }
            else {
                perkIcons[i].gameObject.SetActive(false);
            }
        }
    }

    private void ShowEmptyState()
    {
        // Show the placeholder image
        equippedItemImage.sprite = placeholderSprite;

        // Clear the stat text
        weightValueText.text = "0";
        defenseValueText.text = "0";

        // Hide all perk icons
        for (int i = 0; i < perkIcons.Length; i++)
        {
            perkIcons[i].gameObject.SetActive(false);
        }
    }
}