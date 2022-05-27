using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class CanvasUtility
{
    public static void ShowCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 1;
    }

    public static void ShowCanvas(CanvasGroup canvas, bool withinteractions)
    {
        if (withinteractions)
        {
            canvas.alpha = 1;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }
        else
        {
            canvas.alpha = 1;
        }
    }


    public static void ShowCanvas(CanvasGroup canvas,Image backgroundimage)
    {
        backgroundimage.enabled = true;
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }


    public static void HideCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 0;
    }
    public static void HideCanvas(CanvasGroup canvas, bool interactions)
    {
        if (interactions)
        {
            canvas.alpha = 0;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }
        else
        {
            canvas.alpha = 0;
        }
    }

    public static void HideCanvas(CanvasGroup canvas, Image backgroundimage)
    {
        backgroundimage.enabled = false;
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

}
