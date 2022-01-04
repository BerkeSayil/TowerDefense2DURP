using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{

    GameObject[] towerList = new GameObject[3];


    //specs
    int tier;
    int color;
    int range;
    int health;

    //shooting
    float timer = 0;
    float cooldown;

    int COLOR_AMOUNT = 3;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, range);
    }

    private void Start()
    {

        AssignTierThings();

        //make tile beneath tagges as noRoad

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

        GameObject enemy = FindNearestEnemy();

        ShootAt(enemy);
    }

    private GameObject FindNearestEnemy()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(gameObject.transform.position, range);
        Collider2D nearestCollider = null;

        float minSqrtDist = Mathf.Infinity;

        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].CompareTag("Enemy"))
            {
                float sqrtDistanceToTower = (gameObject.transform.position - collider2Ds[i].transform.position).sqrMagnitude;
                if (sqrtDistanceToTower < minSqrtDist)
                {
                    minSqrtDist = sqrtDistanceToTower;
                    nearestCollider = collider2Ds[i];

                    return nearestCollider.gameObject;
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
        if(enemy != null)
        {
            timer += Time.deltaTime;


            if (timer >= cooldown)
            {
                if (enemy != null)
                {
                    enemy.GetComponent<EnemyScript>().TakeDamage(1);
                    timer = 0;
                    //hit
                    Debug.DrawLine(gameObject.transform.position, enemy.transform.position, Color.red, 0.3f, false);
                }
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
