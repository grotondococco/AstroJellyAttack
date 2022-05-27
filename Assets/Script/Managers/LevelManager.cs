using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    AudioManager m_AudioManager = default;
    PointsManager m_PointsManager = default;
    [SerializeField] Timer m_Timer = default;
    [SerializeField] float timeForBonus = default;
    [SerializeField] Image CanvasImage = default;
    [SerializeField] CanvasGroup canvasResults = default;
    [SerializeField] TMPro.TextMeshProUGUI timerResultsText = default;
    [SerializeField] TMPro.TextMeshProUGUI timerText = default;
    [SerializeField] TMPro.TextMeshProUGUI jelliesText = default;
    [SerializeField] TMPro.TextMeshProUGUI pointsText = default;
    [SerializeField] public int pointsToStopJellySpawn = default;
    [SerializeField] int LevelNumber=default;
    [SerializeField] GameObject levelPassedText = default;
    [SerializeField] GameObject levelMiddleText = default;
    [SerializeField] GameObject levelFailedText = default;
    int resultspoints = 0;
    public int points = 0;
    int extrapoints = 0;
    int nSpecialJellies = 0;
    int nJellies = 0;
    int totalSpecialJellies = 0;
    bool levelCleared100 = false;
    private void Start()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_AudioManager.PlayLevelTrack();
        m_PointsManager= GameObject.Find("PointsManager").GetComponent<PointsManager>();
        Transform JelliesFixedSP = GameObject.Find("SpawnPoints/Jellies/Fixed").transform;
        totalSpecialJellies = JelliesFixedSP.childCount;
        Gamemanager.HideCursor();
    }

    public void checkLevelEsit(bool dead=false)
    {
        Time.timeScale = 0f;

        if (!dead)
            LevelClear();
        else
            LevelFailed();

        ShowResults();
    }

    public void LevelClear()
    {
        timerResultsText.text = timerText.text;
        jelliesText.text = nJellies.ToString() + " +Caged: " + nSpecialJellies.ToString();
        levelMiddleText.SetActive(true);
        CalculatePoints();
        CalculateExtraPoints();
        pointsText.text = resultspoints.ToString() + " +Extra: " + extrapoints.ToString();
        m_AudioManager.PlayLevelClear();
    }

    public void LevelFailed() {
        timerResultsText.text = timerText.text;
        jelliesText.text = "LostAll";
        pointsText.text = "0";
        levelFailedText.SetActive(true);
        m_AudioManager.PlayLevelFail();
    }

    public void ShowResults()
    {
        CanvasUtility.ShowCanvas(canvasResults, CanvasImage);
        Gamemanager.ShowCursor();
    }

    public void HideResults()
    {
        CanvasUtility.HideCanvas(canvasResults, CanvasImage);
    }

    //richiamato da pulsate UI
    public void ReturntoLobby()
    {
        HideResults();
        SaveResults();
        Time.timeScale = 1f;
        Gamemanager.ReturnToLobby();
    }

    public void AddPoints(int pointsToAdd)
    {
        nJellies += 1;
        points += pointsToAdd;
    }
    public void AddPointsSpecialJelly(int pointsToAdd)
    {
        nSpecialJellies += 1;
        points += pointsToAdd*20;
    }

    private void CalculatePoints()
    {
        resultspoints = points;
    }
     
    private void CalculateExtraPoints()
    {
        if (nSpecialJellies >= totalSpecialJellies)
        {
            levelMiddleText.SetActive(false);
            if (m_Timer.gameTime < timeForBonus) { 
                extrapoints = (int)Mathf.Round(timeForBonus - m_Timer.gameTime);
            }
            levelCleared100 = true;
            levelPassedText.SetActive(true);
        }
    }

    private void SaveResults()
    {
        if(levelCleared100) m_PointsManager.SavePoints(resultspoints + extrapoints, LevelNumber);
        else m_PointsManager.SavePoints(resultspoints+extrapoints);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(sceneName);
    }
}
