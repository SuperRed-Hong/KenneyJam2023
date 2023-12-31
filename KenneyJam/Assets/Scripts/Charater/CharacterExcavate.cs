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
    public float countTime;
    public int countnum;
    public int ExcavatePerS = 3;
    public float ExcavateInterval = 0.5f;
    private void Awake()
    {
        status = GetComponentInParent<PlayerStatus>();
        anim = GetComponentInParent<Animator>();
        GameObject go = GameObject.FindGameObjectWithTag("TilemapManager");
        mapcol = go.GetComponent<MapController>();
    }
    private void Excavate(Collision2D col)
    {
        if (mapcol.GetBlockType(col.GetContact(0).point) == MapCellType.rock ||
            mapcol.GetBlockType(col.GetContact(0).point) == MapCellType.mineral1||
            mapcol.GetBlockType(col.GetContact(0).point) == MapCellType.mineral2||
            mapcol.GetBlockType(col.GetContact(0).point) == MapCellType.mineral3)
        {

            if (status.currentWater > 0)
            {
                status.currentWater--;
                switch (mapcol.GetBlockType(col.GetContact(0).point))
                {
                    case MapCellType.mineral1:
                        status.money += 50;
                        break;
                    case MapCellType.mineral2:
                        status.money += 100;
                        break;
                    case MapCellType.mineral3:
                        status.money += 500;
                        break;
                }
                mapcol.DestroyCell(col.GetContact(0).point);
                countnum++;

            }

        }
        //Debug.Log(mapcol.GetBlockType(col.GetContact(0).point));
        //Debug.Log(col.GetContact(0).point);
    }
    private void Update()
    {
        countTime += Time.deltaTime;
        if (countTime >= 1)
        {
            countnum = 0;
            countTime = 0;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Excavate(collision);
    //}
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (ExcavatePerS > countnum)
        {
            Excavate(collision);
        }

    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    Excavate(collision);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{

    //}
}
