using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemeyPrefab;
    public GameObject pUpPrefab;
    public int enemyCount;
    public int waveNumber = 1;
    private float spawnRange = 9.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnmeyWave(waveNumber);
        PowerUpSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            waveNumber++;
            SpawnEnmeyWave(waveNumber);
            PowerUpSpawn();
        }
    }

    void SpawnEnmeyWave(int enemiesToSpawn)
    {
        //Spawn enemeys over time. 
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemeyPrefab, GenerateSpawnPostion(), enemeyPrefab.transform.rotation);
        }    
    }

    //How to do return function.
    private Vector3 GenerateSpawnPostion()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    void PowerUpSpawn()
    {
        Instantiate(pUpPrefab, GenerateSpawnPostion(), pUpPrefab.transform.rotation);
    }
}
