using UnityEngine;
using DG.Tweening;

public class MenuTransition : MonoBehaviour
{
    [Header("Main Equip Menu Panel")]
    [SerializeField] 
    private CanvasGroup equipMenuPanel;

    [Header("Category Screens")]
    
    // One reference per screen category
    [SerializeField]
    private CanvasGroup glovesScreen;
    
    [SerializeField]
    private CanvasGroup hatsScreen;
    
    [SerializeField]
    private CanvasGroup shirtsScreen;
    
    [SerializeField]
    private CanvasGroup pantsScreen;
    
    [SerializeField]
    private CanvasGroup bootsScreen;

    // How long the fade takes
    [SerializeField] private float fadeDuration = 0.5f;

    // Tracks which screen is currently open, fade out
    private CanvasGroup currentScreen;
    
    public void OpenGlovesScreen()
    {
        OpenScreen(glovesScreen);
    }

    public void OpenHatsScreen()
    {
        OpenScreen(hatsScreen);
    }

    public void OpenShirtsScreen()
    {
        OpenScreen(shirtsScreen);
    }

    public void OpenPantsScreen()
    {
        OpenScreen(pantsScreen);
    }

    public void OpenBootsScreen()
    {
        OpenScreen(bootsScreen);
    }

    // Called by the "Back to Main Equip Menu" button in any category screen
    public void GoBackToEquipMenu()
    {
        if (currentScreen != null) {
            FadePanel(currentScreen, false);
        }

        FadePanel(equipMenuPanel, true);
        currentScreen = null;
    }

    // Private helper — fades out the main menu and fades in the target screen
    private void OpenScreen(CanvasGroup targetScreen)
    {
        FadePanel(equipMenuPanel, false);
        FadePanel(targetScreen, true);

        // Remember which screen was opened so GoBackToEquipMenu knows what to close
        currentScreen = targetScreen;
    }

    // FadePanel helper
    private void FadePanel(CanvasGroup panel, bool fadeIn)
    {
        float targetAlpha = fadeIn ? 1f : 0f;
        panel.DOFade(targetAlpha, fadeDuration);
        panel.interactable = fadeIn;
        panel.blocksRaycasts = fadeIn;
    }
}