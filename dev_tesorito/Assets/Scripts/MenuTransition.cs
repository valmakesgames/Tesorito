using UnityEngine;
using DG.Tweening;

public class MenuTransition : MonoBehaviour
{
    // CanvasGroup menu references
    [SerializeField] 
    CanvasGroup equipMenuCanvasGroup;
    [SerializeField]
    CanvasGroup equipmentItemsCanvasGroup;

    // Fade animation duration
    [SerializeField] 
    float fadeDuration = 0.5f;
    
    public void GoToEquipmentItems()
    {
        // Fade Equip Menu out (alpha goes to 0)
        equipMenuCanvasGroup.DOFade(0f, fadeDuration);
        
        // Fade Equipment Menu in (alpha goes to 1)
        equipmentItemsCanvasGroup.DOFade(1f, fadeDuration);
        
        // Toggling CanvasGroup interactable and blocksRaycasts
        equipMenuCanvasGroup.interactable = false;
        equipMenuCanvasGroup.blocksRaycasts = false;
        
        equipmentItemsCanvasGroup.interactable = true;
        equipmentItemsCanvasGroup.blocksRaycasts = true;
        
        Debug.Log("Going to see the Equipment!");
    }

    public void GoBackToEquipMenu() {
        equipmentItemsCanvasGroup.DOFade(0f, fadeDuration);
        equipMenuCanvasGroup.DOFade(1f, fadeDuration);
        
        // Toggling CanvasGroup interactable and blocksRaycasts
        equipmentItemsCanvasGroup.interactable = false;
        equipmentItemsCanvasGroup.blocksRaycasts = false;
        
        equipMenuCanvasGroup.interactable = true;
        equipMenuCanvasGroup.blocksRaycasts = true;
        
        Debug.Log("Going back to Equip Menu");
    }
}
    
