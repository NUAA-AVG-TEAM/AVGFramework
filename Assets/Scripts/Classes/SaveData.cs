using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @param index: 指令索引
/// @param bgID: 背景在Bg表的索引
/// @param sceneIndex: 场景名称在指令表里的索引
/// @param dialog: 当前对话
/// @param nowDate: 当前日期的格式化字符串形式
/// </summary>
public class SaveData
{
    public int index;
    public string bgID;
    public int sceneIndex;
    public string dialog;
    public string nowDate;  
}
