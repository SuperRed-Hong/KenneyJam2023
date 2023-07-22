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
    private EnemyStatus enemystatus;
    private float attackCountdown;
    private float fire;
    [SerializeField]
    private string EnemyTag;
    private void Awake()
    {
        status = GetComponentInParent<PlayerStatus>();
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        attackCountdown += Time.deltaTime;
        fire = Input.GetAxis("Fire1");
        if (fire != 0)
        {
            //anim.SetBool(status.chParam.Attact, false);
            print("123");
        }
        else
        {
            //anim.SetBool(status.chParam.Attact, false);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackCountdown <= 0 && collision.tag == EnemyTag)
        {
            attackCountdown = status.ATKInterval;
            enemystatus = collision.GetComponent<EnemyStatus>();
            enemystatus.currentHP -= status.ATK;
        }
    }
    

}
