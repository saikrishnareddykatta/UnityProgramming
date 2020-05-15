using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    private PlayerController playerControllerScript;
   
    private float spawnRangeX = 8;
    private float spawnPosZ = 15;
    private float spawnPosY = 0f;
    private float startDelay = 1.5f;
    private float spawnInterval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomEnemy()
    {
        if(playerControllerScript.gameOver == false)
        {
            int enemyIndex = Random.Range(0, enemies.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY, spawnPosZ);
            Instantiate(enemies[enemyIndex], spawnPos, enemies[enemyIndex].transform.rotation);
            
        }
        
    }
}
