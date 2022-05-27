using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    [SerializeField] Enemy[] enemyArray = default;
    TilemapRenderer m_TilemapRenderer = default;
    AudioManager m_AudioManager = default;
    
    private void Start()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_TilemapRenderer = gameObject.GetComponent<TilemapRenderer>();
        Close();
        StartCoroutine(CheckEnemies());
    }

    void Open() {
        gameObject.SetActive(false);
    }
    void Close()
    {
        gameObject.SetActive(true);
    }

    IEnumerator CheckEnemies()
    {
        while (!AllEnemiesDefeated())
        {
            for(int i = 0; i < enemyArray.Length; i++)
            {
                if(enemyArray[i]!=null)
                    if (!enemyArray[i].gameObject.activeSelf) 
                            enemyArray[i] = null;
            }
            yield return 0;
        }
        StartCoroutine(Unlock());
        yield return 0;
    }

    bool AllEnemiesDefeated()
    {
        for (int i = 0; i < enemyArray.Length; i++)
             if (enemyArray[i] != null) return false;
        return true;
    }

    IEnumerator Unlock()
    {
        m_AudioManager.PlayDoorUnlock();
        for (int i = 0; i < 3; i++)
        {
            m_TilemapRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            m_TilemapRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        Open();
        yield return 0;
    }
}
