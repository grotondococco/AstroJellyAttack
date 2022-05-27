using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Transform[] GreenSP;
    [SerializeField] Transform[] FlySP;
    [SerializeField] Enemy greenEnemy = default;
    [SerializeField] Enemy flyEnemy = default;
    List<Enemy> enemyList = default;
    List<Transform> spList = default;
    float respawnCD = 10f;
    private void Start()
    {
        spList = new List<Transform>();
        enemyList = new List<Enemy>();
        Transform enemyGreenSP= GameObject.Find("SpawnPoints/Enemies/Green").transform;
        GreenSP = new Transform[enemyGreenSP.childCount];
        for (int i = 0; i < enemyGreenSP.childCount; i++)
        {
            GreenSP[i] = enemyGreenSP.GetChild(i);
            spList.Add(GreenSP[i]);
        }

        Transform enemyFlySP = GameObject.Find("SpawnPoints/Enemies/Fly").transform;
        FlySP = new Transform[enemyFlySP.childCount];
        for (int i = 0; i < enemyFlySP.childCount; i++)
        {
            FlySP[i] = enemyFlySP.GetChild(i);
            spList.Add(GreenSP[i]);
        }

        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < GreenSP.Length; i++)
            enemyList.Add(Instantiate(greenEnemy, GreenSP[i]));

        for (int i = 0; i < FlySP.Length; i++)
           enemyList.Add(Instantiate(flyEnemy, FlySP[i]));

        StartCoroutine(RecycleEnemies());
    }

    IEnumerator RecycleEnemies()
    {
        while (true)
        {
            foreach (Enemy en in enemyList)
            {
                if (!en.gameObject.activeSelf)
                {
                    yield return new WaitForSeconds(respawnCD);
                    en.transform.position = spList[(int)Random.Range(0,spList.Count)].position;
                    en.Spawn();
                }
            }
            yield return 0;
        }
    }

}
