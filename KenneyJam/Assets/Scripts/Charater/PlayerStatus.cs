using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
/// <summary>
/// 玩家属性，负责提供玩家的信息，暂时额外进行受击反馈
/// 玩家只需要挂这个类，会自动挂载其他玩家所需类。
/// </summary> 
[RequireComponent(typeof(CharacterController))]//绑定角色移动
[RequireComponent(typeof(CharacterExcavate))]//绑定挖掘
public class PlayerStatus : MonoBehaviour
{
    [Tooltip("据点的Transform组件")]
    public Transform HomeTF;
    [Tooltip("动画参数类")]
    public CharacterAnimatorParam chParam;
    private CharacterController chController;
    private CharacterExcavate chExcavate;

    [Tooltip("当前血量值")]
    public int currentHP = 100;
    [Tooltip("最大血量值")]
    public int maxHP = 100;
    [Tooltip("钱")]
    public int money = 0;
    [Tooltip("当前水资源")]
    public int currentWater = 20;
    [Tooltip("最大水资源")]
    public int maxWater = 20;
    [Tooltip("攻击力")]
    public int ATK = 1;
    [Tooltip("攻击力间隔")]
    public float ATKInterval = 0.5f;
    [Tooltip("复活时间")]
    public float reviveTime = 2f;

    private void Awake()
    {
        chController = GetComponent<CharacterController>();
        chExcavate = GetComponent<CharacterExcavate>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            currentHP--;
            print(currentHP);
            if (currentHP <= 0)
            {
                Death();
            }
        }
    }
    private void Death()
    {
        gameObject.SetActive(false);
        Invoke("revive", reviveTime);

    }
    private void revive()
    {
        gameObject.SetActive(true);
        currentHP = maxHP;
    }
    public void SetMaxWater()
    {
        currentWater = maxWater;
    }
}
