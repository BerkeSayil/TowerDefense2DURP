using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyGenerator : MonoBehaviour
{
    [SerializeField]GameObject[] enemyList = new GameObject[3];
    [SerializeField]GameObject[] spawnList = new GameObject[16];
    public List<GameObject> enemies = new List<GameObject>();

    string spawnTag = "Spawn";
    int spawnedEnemy = 0;
    float timePassed = 0;
    float timeDelayBetween = 0.3f;

    private void Start()
    {
        spawnList = GameObject.FindGameObjectsWithTag(spawnTag);

    }
    private void Update()
    {
        if (timePassed >= timeDelayBetween)
        {
            if (spawnedEnemy <= 50) { 

                RandomlySpawnEnemy();
                timePassed = 0;
            }
        }
        else
        {
            timePassed += Time.deltaTime;
        }


    }
    private void RandomlySpawnEnemy()
    {
        int randomNum = Random.Range(0, 5);
        int randomSpawn = Random.Range(0, 15);


        if(randomNum < enemyList.Length)
        {
            GameObject enemy = Instantiate(enemyList[randomNum], spawnList[randomSpawn].transform);

            enemies.Add(enemy);
            spawnedEnemy += 1;
        }



    }




}
