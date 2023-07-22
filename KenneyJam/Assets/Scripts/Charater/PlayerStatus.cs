using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �������
/// </summary> 
public class PlayerStatus : MonoBehaviour
{
    public CharacterAnimatorParam chParam;

    [Tooltip("��ǰѪ��ֵ")]
    public int currentHP = 100;
    [Tooltip("���Ѫ��ֵ")]
    public int maxHP = 100;
    [Tooltip("Ǯ")]
    public int money = 0;
    [Tooltip("��ǰˮ��Դ")]
    public int currentWater = 20;
    [Tooltip("���ˮ��Դ")]
    public int maxWater = 20;
    [Tooltip("������")]
    public int ATK = 1;

    public float ATKInterval = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            currentHP--;
            print(currentHP);
        }
    }




}
