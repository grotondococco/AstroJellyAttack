using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyManager : MonoBehaviour
{
    Transform[] fixedSpawnPoints;
    [SerializeField]Transform[] continousSpawnPoints;
    [SerializeField] Jelly[] jellyArray = new Jelly[2];
    float randomSpawnCooldown = 10f;
    LevelManager m_LevelManager = default;
    private void Start()
    {
        m_LevelManager= GameObject.Find("LevelManager").GetComponent<LevelManager>();
        Transform JelliesFixedSP= GameObject.Find("SpawnPoints/Jellies/Fixed").transform;
        fixedSpawnPoints = new Transform[JelliesFixedSP.childCount];
        for (int i = 0; i < fixedSpawnPoints.Length; i++)
            fixedSpawnPoints[i] = JelliesFixedSP.transform.GetChild(i);
        for (int i = 0; i < fixedSpawnPoints.Length; i++)
            Instantiate(jellyArray[1], fixedSpawnPoints[i]).Spawn(false);

        Transform JelliesSP = GameObject.Find("SpawnPoints/Jellies/Continous").transform;
        continousSpawnPoints = new Transform[JelliesSP.childCount];
        for (int i = 0; i < continousSpawnPoints.Length; i++) 
            continousSpawnPoints[i] = JelliesSP.transform.GetChild(i);

        StartCoroutine(RandomSpawn());
    }

    IEnumerator RandomSpawn() {
        while (m_LevelManager.points< m_LevelManager.pointsToStopJellySpawn) {
            yield return new WaitForSeconds(randomSpawnCooldown);
            Instantiate(jellyArray[0], continousSpawnPoints[(int)Random.Range(0, continousSpawnPoints.Length)]).Spawn();
        }
    }
}
