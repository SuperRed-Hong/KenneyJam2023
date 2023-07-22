using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人状态类
/// </summary>
public class EnemyStatus : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 80;
    public int ATK = 1;
    public float knockbackForce = 2;
    private Rigidbody2D rb;
    private PlayerStatus plaStatus;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon" || collision.tag =="")
        {
            print("打中了");
            plaStatus = collision.GetComponentInParent<PlayerStatus>();
            currentHP -= plaStatus.ATK;
            print(currentHP);
        }
    }
}
