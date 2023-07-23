using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 锟斤拷|瀹為檯鐢熸垚鏁屼汉鐨勬暟閲|鐢熸垚鏁屼汉鐨勬渶澶х殑闅忔満鍊?|鐢熸垚鏁屼汉鐨勬渶灏忕殑闅忔満鍊?|鏁屼汉鐨勯鍒朵欢
/// 游戏管理类
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    [Tooltip("敌人的预制件")]
    public GameObject enemyPre;
    [Tooltip("最小生成的敌人随机数")]
    public int minCreate = 1;
    [Tooltip("最大生成的敌人随机数")]
    public int maxCreate = 10;
    [Tooltip("实际会随机生成出的敌人，算法计算不用改")]
    private int createnum;
    [SerializeField]
    private Transform testTF;
    /// <summary>
    /// 创造敌人
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
