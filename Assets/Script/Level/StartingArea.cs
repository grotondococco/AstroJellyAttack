using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingArea : MonoBehaviour
{
    [SerializeField]SpriteRenderer alienSprite = default;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Astro")
        {
            collision.gameObject.GetComponent<AstroControls>().canRescue = true;
            alienSprite.color = new Color(alienSprite.color.r, alienSprite.color.g, alienSprite.color.b, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Astro")
        {
            collision.gameObject.GetComponent<AstroControls>().canRescue = false;
            alienSprite.color = new Color(alienSprite.color.r, alienSprite.color.g, alienSprite.color.b, 0.5f);
        }
    }
}
