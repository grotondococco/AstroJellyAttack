using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private Enemy m_Enemy = default;
    CircleCollider2D m_circleCollider2D = default;
    private float standardRadius = 2.5f;
    private void Start()
    {
        m_circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        m_circleCollider2D.radius = standardRadius;
        m_Enemy = gameObject.transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collisione del Radar con Astro
        if (collision.gameObject.layer == 10)
        {
            m_Enemy.AstroSpotted(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //collisione del Radar con Astro
        if (collision.gameObject.layer == 10)
        {
            m_Enemy.AstroLost();
            ResetRadius();
        }
    }

    public void ImproveRadius() {
        m_circleCollider2D.radius = standardRadius * 3;
    }

    private void ResetRadius()
    {
        m_circleCollider2D.radius = standardRadius;
    }
}
