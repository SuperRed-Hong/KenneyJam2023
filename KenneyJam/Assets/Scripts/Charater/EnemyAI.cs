using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
/// <summary>
/// ����Ѱ·AI
/// ʹ�ô˽ű���Ҫ�ڳ����з���һ�������岢�������Pathfinder�ű����˽ű��ſ�������
/// �˽ű�ֻ��Ҫ���ڵ���������*********�Ѿ��Զ����ص�Ԥ�Ƽ�
/// 
/// ʹ��˵��
///     �����ڳ����и��ݵ�����Home��ǩ �����Զ���ȡ�ݵ����Ϸ����
/// </summary>
public class EnemyAI : MonoBehaviour
{
    [Tooltip("�ݵ�ı�ǩ")]
    public string HomeTag ="Home";

    [Tooltip("���������Transform���")]
    [SerializeField]
    private Transform Enemy;
    [Tooltip("�ݵ��Transform���")]
    [SerializeField]
    private Transform target;
    [Tooltip("���˵��ƶ��ٶ�")]
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
