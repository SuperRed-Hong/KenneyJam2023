using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
/// <summary>
/// ������ԣ������ṩ��ҵ���Ϣ����ʱ��������ܻ�����
/// ���ֻ��Ҫ������࣬���Զ�����������������ࡣ
/// </summary> 
[RequireComponent(typeof(CharacterController))]//�󶨽�ɫ�ƶ�
[RequireComponent(typeof(CharacterExcavate))]//���ھ�
public class PlayerStatus : MonoBehaviour
{
    [Tooltip("�ݵ��Transform���")]
    public Transform HomeTF;
    [Tooltip("����������")]
    public CharacterAnimatorParam chParam;
    private CharacterController chController;
    private CharacterExcavate chExcavate;

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
    [Tooltip("���������")]
    public float ATKInterval = 0.5f;
    [Tooltip("����ʱ��")]
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
