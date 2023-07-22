using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家属性
/// </summary> 
public class PlayerStatus : MonoBehaviour
{
    public CharacterAnimatorParam chParam;

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
    



}
