using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletManager.bulletType type { private set; get; }

    public int damage { get; private set; }

    private float speed;

    private bool canExcavate;

    public void Initialize(int damage, float speed, bool canExcavate, Vector3 shootDir)
    {
        this.damage = damage;
        this.speed = speed;
        this.canExcavate = canExcavate;
        GetComponent<Rigidbody2D>().velocity = shootDir * this.speed;
        transform.rotation = Quaternion.AngleAxis(AngleAroundAxis(Vector3.up, shootDir, Vector3.forward), Vector3.forward);
    }

    private float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis)
    {
        dirA = dirA - Vector3.Project(dirA, axis);

        dirB = dirB - Vector3.Project(dirB, axis);

        float angle = Vector3.Angle(dirA, dirB);

        return angle * (Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) < 0 ? -1 : 1);
    }

    public void Destory()
    {
        transform.parent.GetComponent<BulletManager>().PoolPush(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO:’®ªŸµÿøÈ
        if (collision.gameObject.layer == 6)
        {
            if (canExcavate)
            {
                //TODO
            }
            Destory();
        }
        //TODO:‘Ï≥……À∫¶
        //if (collision.gameObject.name == "Enemy")
        //{

        //    Destory();
        //}
    }
}
