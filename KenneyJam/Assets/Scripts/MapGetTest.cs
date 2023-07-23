using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGetTest : MonoBehaviour
{
    [SerializeField]
    float velocity=20;
    [SerializeField]
    MapController mapController;
    // Update is called once per frame

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    mapController.DestroyCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    Debug.Log(mapController.GetRoomType(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        //}
        //if (Input.GetMouseButtonDown(2))
        //{
        //    Debug.Log(mapController.GetBlockType(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        //}
        //if(Input.GetKeyDown(KeyCode.F))
        //{
        //    
        //}
        //Vector3 pos=Camera.main.transform.position;
        //if(Input.GetKey(KeyCode.W))
        //{
        //    Camera.main.transform.position=new Vector3(pos.x,pos.y+velocity*Time.deltaTime,pos.z);
        //}
        //if(Input.GetKey(KeyCode.S))
        //{
        //    Camera.main.transform.position=new Vector3(pos.x,pos.y-velocity*Time.deltaTime,pos.z);
        //}
        //if(Input.GetKey(KeyCode.D))
        //{
        //    Camera.main.transform.position=new Vector3(pos.x+velocity*Time.deltaTime,pos.y,pos.z);
        //}
        //if(Input.GetKey(KeyCode.A))
        //{
        //    Camera.main.transform.position=new Vector3(pos.x-velocity*Time.deltaTime,pos.y,pos.z);
        //}
    }
}
