using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;
    private GameData gd;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(instance != this) Destroy(gameObject);
    }
    private int totalPoints;
    public int getTotalPoints()
    {
        LoadGameFile();
        totalPoints = gd.points;
        return totalPoints;
    }

    private void LoadGameFile()
    {
        gd = SaveManager.getGameData();
    }

    //called when level is cleader100%
    public void SavePoints(int pointsToSave,int levelNumber=1)
    {
        LoadGameFile();
        totalPoints += pointsToSave;
        if(levelNumber>=gd.nLevelsUnlocked)
            SaveManager.SaveGame(new GameData(totalPoints, gd.nLevelsUnlocked + 1));
        else
            SaveManager.SaveGame(new GameData(totalPoints, gd.nLevelsUnlocked));
    }

    //called when level is cleared
    public void SavePoints(int pointsToSave) {
        LoadGameFile();
        totalPoints += pointsToSave;
        SaveManager.SaveGame(new GameData(totalPoints, gd.nLevelsUnlocked));
    }

    public void UsePoints(int pointsToUse) {
        LoadGameFile();
        totalPoints -= pointsToUse;
        SaveManager.SaveGame(new GameData(totalPoints, gd.nLevelsUnlocked));
    }

}
