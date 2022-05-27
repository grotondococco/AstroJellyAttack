using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge : MonoBehaviour
{
    GameObject [] bullets = default;
    Bullet_rifle[] bullets_in_cartridge;
    GameObject bulletType = default;
    AudioManager m_AudioManager = default;
    int n_total_bullets = 5;
    private void Start()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        bulletType = (GameObject)Resources.Load("bullet_rifle");
        bullets = new GameObject[n_total_bullets];
        bullets_in_cartridge = new Bullet_rifle[n_total_bullets];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(bulletType,gameObject.transform.position,gameObject.transform.rotation);
            bullets_in_cartridge[i] = bullets[i].GetComponent<Bullet_rifle>();
        }
    }

    public void FireBullet(string direction)
    {
        if (checkCartridge(direction))
        {
            //Debug.Log("Bullet fired!");
        }
        else
        {
            Debug.Log("Cartridge end! Reloading!");
        }
    }

    bool checkCartridge(string direction)
    {
        bool res = false;
        for(int i = 0; i < bullets_in_cartridge.Length; i++)
        {
            if (bullets_in_cartridge[i].bulletReady)
            {
                bullets_in_cartridge[i].Spawn(direction);
                m_AudioManager.PlayFire();
                res = true;
                break;
            }
        }
        return res;
    }

}
