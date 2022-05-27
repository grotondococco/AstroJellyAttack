using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI number3 = default;
    [SerializeField] TMPro.TextMeshProUGUI number2 = default;
    [SerializeField] TMPro.TextMeshProUGUI number1 = default;
    [SerializeField] TMPro.TextMeshProUGUI go = default;

    public bool timerEnd;
    private float timerSpeed=2.5f;

    public void StartTheTimer()
    {
        timerEnd = false;
        List<TMPro.TextMeshProUGUI> textList = new List<TMPro.TextMeshProUGUI>();
        textList.Add(number3);
        textList.Add(number2);
        textList.Add(number1);
        textList.Add(go);
        StartCoroutine(AppearAndDisappear(textList));
    }


    IEnumerator AppearAndDisappear(List<TMPro.TextMeshProUGUI> list) {
        foreach (TMPro.TextMeshProUGUI element in list) 
        {
            float t = 0;
            element.color = new Color(255, 0, 0, 0);
            while (t < 1)
            {
                element.color = new Color(255, 0, 0, t);
                t += Time.deltaTime* timerSpeed;
                yield return 0;

            }
            //se l'elemento è l'ultimo della lista (il go) non resettare la trasparenza
            if (list.IndexOf(element)!=list.Count-1)    
                element.color = new Color(255, 0, 0, 0);
            yield return 0;
        }
        StartCoroutine(Disappear(go));
    }

    IEnumerator Disappear(TMPro.TextMeshProUGUI text) {
        timerEnd = true;
        float t = 1;
        text.color = new Color(255, 0, 0, 1);
        while (t >= 0)
        {
            text.color = new Color(255, 0, 0, t);
            t -= Time.deltaTime* timerSpeed;
            yield return 0;
        }
        yield return 0;
    }
}
