using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ๆธธๆ็ฎก็็ฑ?
/// </summary>
public class GameManager : MonoBehaviour
{
    //[Tooltip("ๆไบบ็้ขๅถไปถ")]
    public GameObject enemyPre;
    //[Tooltip("็ๆๆไบบ็ๆๅฐ็้ๆบๅ?)]
    public int minCreate = 1;
    //[Tooltip("็ๆๆไบบ็ๆๅคง็้ๆบๅ?)]
    public int maxCreate = 10;
    //[Tooltip("ๅฎ้็ๆๆไบบ็ๆฐ้?)]
    private int createnum;
    [SerializeField]
    private Transform testTF;
    /// <summary>
    /// ็ๆๆไบบ
    /// </summary>
    /// <param name="coordinate">ๆไบบ็็ๆ็น</param>
    public void CreateEnemy(Vector2 coordinate)
    {
        createnum = Random.Range(minCreate, maxCreate);
        for (int i = 0; i < createnum; i++)
        {
            Instantiate(enemyPre, coordinate, Quaternion.identity).GetComponent<EnemyAI>().Initialize();
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("ษ๚ณษตะศห"))
        {
            CreateEnemy(new Vector2(testTF.position.x, testTF.position.z));
        }
        
    }
   
    
}
