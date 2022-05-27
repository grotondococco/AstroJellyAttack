using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            LoadGameResolution();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void LoadGameResolution()
    {
        ResolutionOptions rop=SaveManager.getGameResolution();
        Screen.SetResolution(rop.width, rop.height, true, rop.refresh);
    }
    public void NewGame()
    {
        SaveManager.NewGame();
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SaveManager.LoadData();
        SceneManager.LoadScene(1);
    }
    public static void ReturnToLobby() {
        SceneManager.LoadScene(1);
    }

    public static void Credits()
    {
        SceneManager.LoadScene(2);
    }

    public static void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit(0);
    }

    public static void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void ShowCursor()
    {
        Cursor.visible = true;
    }

    public static void HideCursor()
    {
        Cursor.visible = false;
    }
}
