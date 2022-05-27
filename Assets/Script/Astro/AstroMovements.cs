using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroMovements : MonoBehaviour
{
    float speed;
    [SerializeField] public float runningspeed = 2f;
    [SerializeField] Transform m_AstroTransform=default;
    [SerializeField] Rigidbody2D m_Rigidbody2D = default;

    private void Start()
    {
        speed = 2f;
        int poweruplevel = SaveManager.GetPowerupSettings().speedLevel;
        if (poweruplevel == 2) speed = speed * 1.2f;
        if (poweruplevel == 3) speed = speed * 1.5f;
        runningspeed = speed * 2;
        //Debug.Log("speed: " + speed + " runningspeed: " + runningspeed);
    }

    public void Move(Vector2 direction)
    {
        m_AstroTransform.Translate(direction * speed * Time.deltaTime);
    }

    public void Move(bool running)
    {
        if(running) m_Rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * runningspeed;
        else m_Rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
    }
}
