using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;
    
    // These get filled at runtime. Adding [HideInInspector] to reduce clutter in Hierarchy
    [HideInInspector]
    public EquipmentItem equippedHat;
    [HideInInspector]
    public EquipmentItem equippedShirt;
    [HideInInspector]
    public EquipmentItem equippedGloves;
    [HideInInspector]
    public EquipmentItem equippedPants;
    [HideInInspector]
    public EquipmentItem equippedBoots;
    
    [Header("Default Items")]
    [SerializeField] private EquipmentItem defaultHat;
    [SerializeField] private EquipmentItem defaultShirt;
    [SerializeField] private EquipmentItem defaultGloves;
    [SerializeField] private EquipmentItem defaultPants;
    [SerializeField] private EquipmentItem defaultBoots;

    public float totalWeight;
    public float maxWeight = 70f;

    public event System.Action OnEquipmentChanged;
    public event System.Action OnLegendaryBlocked;

    private void Awake()
    {
        Instance = this;
        
        if (defaultHat != null) {
            TryEquipItem(EquipmentSlot.Hat, defaultHat);
        }

        if (defaultShirt != null) {
            TryEquipItem(EquipmentSlot.Shirt, defaultShirt);
        }

        if (defaultGloves != null) {
            TryEquipItem(EquipmentSlot.Gloves, defaultGloves);
        }

        if (defaultPants != null) {
            TryEquipItem(EquipmentSlot.Pants, defaultPants);
        }

        if (defaultBoots != null) {
            TryEquipItem(EquipmentSlot.Boots, defaultBoots);
        }
    }

    public EquipmentItem GetEquippedItem(EquipmentSlot slot)
    {
        if (slot == EquipmentSlot.Hat)    return equippedHat;
        if (slot == EquipmentSlot.Shirt)  return equippedShirt;
        if (slot == EquipmentSlot.Gloves) return equippedGloves;
        if (slot == EquipmentSlot.Pants)  return equippedPants;
        if (slot == EquipmentSlot.Boots)  return equippedBoots;
        return null;
    }

    private void SetEquippedItem(EquipmentSlot slot, EquipmentItem item)
    {
        if (slot == EquipmentSlot.Hat)    equippedHat = item;
        if (slot == EquipmentSlot.Shirt)  equippedShirt = item;
        if (slot == EquipmentSlot.Gloves) equippedGloves = item;
        if (slot == EquipmentSlot.Pants)  equippedPants = item;
        if (slot == EquipmentSlot.Boots)  equippedBoots = item;
    }

    public bool IsItemEquipped(EquipmentSlot slot, EquipmentItem item)
    {
        EquipmentItem currentItem = GetEquippedItem(slot);
        return currentItem == item;
    }

    private bool IsALegendaryAlreadyEquipped()
    {
        if (equippedHat != null && equippedHat.isLegendary)       return true;
        if (equippedShirt != null && equippedShirt.isLegendary)   return true;
        if (equippedGloves != null && equippedGloves.isLegendary) return true;
        if (equippedPants != null && equippedPants.isLegendary)   return true;
        if (equippedBoots != null && equippedBoots.isLegendary)   return true;
        return false;
    }

    public bool TryEquipItem(EquipmentSlot slot, EquipmentItem item)
    {
        if (item.isLegendary) {
            EquipmentItem currentlyInSlot = GetEquippedItem(slot);
            bool aLegendaryExistsElsewhere = IsALegendaryAlreadyEquipped();
            bool thisSlotAlreadyHasLegendary = currentlyInSlot != null && currentlyInSlot.isLegendary;

            if (aLegendaryExistsElsewhere && !thisSlotAlreadyHasLegendary) {
                if (OnLegendaryBlocked != null) {
                    OnLegendaryBlocked();
                }
                return false;
            }
        }

        EquipmentItem oldItem = GetEquippedItem(slot);
        if (oldItem != null) {
            totalWeight = totalWeight - oldItem.weightValue;
        }

        SetEquippedItem(slot, item);
        totalWeight = totalWeight + item.weightValue;

        if (OnEquipmentChanged != null) {
            OnEquipmentChanged();
        }
        return true;
    }

    public void UnequipItem(EquipmentSlot slot)
    {
        EquipmentItem currentItem = GetEquippedItem(slot);

        if (currentItem != null) {
            totalWeight = totalWeight - currentItem.weightValue;
            SetEquippedItem(slot, null);

            if (OnEquipmentChanged != null) {
                OnEquipmentChanged();
            }
        }
    }
}