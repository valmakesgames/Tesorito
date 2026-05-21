using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentItemCardDisplay : MonoBehaviour
{
    [Header("Card Visuals")]
    [SerializeField] private Image equippedItemImage;
    [SerializeField] private TextMeshProUGUI weightValueText;
    [SerializeField] private TextMeshProUGUI defenseValueText;

    // Exactly 3 slots for perk icons — matches our max 3 perks per item
    [SerializeField] private Image[] perkIcons = new Image[3];

    // Hand this method an EquipmentItem and fill the card with that item's data
    public void Populate(EquipmentItem item)
    {
        equippedItemImage.sprite = item.equippedItemSprite;
        weightValueText.text = item.weightValue.ToString();
        defenseValueText.text = item.defenseValue.ToString();

        // Loop through the 3 perk icon slots
        for (int i = 0; i < perkIcons.Length; i++)
        {
            // Check if this item actually has a perk at position i
            if (i < item.perks.Length && item.perks[i] != null)
            {
                // There is a perk here — show the icon
                perkIcons[i].gameObject.SetActive(true);
                perkIcons[i].sprite = item.perks[i].perkIcon;
            }
            else
            {
                // No perk in this slot — hide the icon
                // This handles items with 1 or 2 perks
                perkIcons[i].gameObject.SetActive(false);
            }
        }
    }
}