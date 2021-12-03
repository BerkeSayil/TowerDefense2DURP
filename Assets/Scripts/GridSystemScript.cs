using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemScript : MonoBehaviour
{
    [SerializeField]GameObject placableTilesParent;
    [SerializeField] GameObject tilePrefab;
    GameObject[] placableTiles = new GameObject[0];
    Vector2 startPos = new Vector2(-27.75f, 15.5f);
    Vector2 endPos = new Vector2(27.5f, -8f);

    float xDiff = 1.5f;
    float yDiff = 2f;

    void Start()
    { 
        for (float positionY = startPos.y; positionY > endPos.y; positionY -= yDiff)
        {
            for (float positionX = startPos.x; positionX < endPos.x; positionX += xDiff)
            {
                Instantiate(tilePrefab, new Vector2(positionX, positionY), Quaternion.identity);
            }

        }
        


    }
}
