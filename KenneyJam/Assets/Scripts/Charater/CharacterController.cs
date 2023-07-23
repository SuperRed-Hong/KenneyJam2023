using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
/// <summary>
/// 玩家控制类
/// </summary>
public class CharacterController : MonoBehaviour
{
    
    private float hor;
    private float ver;
    [Tooltip("玩家移动速度")]
    public int _SpeedMove = 2;
    public Vector3 direction;

    public Vector2 inputDirection = new Vector2();
    public PlayerAnimation PlayerAnimation;
    public bool right;
    public bool left;
    public bool up;
    public bool down;

    public void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        if (hor != 0 || ver != 0)
        {
            inputDirection.x = hor;
            inputDirection.y = ver;
            Movement(hor, ver);
        }
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
    private void Movement(float hor,float ver)
    {
        transform.Translate(new Vector3(hor, ver, 0) * Time.deltaTime * _SpeedMove);
    }
    
}
