using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    GameObject[] childs = new GameObject[6];
    private int CHILD_COUNT = 6;
    int tier; //0 -> 1 , 1 -> 2, 2-> 3
    int color; // 3 -> R , 4 -> G , 5 -> B
    public bool merging = false;

    private void Start()
    {
        //childs are assigned
        for(int i = 0; i < CHILD_COUNT; i++)
        {
            childs[i] = gameObject.transform.GetChild(i).gameObject;
        }

        if (!merging)
        {
            RandomColor();
            RandomTier();
        }
        else
        {

        }
        

    }
    public bool CanMergable(CardScript card)
    {
        if (color == card.GetColor() && tier == card.GetTier())
        {
            return true;
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


    private void RandomColor()
    {
        //assign random color
        color = Random.Range(3, 6);
        childs[color].SetActive(true);
    }

    private void RandomTier()
    {
        //assign random tier
        tier = Random.Range(0, 3);
        for (int i = 0; i < tier + 1; i++)
        {
            childs[i].gameObject.SetActive(true);
        }
    }
}
