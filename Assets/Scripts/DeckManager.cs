using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    GameObject cardBefore;
    GameObject cardLatest;
    GameObject tileLatest;

    bool buildMode = false;
    bool merging = false;
    bool firstTime = true;

    string NO_TAG = "No tag";

    [SerializeField] Texture2D buildModeCursor;
    [SerializeField] Texture2D defaultModeCursor;
    [SerializeField] GameObject card;
    [SerializeField] GameObject towerPrefab;

    int DECK_SEPERATOR_CONST = 6;

    string PLACEABLE_TILE = "PlacementPos";
    string CARD = "Card";

    float timer;
    [SerializeField]float handTimerCoolDown = 20f;

    private void Start()
    {
        DefaultTheCursor();
      
    }


    private void Update()
    {

        if(Input.GetMouseButtonDown(0)){
            //left clicked
            if (buildMode)
            {
                //check placable
                if (LookForMouse() == PLACEABLE_TILE)
                {
                    PlaceTower(cardLatest);
                    CancelBuild();
                }

            }else if(LookForMouse() == CARD)
            {
                BuildMode();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            //cancel building
            if (buildMode)
            {
                ReturnCardBack();
            }
            else
            {

                //merge action
                if (merging && LookForMouse() == CARD)
                {
                    //do checking to merge
                    MergeCards();

                }
                else if (LookForMouse() == CARD)
                {
                    MergeReady();

                }else if (merging && LookForMouse() != CARD)
                {
                    CancelMerge();
                }
            }
           
        }

        HandTimer();
        
    }

    private void HandTimer()
    {

        timer += Time.deltaTime;
        

        if (timer >= handTimerCoolDown)
        {
            timer = 0;
            if (merging)
            {
                CancelMerge();
            }

            CancelBuild();
            
            DealFullHand();

        }
        else if(firstTime)
        {
            DealFullHand();
            firstTime = false;
        }


    }


    private void PlaceTower(GameObject card) 
    {
        
        GameObject tower = Instantiate(towerPrefab, tileLatest.transform);
        tower.GetComponent<TowerScript>().InstallTower(card.GetComponent<CardScript>());

        Destroy(card);

        tileLatest.tag = "Placed";

        AstarPath.active.Scan();

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
            return NO_TAG;   
        }

    }

    private void ReturnCardBack()
    {
        CancelBuild();

        cardLatest.SetActive(true);
    }

    private void BuildMode()
    {
        buildMode = true;

        //do what you want to card to show its selected
        cardLatest.SetActive(false);

        //change cursor to build mode
        Vector2 cursorOffset = new Vector2(buildModeCursor.width / 2, buildModeCursor.height / 2);

        Cursor.SetCursor(buildModeCursor, cursorOffset, CursorMode.Auto);

    }

    private void MergeReady()
    {
        cardBefore = cardLatest;
        cardBefore.SetActive(false);


        merging = true;
    }

    private void MergeCards()
    {
        if (cardBefore.GetComponent<CardScript>().CanMergable(cardLatest.GetComponent<CardScript>()))
        {
            Vector2 position = cardLatest.transform.position;
            int tier = cardLatest.GetComponent<CardScript>().GetTier();
            int color = cardLatest.GetComponent<CardScript>().GetColor();

            
            //instantiate on position same color but tier +1 card
            GameObject mergedCard = Instantiate(card, position, Quaternion.identity);
            
            mergedCard.GetComponent<CardScript>().MergedCardCreation(cardLatest.GetComponent<CardScript>());

            cardLatest.SetActive(false);
            merging = false;
            Destroy(cardBefore);
            Destroy(cardLatest);
        }
        else
        {
            CancelMerge();
        }
    }

    private void CancelMerge()
    {
        cardBefore.SetActive(true);

        merging = false;
    }

    private void CancelBuild()
    {
        buildMode = false;
        DefaultTheCursor();

    }

    private void DefaultTheCursor()
    {
        Vector2 cursorOffset = new Vector2(defaultModeCursor.width / 2, defaultModeCursor.height / 2);

        Cursor.SetCursor(defaultModeCursor, cursorOffset, CursorMode.Auto);
    }

    private void DealFullHand()
    {
        DeleteHand();

        for (int i = -2; i < 3; i++)
        {
            GameObject cardThis = Instantiate(card, new Vector3(gameObject.transform.position.x + (i * DECK_SEPERATOR_CONST),
                    gameObject.transform.position.y, 0),
                    Quaternion.identity);
            cardThis.GetComponent<CardScript>().RandomCardCreation();

        }
    }

    private void DeleteHand()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag(CARD);

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].SetActive(false);
            
        }

        if (buildMode)
        {
            CancelBuild();
        }
        if (merging)
        {
            CancelMerge();
        }


    }

}
