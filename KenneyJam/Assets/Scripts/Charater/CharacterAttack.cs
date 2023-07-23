using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家攻击类，负责玩家的攻击。不过暂时没用，但先提出来，以方便扩展
/// 玩家的武器挂，由武器碰撞器检测和触发。
/// </summary>
public class CharacterAttack : MonoBehaviour
{
    private PlayerStatus status;
    public Animator anim;
    private EnemyStatus enemystatus;
    private float attackCountdown;
    private float fire;
    private Vector3 direction;
    private CharacterController characterController;
    [SerializeField]
    private string EnemyTag;
    private void Awake()
    {
        status = GetComponentInParent<PlayerStatus>();
        
        characterController = GetComponentInParent<CharacterController>();
    }
    private void Update()
    {
        attackCountdown += Time.deltaTime;
        fire = Input.GetAxis("Fire1");
        if (fire != 0 && attackCountdown > 0.5f)
        {
            Debug.Log("fire");
       

            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotz);
            anim.SetBool("attack", true);
            attackCountdown = 0;

        }
        else
        {

            //anim.SetBool(status.chParam.Attact, false);
            anim.SetBool("attack", false);

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
