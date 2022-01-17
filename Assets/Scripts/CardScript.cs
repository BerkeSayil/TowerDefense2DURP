using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    GameObject[] childs = new GameObject[6];
    private int CHILD_COUNT = 6;
    int tier; //0 -> 1 , 1 -> 2, 2-> 3
    int color; // 3 -> R , 4 -> G , 5 -> B

    private void Awake()
    {
        for (int i = 0; i < CHILD_COUNT; i++)
        {
            childs[i] = gameObject.transform.GetChild(i).gameObject;
        }

    }
    public void MergedCardCreation(CardScript card)
    {
        color = card.GetColor();
        childs[color].SetActive(true);

        tier = card.GetTier() + 1;
        childs[tier].SetActive(true);


    }
    public void RandomCardCreation()
    {
        //assign random color
        
        color = Random.Range(3, 6);
        childs[color].SetActive(true);

        //assign random tier
        int[] tierWeight = { 0, 0, 0, 0, 1, 1, 2 };

        tier = tierWeight[Random.Range(0, 7)];
        childs[tier].SetActive(true);

    }

    public bool CanMergable(CardScript card)
    {
        if (color == card.GetColor() && tier == card.GetTier())
        {
            if (tier != 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public int GetTier()
    {
        return tier;
    }
    public int GetColor()
    {
        return color;
    }
    

}
