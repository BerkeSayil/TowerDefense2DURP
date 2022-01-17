using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyGenerator : MonoBehaviour
{
    [SerializeField]GameObject[] enemyList = new GameObject[3];
    [SerializeField]GameObject[] spawnList = new GameObject[16];
    public List<GameObject> enemies = new List<GameObject>();

    string spawnTag = "Spawn";
    float timePassed = 0;
    int enemyCount = 0;

    //generation size and speed variables
    float timeDelayBetween = 0.3f;

    private void Start()
    {
        spawnList = GameObject.FindGameObjectsWithTag(spawnTag);

    }
    private void Update()
    {
        if (timePassed >= timeDelayBetween)
        {
            RandomlySpawnEnemy();
            timePassed = 0;
            MakeHarder();

            Debug.Log(enemyCount);
        }
        else
        {
            timePassed += Time.deltaTime;
        }


    }

    private void MakeHarder()
    {
        
        switch (enemyCount)
        {
            case 74:
                timeDelayBetween = 0.25f;
                break;
            case 164:
                timeDelayBetween = 0.21f;
                break;
            case 220:
                timeDelayBetween = 0.17f;
                break;
            case 282:
                timeDelayBetween = 0.12f;
                break;
            case 380:
                timeDelayBetween = 0.075f;
                break;
            case 520:
                timeDelayBetween = 0.0342f;
                break;
            case 640:
                timeDelayBetween = 0.01666f; //its hell
                break;
        }
    }

    private void RandomlySpawnEnemy()
    {
        int[] enemyTypeWeight = {0,0,0,0,0,0,0,1,1,1,1,2,2,2 };

        int randomNum = enemyTypeWeight[Random.Range(0, enemyTypeWeight.Length)];
        int randomSpawn = Random.Range(0, 15);


        if (randomNum < enemyList.Length)
        {
            GameObject enemy = Instantiate(enemyList[randomNum], spawnList[randomSpawn].transform);

            enemies.Add(enemy);
            enemyCount += 1;
        }



    }




}
