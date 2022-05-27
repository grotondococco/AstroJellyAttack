using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] CanvasGroup MainMenu = default;
    [SerializeField] CanvasGroup OptionalMenu = default;
    public void ShowMainMenu()
    {
        HideOptionalMenu();
        CanvasUtility.ShowCanvas(MainMenu, true);
    }

    private void HideMainMenu() 
    {
        CanvasUtility.HideCanvas(MainMenu, true);
    }

    public void ShowOptionalMenu()
    {
        HideMainMenu();
        CanvasUtility.ShowCanvas(OptionalMenu,true);
    }

    private void HideOptionalMenu()
    {
        CanvasUtility.HideCanvas(OptionalMenu, true);
    }
    
    public void HideAllMenus()
    {
        HideMainMenu();
        HideOptionalMenu();
    }
}
