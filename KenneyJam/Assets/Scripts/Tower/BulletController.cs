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
        transform.localRotation = Quaternion.FromToRotation(transform.localRotation.eulerAngles, shootDir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO:’®ªŸµÿøÈ
        if (collision.gameObject.name == "Tilemap" && canExcavate)
        {

            transform.parent.GetComponent<BulletManager>().PoolPush(gameObject);
        }
        //TODO:‘Ï≥……À∫¶
        if (collision.gameObject.name == "Enemy")
        {

            transform.parent.GetComponent<BulletManager>().PoolPush(gameObject);
        }
    }
}
