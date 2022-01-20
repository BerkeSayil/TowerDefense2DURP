using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int damage;

    string TAG_TOWER = "Tower";

    private void DealDamage(Collision2D collision)
    {
        //TODO make it deal damage we have squares representing hearts they need to go down when base is toched

        TowerScript towerScript = collision.gameObject.GetComponent<TowerScript>();
        towerScript.GetHit(damage);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        //TODO make this  work bruh
        if (collision.gameObject.CompareTag(TAG_TOWER))
        {
            DealDamage(collision);

        }
        
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }




}
