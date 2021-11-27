using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    GameObject cardBefore;
    GameObject cardLatest;
    GameObject tileLatest;

    bool haveCard = false;
    bool merging = false;
   

    [SerializeField] Texture2D buildModeCursor;
    [SerializeField] Texture2D defaultModeCursor;
    [SerializeField] GameObject card;
    [SerializeField]GameObject towerPrefab;


    string PLACEABLE_TILE = "PlacementPos";
    string CARD = "Card";

    private void Start()
    {
        DefaultTheCursor();

        DealFullHand();
    }



    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            //left clicked
            if (haveCard)
            {
                
                //check placable
                if (LookForMouse() == PLACEABLE_TILE)
                {
                    PlaceTower(cardLatest);
                    haveCard = false;
                }

            }else if(LookForMouse() == CARD)
            {
                BuildMode();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            //cancel building
            if (haveCard)
            {
                ReturnCardBack();
                DefaultTheCursor();
            }
            //right clicked
            if (merging && LookForMouse() == CARD)
            {
                //do checking to merge
                MergeCards();
            }
            else if (LookForMouse() == CARD)
            {
                MergeReady();
            }
        }

        

    }

    private void PlaceTower(GameObject card) 
    {
        //
        //
        //

        DefaultTheCursor();
        Destroy(card);
    }

    private string LookForMouse()
    {   
        Vector2 mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null)
        {
            if(hit.collider.tag == CARD)
            {
                cardLatest = hit.collider.gameObject;
            }else if (hit.collider.tag == PLACEABLE_TILE)
            {
                tileLatest = hit.collider.gameObject;
            }


            return hit.collider.tag;
        }
        else
        {
            return "No tag";   
        }

    }

    private void ReturnCardBack()
    {
        haveCard = false;

        cardLatest.SetActive(true);
    }

    private void BuildMode()
    {
        haveCard = true;

        //do what you want to card to show its selected
        cardLatest.SetActive(false);

        //change cursor to build mode
        Vector2 cursorOffset = new Vector2(buildModeCursor.width / 2, buildModeCursor.height / 2);

        Cursor.SetCursor(buildModeCursor, cursorOffset, CursorMode.Auto);

    }

    private void MergeReady()
    {
        cardLatest = cardBefore;
        cardBefore.SetActive(false);


        merging = true;
    }

    private void MergeCards()
    {
        if (cardBefore.GetComponent<CardScript>().CanMergable(cardLatest.GetComponent<CardScript>()))
        {
            Vector2 position = cardLatest.transform.position;
            cardLatest.SetActive(false);

            //instantiate on position same color but tier +1 card
            Instantiate(card, position, Quaternion.identity);


        }
    }

    private void DefaultTheCursor()
    {
        Vector2 cursorOffset = new Vector2(defaultModeCursor.width / 2, defaultModeCursor.height / 2);

        Cursor.SetCursor(defaultModeCursor, cursorOffset, CursorMode.Auto);
    }

    private void DealFullHand()
    {
        for (int i = -15; i < 16; i += 6)
        {
            Instantiate(card, new Vector3(gameObject.transform.position.x + i,
                gameObject.transform.position.y, 0),
                Quaternion.identity);
           
        }
    }

}
