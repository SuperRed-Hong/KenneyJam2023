using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
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
