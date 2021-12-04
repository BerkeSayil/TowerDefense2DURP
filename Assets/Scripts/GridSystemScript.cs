using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemScript : MonoBehaviour
{
    [SerializeField]GameObject placableTilesParent;
    [SerializeField]GameObject tilePrefab;
    [SerializeField]GameObject baseBuilding; 
    Vector2 startPos = new Vector2(-30f, 15.5f);
    Vector2 endPos = new Vector2(30f, -10f);

    float xDiff = 3.54f; //depends on sprite
    float yDiff = 3f;   //same depends on sprite
    
    void Start()
    {
        float lineNum = 1;
        for (float positionY = startPos.y; positionY > endPos.y; positionY -= yDiff)
        {
            if(lineNum % 2 == 0)
            {
                for (float positionX = startPos.x + xDiff/2 ; positionX < endPos.x; positionX += xDiff)
                {
                    GameObject tiles = Instantiate(tilePrefab, new Vector2(positionX, positionY),
                        Quaternion.identity, placableTilesParent.transform);

                    DeleteUnderBase(tiles);
                }
            }
            else
            {
                for (float positionX = startPos.x; positionX < endPos.x; positionX += xDiff)
                {
                    GameObject tiles = Instantiate(tilePrefab, new Vector2(positionX, positionY), 
                        Quaternion.identity, placableTilesParent.transform);

                    DeleteUnderBase(tiles);
                }
            }

            lineNum += 1;

        }

    }

    private void DeleteUnderBase(GameObject tile)
    {
        ColliderDistance2D colliderDis = tile.GetComponent<CircleCollider2D>
            ().Distance(baseBuilding.GetComponent<BoxCollider2D>());
        if (colliderDis.distance < 0)
        {
            Destroy(tile);
        }

    }
}
