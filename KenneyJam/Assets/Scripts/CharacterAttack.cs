using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class CharacterAttack : MonoBehaviour
{
    private PlayerStatus status;
    private Animator anim;
    private void Awake()
    {
        status = GetComponent<PlayerStatus>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Attacking(int hp,int hurt)
    {
        hp -= hurt;
    }
}
