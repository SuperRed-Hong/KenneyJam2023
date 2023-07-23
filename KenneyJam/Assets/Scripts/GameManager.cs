using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏管理类
/// </summary>
public class GameManager : MonoBehaviour
{
    [Tooltip("敌人的预制件")]
    public GameObject enemyPre;
    [Tooltip("生成敌人的最小的随机值")]
    public int minCreate = 1;
    [Tooltip("生成敌人的最大的随机值")]
    public int maxCreate = 10;
    [Tooltip("实际生成敌人的数量")]
    private int createnum;
    [SerializeField]
    private Transform testTF;
    /// <summary>
    /// 生成敌人
    /// </summary>
    /// <param name="coordinate">敌人的生成点</param>
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
        if (GUILayout.Button("生成敌人"))
        {
            CreateEnemy(new Vector2(testTF.position.x, testTF.position.z));
        }
        
    }
   
    
}
