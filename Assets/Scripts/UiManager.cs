using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject hearthObjects;
    [SerializeField] GameObject unHearthObjects;
    GameObject[] hearts = new GameObject[8];
    GameObject[] heartsEmpty = new GameObject[8];

    int heartAmount = 0;

    private void Start()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = hearthObjects.transform.GetChild(i).gameObject;
            heartsEmpty[i] = unHearthObjects.transform.GetChild(i).gameObject;
            heartAmount += 1;

        }
    }

    public void NextRound()
    {

    }

    public void RemoveHearth()
    {
        if(heartAmount > 0)
        {
            heartAmount -= 1;
            hearts[heartAmount].SetActive(false);
            heartsEmpty[heartAmount].SetActive(true);
        }
        else
        {
            LoseGameCanvas();
        }
        

    }

    private void LoseGameCanvas()
    {
        Time.timeScale = 0;

    }
}
