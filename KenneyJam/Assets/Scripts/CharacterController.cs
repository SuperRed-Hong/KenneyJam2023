using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ҿ�����
/// </summary>
public class CharacterController : MonoBehaviour
{
    private float hor;
    private float ver;
    [Tooltip("����ƶ��ٶ�")]
    public int _SpeedMove = 2;

    public Vector2 inputDirection = new Vector2();


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
    }

    private void Movement(float hor,float ver)
    {
        transform.Translate(new Vector3(hor, ver, 0) * Time.deltaTime * _SpeedMove);
    }
}
