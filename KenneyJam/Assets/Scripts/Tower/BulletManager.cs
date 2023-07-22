using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private static List<GameObject> poolList;

    private readonly int PoolCapacity = 100;

    private GameObject lastBullet;

    public Sprite[] bulletSprites;

    public enum bulletType
    {
        normal,
        heavy,
        fast
    }

    private void Awake()
    {
        PoolInitialize();
    }

    public void PoolInitialize()
    {
        poolList = new(PoolCapacity);
    }

    public void PoolPush(GameObject bullet)
    {
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        bullet.SetActive(false);
        if (poolList.Count < PoolCapacity) poolList.Add(bullet);
        else Destroy(bullet);
    }

    public void PoolPop(Vector3 BirthPos, Vector3 shootDir, bulletType bulletType)
    {
        if (poolList.Count > 0)
        {
            lastBullet = poolList[0];
            poolList.RemoveAt(0);
            lastBullet.transform.position = BirthPos;
            lastBullet.SetActive(true);
        }
        else
        {
            lastBullet = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"), BirthPos, Quaternion.identity, transform);
        }
        switch (bulletType)
        {
            case bulletType.normal:
                lastBullet.GetComponent<BulletController>().Initialize(50, 4.0f, false, shootDir);
                lastBullet.GetComponent<SpriteRenderer>().sprite = bulletSprites[0];
                break;
            case bulletType.heavy:
                lastBullet.GetComponent<BulletController>().Initialize(200, 3.0f, true, shootDir);
                lastBullet.GetComponent<SpriteRenderer>().sprite = bulletSprites[1];
                break;
            case bulletType.fast:
                lastBullet.GetComponent<BulletController>().Initialize(100, 5.0f, false, shootDir);
                lastBullet.GetComponent<SpriteRenderer>().sprite = bulletSprites[2];
                break;
        }
    }

    public void PoolClear()
    {
        poolList.Clear();
    }
}
