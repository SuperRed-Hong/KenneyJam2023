using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����״̬�࣬ �����ṩ���˵���Ϣ����ʱ��������ܻ�����
/// ֻ�е��˹�
/// </summary>

public class EnemyStatus : MonoBehaviour
{
    [Tooltip("�����������ֵ")]
    public int maxHP = 100;
    [Tooltip("���˵�ǰ����ֵ")]
    public int currentHP = 80;
    [Tooltip("���˹�����")]
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
        if (collision.tag == "Weapon" )
        {
            //print("������");
            plaStatus = collision.GetComponentInParent<PlayerStatus>();
            currentHP -= plaStatus.ATK;
            print(currentHP);
            if (currentHP <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (collision.tag == "Bullet")
        {
            collision.GetComponent<BulletController>().Destory();
            currentHP -= collision.GetComponent<BulletController>().damage;
            print(currentHP);
            if (currentHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
