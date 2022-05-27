using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]TMPro.TextMeshProUGUI timerText = default;
    public float gameTime;
    private void Start()
    {
        gameTime = 0;
        timerText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }
    public void StartTimer(bool debug=false) {
        Start();
        if (!debug) 
            StartCoroutine(checkTime(Time.time));
        else
            timerText.text = "Time: Debug";
    }
    void StopTimer() {
        StopAllCoroutines();
    }

    IEnumerator checkTime(float startingTime)
    {
        while (true)
        {
            gameTime = Time.time - startingTime;
            UpdateText(gameTime);
            yield return new WaitForSeconds(0.125f);
        }
    }

    void UpdateText(float time)
    {
        int hours = (int)Mathf.Floor(time / 360);
        int minutes = (int)Mathf.Floor(time / 60);
        int seconds = (int)Mathf.Floor(time % 60);
        timerText.text = ("Time: " + hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2"));
    }

}
