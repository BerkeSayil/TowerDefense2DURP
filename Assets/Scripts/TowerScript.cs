using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    
    GameObject[] towerList = new GameObject[3];

    RandomEnemyGenerator gameController;
    GameObject baseObject;
    GameObject nearestEnemy;

    string tagGameController = "GameController";
    string tagBaseObject = "Base";

    int tier;
    int color;
    float minDistance = 10000;
    float timer = 0;
    float cooldown;

    int COLOR_AMOUNT = 3;


    private void Start()
    {

        AssignCooldown();

        gameController = GameObject.FindGameObjectWithTag(tagGameController)
            .GetComponent<RandomEnemyGenerator>();

        baseObject = GameObject.FindGameObjectWithTag(tagBaseObject);

        nearestEnemy = gameObject;

    }
    public void InstallTower(CardScript card)
    {

        for (int i = 0; i < COLOR_AMOUNT; i++)
        {
            towerList[i] = gameObject.transform.GetChild(i).gameObject;
        }

        tier = card.GetTier();
        color = card.GetColor() - COLOR_AMOUNT;


        GameObject tower = towerList[color];
        tower.SetActive(true);

        tower.transform.GetChild(tier).gameObject.SetActive(true);
        
    }

    private void Update()
    {
        GameObject enemy = FindNearestToBaseEnemy();

        ShootAt(enemy);
    }

    private GameObject FindNearestToBaseEnemy()
    {

        for(int i = 0; i < gameController.enemies.Count; i++)
        {
            if (!gameController.enemies[i].activeInHierarchy || gameController.enemies[i] == null)
            {
                gameController.enemies.Remove(gameController.enemies[i]);
                minDistance = 10000;
                return null;
                
                
            }
            else
            {
                GameObject recentEnemy = gameController.enemies[i];
                float distanceRecent = Vector2.Distance(recentEnemy.transform.position,
                    baseObject.transform.position);

                if (minDistance > distanceRecent)
                {
                    nearestEnemy = recentEnemy;
                    minDistance = distanceRecent;
                }

            }
            
        }
        Debug.DrawLine(this.gameObject.transform.position, nearestEnemy.transform.position);
        return nearestEnemy;

    }

    private void AssignCooldown()
    {
        if(tier == 0)
        {
            cooldown = 1.5f;

        }else if(tier == 1)
        {
            cooldown = 0.3f;

        }else if(tier == 2)
        {
            cooldown = 0.15f;

        }
    }


    private void ShootAt(GameObject enemy)
    {
        timer += Time.deltaTime;


        if(timer >= cooldown)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyScript>().TakeDamage(1);
                timer = 0;
            }
        }
        
        
    }

}
