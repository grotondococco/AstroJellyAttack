using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsLobby : MonoBehaviour
{
    TMPro.TextMeshProUGUI pointsText = default;
    private void Start()
    {
        pointsText = GetComponent<TMPro.TextMeshProUGUI>();
        pointsText.text=(SaveManager.getGameData().points).ToString();
    }
}
