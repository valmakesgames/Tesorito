using UnityEngine;

// Scriptable Object to make equipment items
[CreateAssetMenu(fileName = "NewEquipmentItem", menuName = "Equipment/Item")]
public class EquipmentItem : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    public string description;
    public Sprite equippedItemSprite;

    [Header("Stats")] 
    public float weightValue;
    public float defenseValue;

    [Header("Item Type")]
    public bool isLoreOnly;
    public bool isLegendary;

    [Header("Item Perks")] 
    public ItemPerk[] perks = new ItemPerk[3];
}
