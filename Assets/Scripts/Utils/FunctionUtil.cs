using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

// 函数工具类脚本
public class FunctionUtil : MonoBehaviour
{
    // 传入一个节点的Transform,删除其下所有子节点~
    public static void RemoveChildren(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            Destroy(t.GetChild(i).gameObject);
        }
            
    }


    // cv的，深拷贝一个类对象
    // 定义为static就不用实例化那个类了，ctm

    public static T DeepCopyByReflection<T>(T obj)
    {
        if (obj is string || obj.GetType().IsValueType)
            return obj;

        object retval = System.Activator.CreateInstance(obj.GetType());
        FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
        foreach (var field in fields)
        {
            try
            {
                field.SetValue(retval, DeepCopyByReflection(field.GetValue(obj)));
            }
            catch { }
        }

        return (T)retval;
    }
}
