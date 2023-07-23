using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Ϸ������
/// </summary>
public class GameManager : MonoBehaviour
{
    [Tooltip("���˵�Ԥ�Ƽ�")]
    public GameObject enemyPre;
    [Tooltip("���ɵ��˵���С�����ֵ")]
    public int minCreate = 1;
    [Tooltip("���ɵ��˵��������ֵ")]
    public int maxCreate = 10;
    [Tooltip("ʵ�����ɵ��˵�����")]
    private int createnum;
    [SerializeField]
    private Transform testTF;
    /// <summary>
    /// ���ɵ���
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
