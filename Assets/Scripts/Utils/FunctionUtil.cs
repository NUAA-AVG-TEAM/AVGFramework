using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

// ����������ű�
public class FunctionUtil : MonoBehaviour
{
    // ����һ���ڵ��Transform,ɾ�����������ӽڵ�~
    public static void RemoveChildren(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            Destroy(t.GetChild(i).gameObject);
        }
            
    }


    // cv�ģ����һ�������
    // ����Ϊstatic�Ͳ���ʵ�����Ǹ����ˣ�ctm

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
