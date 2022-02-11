using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 函数工具类脚本
public class FunctionUtil : MonoBehaviour
{
    private static FunctionUtil instance;

    public static FunctionUtil GetInstance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }


    // 传入一个节点的Transform,删除其下所有子节点~
    public void RemoveChildren(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            Destroy(t.GetChild(i).gameObject);
        }
            
    }
}
