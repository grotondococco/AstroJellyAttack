using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MenuManager
{ 
   bool isPaused = false;
   [SerializeField] Image CanvasBackground = default;
   [SerializeField] AstroLobbyControls m_AstroLobbyControls = default;
   [SerializeField] AstroControls m_AstroControls = default;
   private AudioManager m_AudioManager;
    public bool canPause = false;
    private void Start()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&canPause)
        {
            isPaused = !isPaused;
            UpdateAstroControls();
            if (isPaused) showPauseMenu();
            else closePauseMenu();
        }
    }
    public void showPauseMenu()
    {
        Gamemanager.ShowCursor();
        m_AudioManager.PlayPauseIn();
        CanvasBackground.enabled = true;
        Time.timeScale = 0f;
        UpdateAstroControls();
        ShowMainMenu();
    }

    public void closePauseMenu()
    {
        Gamemanager.HideCursor();
        m_AudioManager.PlayPauseOut();
        CanvasBackground.enabled = false;
        isPaused = false;
        HideAllMenus();
        UpdateAstroControls();
        Time.timeScale = 1f;
    }

    //chiamata dal pulsate UI
    public void returnToMainMenu()
    {
        closePauseMenu();
        Gamemanager.ReturnToMenu();
    }

    public void returnToLobby()
    {
        closePauseMenu();
        Gamemanager.ReturnToLobby();
    }

    private void UpdateAstroControls()
    {
        if (m_AstroLobbyControls != null) m_AstroLobbyControls.isPaused = isPaused;
        if (m_AstroControls != null)
        {
            m_AstroControls.isPaused = isPaused;
        }
    }

}
