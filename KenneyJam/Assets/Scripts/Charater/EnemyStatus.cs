using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人状态类， 负责提供敌人的信息，暂时额外进行受击反馈
/// 只有敌人挂
/// </summary>

public class EnemyStatus : MonoBehaviour
{
    [Tooltip("敌人最大生命值")]
    public int maxHP = 100;
    [Tooltip("敌人当前生命值")]
    public int currentHP = 80;
    [Tooltip("敌人攻击力")]
    public int ATK = 1;

    //public float knockbackForce = 2;
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
