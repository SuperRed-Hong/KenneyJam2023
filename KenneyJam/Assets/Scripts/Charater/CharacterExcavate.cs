using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色的挖掘类，负责提供挖掘功能。提出来，以方便扩展
/// </summary>
public class CharacterExcavate : MonoBehaviour
{
    private PlayerStatus status;
    private Animator anim;
    private void Awake()
    {
        status = GetComponent<PlayerStatus>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Excavate()
    {
        if (status.currentWater > 0)
        {
            status.currentWater--;
            //anim.SetBool(status.chParam.Excavate, true);
            status.money++;
            //if (status.ATK < )
            //{

            //}
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Excavate();
    }
}
