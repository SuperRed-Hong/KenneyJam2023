using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject Gun;

    public Transform shootTransform;

    public GameObject atkRange;

    public BulletManager bulletManager;

    public Sprite[] towerSprites;

    public Sprite[] gunSprites;

    private int Level;
    public int level
    {
        set
        {
            Level = value;
            if(level == 1)
            {
                atkRadius = 5;
                reloadTime = 1.0f;
                bulletType = BulletManager.bulletType.normal;
                GetComponent<SpriteRenderer>().sprite = towerSprites[0];
                Gun.GetComponent<SpriteRenderer>().sprite = gunSprites[0];
            }
            if (level == 2)
            {
                atkRadius = 7;
                reloadTime = 1.2f;
                bulletType = BulletManager.bulletType.heavy;
                GetComponent<SpriteRenderer>().sprite = towerSprites[1];
                Gun.GetComponent<SpriteRenderer>().sprite = gunSprites[1];
            }
            if (level == 3)
            {
                atkRadius = 9;
                reloadTime = 0.8f;
                bulletType = BulletManager.bulletType.fast;
                GetComponent<SpriteRenderer>().sprite = towerSprites[2];
                Gun.GetComponent<SpriteRenderer>().sprite = gunSprites[2];
            }
            atkRange.GetComponent<Transform>().localScale = new(atkRadius * 0.5f, atkRadius * 0.5f, atkRadius * 0.5f);
        }
        get
        {
            return Level;
        }
    }

    private int atkRadius;

    private float reloadTime;

    private BulletManager.bulletType bulletType;

    public List<GameObject> enemyInDetection;

    private Vector3 targetDir;

    private bool powerOn;

    private bool readyToShoot;

    private void Start()
    {
        // OnTest
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            level = 1; 
        if (Input.GetKeyDown(KeyCode.Alpha2))
            level = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            level = 3;
    }

    private void FixedUpdate()
    {
        if (powerOn)
        {
            if (enemyInDetection.Count == 0) return;
            if (enemyInDetection[0] == null)
            {
                enemyInDetection.RemoveAt(0);
                return;
            }
            targetDir = enemyInDetection[0].transform.position - transform.position;
            if (enemyInDetection.Count > 1)
            {
                List<int> removeId = new();
                for (int i = 1; i < enemyInDetection.Count; i++)
                {
                    if(enemyInDetection[i] == null)
                    {
                        removeId.Add(i);
                        continue;
                    }
                    Vector3 distance = enemyInDetection[i].transform.position - transform.position;
                    if (distance.x <  targetDir.x)
                        targetDir = distance;
                }
                if(removeId.Count != 0)
                {
                    foreach (var r in removeId)
                        enemyInDetection.RemoveAt(r);
                }
            }
            Gun.transform.Rotate(Quaternion.FromToRotation((shootTransform.position
                - Gun.transform.position).normalized, targetDir.normalized).eulerAngles);
            Shoot();
        }
    }


    private IEnumerator Reload()
    {
        yield return new WaitForSecondsRealtime(reloadTime);
        readyToShoot = true;
    }

    private void Shoot()
    {
        if (!readyToShoot) return;
        bulletManager.PoolPop(shootTransform.position, 
        (shootTransform.position - Gun.transform.position).normalized, bulletType);
        readyToShoot = false;
        StartCoroutine(nameof(Reload));
    }

    public void Initialize()
    {
        level = 1;
        bulletManager = GameObject.Find("BulletParent").GetComponent<BulletManager>();
        enemyInDetection = new List<GameObject>();
        PowerOn();
        readyToShoot = false;
        StartCoroutine(nameof(Reload));
    }

    public void PowerOn()
    {
        powerOn = true;
    }

    public void PowerOff()
    {
        powerOn = false;
    }
}
