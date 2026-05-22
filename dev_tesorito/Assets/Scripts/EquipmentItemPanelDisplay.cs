using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class EquipmentItemPanelDisplay : MonoBehaviour
{
    [Header("Card Reference")]
    [SerializeField] 
    private EquipmentItemCardDisplay cardDisplay;

    [Header("Item Details")]
    [SerializeField] 
    private TextMeshProUGUI itemNameText;
    
    [SerializeField] 
    private TextMeshProUGUI itemDescriptionText;

    [Header("Perk Rows")]
    [SerializeField] 
    private Image[] perkRowIcons = new Image[3];
    
    [SerializeField] 
    private TextMeshProUGUI[] perkRowNames = new TextMeshProUGUI[3];
    
    [SerializeField] 
    private TextMeshProUGUI[] perkRowDescriptions = new TextMeshProUGUI[3];

    [Header("Equip Button")]
    [SerializeField] 
    private TextMeshProUGUI equipButtonText;

    [Header("Legendary Warning")]
    [SerializeField] 
    private TextMeshProUGUI legendaryWarningText;

    [Header("Lore Only Blurb")]
    [SerializeField] 
    private TextMeshProUGUI loreBlurbText;

    [Header("Panel Contents")]
    [SerializeField] 
    private GameObject equippableContent;

    private EquipmentItemIcon currentlySelectedIcon;
    private EquipmentItem currentItem;
    private EquipmentSlot currentSlot;

    private Vector2 hiddenPosition;
    private Vector2 shownPosition;
    
    [SerializeField] 
    private float slideDuration = 0.4f;

    private void Awake()
    {
        // Store positions and set starting state
        shownPosition = GetComponent<RectTransform>().anchoredPosition;
        hiddenPosition = shownPosition + new Vector2(-800f, 0f);
        GetComponent<RectTransform>().anchoredPosition = hiddenPosition;
        gameObject.SetActive(false);

        if (legendaryWarningText != null)
            legendaryWarningText.gameObject.SetActive(false);
    }

    private void Start()
    {
        EquipmentManager.Instance.OnLegendaryBlocked += ShowLegendaryWarning;
    }

    private void OnDestroy()
    {
        if (EquipmentManager.Instance != null)
            EquipmentManager.Instance.OnLegendaryBlocked -= ShowLegendaryWarning;
    }

    public void Populate(EquipmentItem item, EquipmentSlot slot, EquipmentItemIcon clickedIcon)
    {
        // Icon selection state, clear white selection ring
        if (currentlySelectedIcon != null && currentlySelectedIcon != clickedIcon) {
            currentlySelectedIcon.Deselect();
        }
        
        // New, currently selected icon
        currentlySelectedIcon = clickedIcon;
        
        currentItem = item;
        currentSlot = slot;

        if (legendaryWarningText != null) {
            legendaryWarningText.gameObject.SetActive(false);
        }

        if (item.isLoreOnly) {
            equippableContent.SetActive(false);
            loreBlurbText.gameObject.SetActive(true);
            loreBlurbText.text = item.description;
            return;
        }

        equippableContent.SetActive(true);
        loreBlurbText.gameObject.SetActive(false);

        cardDisplay.Populate(item);
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.description;
        UpdateEquipButtonText();

        for (int i = 0; i < perkRowIcons.Length; i++)
        {
            if (i < item.perks.Length && item.perks[i] != null) {
                perkRowIcons[i].gameObject.SetActive(true);
                perkRowNames[i].gameObject.SetActive(true);
                perkRowDescriptions[i].gameObject.SetActive(true);

                perkRowIcons[i].sprite = item.perks[i].perkIcon;
                perkRowNames[i].text = item.perks[i].perkName;
                perkRowDescriptions[i].text = item.perks[i].perkDescription;
            }
            else {
                perkRowIcons[i].gameObject.SetActive(false);
                perkRowNames[i].gameObject.SetActive(false);
                perkRowDescriptions[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnEquipButtonClicked()
    {
        if (EquipmentManager.Instance.IsItemEquipped(currentSlot, currentItem)) {
            EquipmentManager.Instance.UnequipItem(currentSlot);
        }
        else {
            EquipmentManager.Instance.TryEquipItem(currentSlot, currentItem);
        }

        UpdateEquipButtonText();
    }

    private void UpdateEquipButtonText()
    {
        if (EquipmentManager.Instance.IsItemEquipped(currentSlot, currentItem)) {
            equipButtonText.text = "UNEQUIP";
        }
        else {
            equipButtonText.text = "EQUIP ITEM";
        }
    }

    private void ShowLegendaryWarning()
    {
        if (legendaryWarningText != null) {
            legendaryWarningText.gameObject.SetActive(true);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        GetComponent<RectTransform>()
            .DOAnchorPos(shownPosition, slideDuration)
            .SetEase(Ease.OutCubic);
    }

    public void Hide()
    {
        GetComponent<RectTransform>()
            .DOAnchorPos(hiddenPosition, slideDuration)
            .SetEase(Ease.InCubic)
            .OnComplete(() => gameObject.SetActive(false));
    }

    public void ClearSelection() 
    {
        currentlySelectedIcon = null;
    }
}