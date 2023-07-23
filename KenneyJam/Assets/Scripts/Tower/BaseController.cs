using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public GameObject shield;

    public Spawn shop;

    public Transform relifePos;

    public int HP;

    private readonly int MAX_HP = 1500;

    private readonly int HEAL_HP = 300;

    public void OnAttack(int damage)
    {
        HP -= damage;
        if(HP < 0)
        {
            //TODO
        }
    }

    public void Heal()
    {
        HP += HEAL_HP;
        if (HP > 1500) HP = 1500;
    }

    public void Initialize(GameObject shopUI)
    {
        HP = 1500;
        shop.shopUI = shopUI;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            //TODO
            OnAttack(100);
            Destroy(collision.gameObject);
        }
    }
}
