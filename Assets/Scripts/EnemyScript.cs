using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int damage;

    private void DealDamage()
    {
        //TODO make it deal damage we have squares representing hearts they need to go down when base is toched
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
    }




}
