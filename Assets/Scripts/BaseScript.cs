using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    //TODO Not working fix it aq

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Base");

        }
        else
        {
            Debug.Log(collision.gameObject.tag);
        }
    }
}
