using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject hearthObjects;
    GameObject[] hearts = new GameObject[3];

    int heartAmount = 0;

    private void Start()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = hearthObjects.transform.GetChild(i).gameObject;
            heartAmount += 1;

        }
    }

    public void NextRound()
    {

    }

    public void RemoveHearth()
    {
        heartAmount -= 1;
        hearts[heartAmount].SetActive(false);


    }
}
