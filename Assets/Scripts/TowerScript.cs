using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    
    GameObject[] towerList = new GameObject[3];
    

    int tier;
    int color;

    int COLOR_AMOUNT = 3;

   

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
}
