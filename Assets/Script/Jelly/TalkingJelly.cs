using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingJelly : MonoBehaviour
{
    [SerializeField] CanvasGroup CanvasController = default;
    [SerializeField] CanvasGroup CanvasTalk = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Astro") {
            HideCanvasTalk();
            CanvasController.alpha = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Astro") {
            ShowCanvasTalk();
            CanvasController.alpha = 0f;
        }
    }


    private void ShowCanvasTalk()
    {
        CanvasTalk.alpha = 1;
    }

    private void HideCanvasTalk()
    {
        CanvasTalk.alpha = 0;
    }
}
