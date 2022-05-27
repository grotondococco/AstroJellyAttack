using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBG : MonoBehaviour
{
    Camera cam;
    bool isLerping = false;
    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        cam.backgroundColor = Color.white;
        StartCoroutine(ColorSpin());
    }

    IEnumerator ColorSpin()
    {
        while (true)
        {
            if (!isLerping)
            {
                StartCoroutine(ColorLerp());
            }
            yield return new WaitForSeconds(0.125f);
        }
    }

    IEnumerator ColorLerp()
    {
        isLerping = true;
        float t = 0;
        Color col= new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f), 1f);
        while (t < 1)
        {
            t += Time.deltaTime/3;
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, col, t);
            yield return 0;
        }
        isLerping = false;
        yield return 0;
    }

}
