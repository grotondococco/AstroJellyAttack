using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyLevelCanvas : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas = default;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CanvasUtility.ShowCanvas(canvas);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CanvasUtility.HideCanvas(canvas);
    }
}
