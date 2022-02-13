using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// @params isFinish : 一周目是否通关
/// @params cgList : cg表
/// @params dialogList : 对话表
/// </summary>
public class GlobalData
{
    public bool isFinish = false;
    public HashSet<int> cgList;
    public Dictionary<string, int> dialogList;
    
}
