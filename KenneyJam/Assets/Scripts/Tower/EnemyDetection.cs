using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            transform.parent.GetComponent<TowerController>().enemyInDetection.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            transform.parent.GetComponent<TowerController>().enemyInDetection.Remove(collision.gameObject);
        }
    }
}
