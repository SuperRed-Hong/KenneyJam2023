using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    MapController mapController;
    [SerializeField]
    GameObject arrowPrefab;
    [SerializeField]
    float modifier=0.1f;

    GameObject[,] arrowList;

    void Start()
    {
        arrowList=new GameObject[10,5];
        for(int i=0;i<10;i++)
        {
            for(int j=0;j<5;j++)
            {
                arrowList[i,j]=Transform.Instantiate(arrowPrefab,transform);
            }
        }
    }

    void FixedUpdate()
    {
        if(mapController.GetRoomType(player.transform.position)!=MapCellType.notRoom)
        {
            List<Vector2> directions=mapController.GetNearRoomDirection(player.transform.position,7,20);
            for(int i=0;i<10;i++)
            {
                for(int j=0;j<5;j++)
                {
                    if(i<directions.Count)
                    {
                        arrowList[i,j].SetActive(true);
                        arrowList[i,j].transform.position=(Vector2)player.transform.position+directions[i]*modifier*j*0.2f;
                        arrowList[i,j].transform.rotation=Quaternion.Euler(0,0,Vector2.SignedAngle(Vector2.right,directions[i]));
                    }
                    else
                        arrowList[i,j].SetActive(false);
                }
            }
        }
        else
        {
            for(int i=0;i<10;i++)
            {
                for(int j=0;j<5;j++)
                {
                    arrowList[i,j].SetActive(false);
                }
            }
        }
    }
}
