using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject shopUI;

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            collision.transform.Find("player/interactableItem").gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                //全局暂停 
                shopUI.SetActive(true) ;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.Find("player/interactableItem").gameObject.SetActive(false);
            
        }
    }
}
