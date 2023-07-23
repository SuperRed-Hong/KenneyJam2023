using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTitle : MonoBehaviour
{
    public Vector3 trans1;//��¼ԭλ��
    Vector2 trans2;//��г�˶��仯��λ�ã�����ó�

    public float zhenFu = 10f;//���
    public float HZ = 1f;//Ƶ��

    public bool aliveWhilePause = false;
    public bool axisY = true;

    private void Awake()
    {
        if (!aliveWhilePause) trans1 = transform.position;
    }

    private void Update()
    {
        if (aliveWhilePause)
        {
            trans2 = trans1;
            if (axisY) trans2.y = Mathf.Sin(Time.realtimeSinceStartup * Mathf.PI * HZ) * zhenFu + trans1.y;
            else trans2.x = Mathf.Sin(Time.realtimeSinceStartup * Mathf.PI * HZ) * zhenFu + trans1.x;
            transform.localPosition = trans2;
        }
        else
        {
            trans2 = trans1;
            if (axisY) trans2.y = Mathf.Sin(Time.fixedTime * Mathf.PI * HZ) * zhenFu + trans1.y;
            else trans2.x = Mathf.Sin(Time.fixedTime * Mathf.PI * HZ) * zhenFu + trans1.x;
            transform.position = trans2;
        }
        /*
        if (Time.timeScale == 0 && !aliveWhilePause) return;
        trans2 = trans1;
        if(axisY) trans2.y = Mathf.Sin(Time.realtimeSinceStartup * Mathf.PI * HZ) * zhenFu + trans1.y;
        else trans2.x = Mathf.Sin(Time.realtimeSinceStartup * Mathf.PI * HZ) * zhenFu + trans1.x;
        if(aliveWhilePause) transform.localPosition = trans2;
        else transform.position = trans2;
        */
    }
}
