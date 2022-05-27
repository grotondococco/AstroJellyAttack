using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyLevelAreaManager : MonoBehaviour
{
    public bool level1, level2, level3;

    [SerializeField] GameObject particles1 = default;
    [SerializeField] GameObject particles2 = default;
    [SerializeField] GameObject particles3 = default;
    private void Start()
    {
        int nLevelsUnlocked = SaveManager.getGameData().nLevelsUnlocked;
        if (nLevelsUnlocked >= 1)
        {
            level1 = true;
            if (nLevelsUnlocked >= 2) { 
                level2 = true;
                if (nLevelsUnlocked == 3)
                {
                    level3 = true;
                }
            }
        }
        if (level1) particles1.SetActive(true);
        if (level2) particles2.SetActive(true);
        if (level3) particles3.SetActive(true);
    }
}
