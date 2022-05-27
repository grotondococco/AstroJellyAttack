using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]TMPro.TextMeshProUGUI points = default;
    PointsManager m_PointsManager = default;
    [SerializeField] Image InstructionsCanvasBG = default;
    [SerializeField]
    Image IstructionsImage = default;
    [SerializeField] Image MerchantCanvasBG = default;

    [SerializeField] CanvasGroup InstructionsCanvas = default;
    [SerializeField] CanvasGroup MerchantCanvas = default;
    [SerializeField] AstroLobbyControls m_AstroLobbyControls = default;
    [SerializeField] InstructionsManager m_InstructionsManager = default;
    private void Start()
    {
        m_PointsManager = GameObject.Find("PointsManager").GetComponent<PointsManager>();
        UpdateGUI();
        Gamemanager.HideCursor();
    }

    public void UpdateGUI()
    {
        points.text = m_PointsManager.getTotalPoints().ToString();
    }

    public void ShowInstructions()
    {
        m_AstroLobbyControls.isPaused = true;
        m_AstroLobbyControls.m_AstroAnimationManager.SetAnimationIdle();
        Gamemanager.ShowCursor();
        CanvasUtility.ShowCanvas(InstructionsCanvas, InstructionsCanvasBG);
        IstructionsImage.enabled = true;
        m_InstructionsManager.nIstr = 0;
        m_InstructionsManager.ShowIstructions();
    }

    public void ShowPowerUpMenu()
    {
        m_AstroLobbyControls.isPaused = true;
        m_AstroLobbyControls.m_AstroAnimationManager.SetAnimationIdle();
        Gamemanager.ShowCursor();
        CanvasUtility.ShowCanvas(MerchantCanvas, MerchantCanvasBG);
    }

    //called by GUI
    public void HideInstructions() {
        m_AstroLobbyControls.isPaused = false;
        Gamemanager.HideCursor();
        IstructionsImage.enabled = false;
        CanvasUtility.HideCanvas(InstructionsCanvas, InstructionsCanvasBG);

    }
    //called by GUI
    public void HidePowerUpMenu() {
        m_AstroLobbyControls.isPaused = false;
        Gamemanager.HideCursor();
        CanvasUtility.HideCanvas(MerchantCanvas, MerchantCanvasBG);
    }
}
