using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Barrier : MonoBehaviour
{
    [SerializeField] Animator m_animator = default;

    public float damage = 10f;

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (collision.GetComponent<Astro>().vulnerable)
            {
                collision.GetComponent<Astro>().TakeDamage(damage);
                m_animator.SetTrigger("Triggered");
            }
        }
    }
}
