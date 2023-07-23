using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
/// <summary>
/// 敌人寻路AI
/// 使用此脚本需要在场景中放置一个空物体并且添加上Pathfinder脚本，此脚本才可以运行
/// 此脚本只需要挂在到敌人身上*********已经自动挂载到预制件
/// 
/// 使用说明
///     必须在场景中给据点设置Home标签 用于自动获取据点的游戏对象
/// </summary>
public class EnemyAI : MonoBehaviour
{
    [Tooltip("据点的标签")]
    public string HomeTag ="Home";

    [Tooltip("敌人自身的Transform组件")]
    [SerializeField]
    private Transform Enemy;
    [Tooltip("据点的Transform组件")]
    [SerializeField]
    private Transform target;
    [Tooltip("敌人的移动速度")]
    public float speed = 20f;


    public float nextWaypointDistance = 3f;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndofPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;


    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponentInChildren<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);



        Enemy = GetComponentInChildren<Transform>();
        GameObject go = GameObject.FindGameObjectWithTag(HomeTag);
        target = go.GetComponent<Transform>();


    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathcomplete);
        }
    }

    private void OnPathcomplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

    }

    private void Update()
    {
        if (path == null) return;
        if (currentWaypoint >=path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else
        {
            reachedEndofPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance <nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (rb.velocity.x >=0.01f)
        {
            Enemy.localScale = new Vector3(-Enemy.localScale.x, Enemy.localScale.y, Enemy.localScale.z);
        }
        else if(rb.velocity.x <= -0.01f)
        {
            Enemy.localScale = new Vector3(Mathf.Abs(Enemy.localScale.x), Enemy.localScale.y, Enemy.localScale.z);
        }

    }
}
