using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenArea : MonoBehaviour
{
    float oxygenMaxQuantity = 50f;
    float oxygenQuantity;
    float oxygenTransfusionQuantity;
    float transfusionDeltatime = 1f;

    float imageMaxTransparence = 0.70f;
    float imageMinTransparence = 0.10f;

    [SerializeField] ParticleSystem bubbles = default;
    ParticleSystem.MainModule bubbleMainModule = default;
    [SerializeField] SpriteRenderer oxygenImageOnGround = default;

    private void Start()
    {
        oxygenQuantity = oxygenMaxQuantity;
        oxygenTransfusionQuantity = oxygenQuantity / 10;
        oxygenImageOnGround.color = new Color(1f, 1f, 1f, imageMaxTransparence);
        bubbleMainModule = bubbles.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collisione con Astro
        if (collision.gameObject.layer == 10 && oxygenQuantity > 0)
        {
            collision.gameObject.GetComponent<Astro>().needToBreathe = false;
            StartCoroutine(OxygenTransfusion(collision.gameObject.GetComponent<Astro>()));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Astro
        if (collision.gameObject.layer == 10)
        {
            StopAllCoroutines();
            bubbleMainModule.simulationSpeed = 1;
            collision.gameObject.GetComponent<Astro>().needToBreathe = true;
        }
    }

    IEnumerator OxygenTransfusion(Astro astro)
    {
        ParticleSystem.MainModule mm = bubbles.main;
        bubbleMainModule.simulationSpeed = 3;
        while (oxygenQuantity > 0)
        {
            astro.AddOxygen(oxygenTransfusionQuantity);
            oxygenQuantity -= oxygenTransfusionQuantity;
            oxygenImageOnGround.color = new Color(1f, 1f, 1f, 
                Mathf.Clamp(oxygenQuantity * 0.7f / oxygenMaxQuantity,imageMinTransparence, imageMaxTransparence));
            yield return new WaitForSeconds(transfusionDeltatime);
        }
        astro.needToBreathe = true;
        bubbles.Stop();
        oxygenImageOnGround.color = new Color(1f, 1f, 1f, 0f) ;
    }
}
