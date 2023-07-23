using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��|实际生成敌人的数�|生成敌人的最大的随机�?|生成敌人的最小的随机�?|敌人的预制件
/// ��Ϸ������
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    [Tooltip("���˵�Ԥ�Ƽ�")]
    public GameObject enemyPre;
    [Tooltip("��С���ɵĵ��������")]
    public int minCreate = 1;
    [Tooltip("������ɵĵ��������")]
    public int maxCreate = 10;
    [Tooltip("ʵ�ʻ�������ɳ��ĵ��ˣ��㷨���㲻�ø�")]
    private int createnum;
    [SerializeField]
    private Transform testTF;
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="coordinate">���˵����ɵ�</param>
    public void CreateEnemy(Vector2 coordinate)
    {
        createnum = Random.Range(minCreate, maxCreate);
        for (int i = 0; i < createnum; i++)
        {
            Instantiate(enemyPre, coordinate, Quaternion.identity);
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("���ɵ���"))
        {
            CreateEnemy(new Vector2(testTF.position.x, testTF.position.z));
        }
        
    }
   
    
}
