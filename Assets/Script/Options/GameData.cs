using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int points;
    public int nLevelsUnlocked;
    public GameData(int points = 0, int nLevelsUnlocked = 1)
    {
        this.points = points;
        this.nLevelsUnlocked = nLevelsUnlocked;
    }
}
