using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    private bool trigger = false;
    [SerializeField] Image BackImage = default;
    private void Start()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayCreditsTrack();
        StartCoroutine(CheckUserInput());
        //StartCoroutine(CheckConfirm());
        Gamemanager.HideCursor();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void showCursor()
    {
        Gamemanager.ShowCursor();
    }

    IEnumerator CheckUserInput()
    {
        while (true)
        {
            if (Input.GetMouseButtonUp(0)&&!trigger) {
                trigger = true;
                StartCoroutine(BackShow());
            }
            yield return 0;
        }
    }

   /* IEnumerator CheckConfirm()
    {
        while (true)
        {
            if (trigger)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ReturnToMenu();
                }

            }
            yield return 0;
        }
    }*/

    IEnumerator BackShow(float duration=3f)
    {
        Gamemanager.ShowCursor();
        float finish = Time.time + duration;
        BackImage.gameObject.SetActive(true);
        while (Time.time<finish) yield return new WaitForSeconds(0.125f);
        trigger = false;
        BackImage.gameObject.SetActive(false);
        Gamemanager.HideCursor();
        yield return 0;
    }

    
}
