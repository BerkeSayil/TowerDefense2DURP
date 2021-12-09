using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    
    GameObject[] towerList = new GameObject[3];
    public List<GameObject> enemiesInRange = new List<GameObject>();

    RandomEnemyGenerator gameController;
    GameObject baseObject;
    GameObject nearestEnemy;

    string tagGameController = "GameController";
    string tagBaseObject = "Base";

    //specs
    int tier;
    int color;
    int range;
    int health;

    //shooting
    float minDistance = 10000;
    float timer = 0;
    float cooldown;

    int COLOR_AMOUNT = 3;


    private void Start()
    {

        AssignTierThings();

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

                if(distanceRecent < range)
                {
                    if (minDistance > distanceRecent)
                    {
                        nearestEnemy = recentEnemy;
                        minDistance = distanceRecent;

                        return recentEnemy;
                    }
                }
 
            }
            
        }

        return null;
        

    }

    private void AssignTierThings()
    {
        if(tier == 0)
        {
            cooldown = 1f;
            range = 5;
            health = 2;

        }else if(tier == 1)
        {
            cooldown = 0.3f;
            range = 8;
            health = 4;

        }
        else if(tier == 2)
        {
            cooldown = 0.1f;
            range = 12;
            health = 6;
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
                Debug.Log("BOOM HEADSHOT!!!!!");
            }
        }
        
        
    }

    public void GetHit(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            gameObject.SetActive(false);
        }

    }

}
