using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ½ÇÉ«µÄÍÚ¾òÀà
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
        if (status.currentWater >= 0)
        {
            status.currentWater--;
            anim.SetBool(status.chParam.Excavate, true);
            //if (status.ATK < )
            //{

            //}
        }
    }
}
