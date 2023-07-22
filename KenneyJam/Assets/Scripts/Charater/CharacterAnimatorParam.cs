using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动画参数类，负责存储动画的参数名字，以方便代码直接变量调用和修改。
/// 
/// </summary>

[System.Serializable]
public class CharacterAnimatorParam 
{
    [Tooltip("挖掘动画名字")]
    public string Excavate = "Excavate";
    [Tooltip("跑步动画名字")]
    public string Run = "Run";
    [Tooltip("攻击动画名字")]
    public string Attact = "Attact";
}
