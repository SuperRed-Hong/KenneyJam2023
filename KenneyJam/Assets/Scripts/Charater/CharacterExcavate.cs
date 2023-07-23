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
    private MapController mapcol;
    private void Awake()
    {
        status = GetComponentInParent<PlayerStatus>();
        anim = GetComponentInParent<Animator>();
        GameObject go = GameObject.FindGameObjectWithTag("MaManager");
        mapcol = go.GetComponent<MapController>();
    }
    private void Excavate(Collision2D col)
    {
        if (mapcol.GetBlockType(col.GetContact(0).point) == MapCellType.rock ||
            mapcol.GetBlockType(col.GetContact(0).point) == MapCellType.mineral)
        {

            if (status.currentWater > 0)
            {
                status.currentWater--;
                mapcol.DestroyCell(col.GetContact(0).point);
                switch (mapcol.GetBlockType(col.GetContact(0).point))
                {
                    case MapCellType.air:
                        status.money += 100;
                        break;
                }
            }

        }
        //Debug.Log(mapcol.GetBlockType(col.GetContact(0).point));
        //Debug.Log(col.GetContact(0).point);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Excavate(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Excavate(collision);
    }
}
