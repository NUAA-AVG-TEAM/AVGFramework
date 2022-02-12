using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ����������ű�
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


    // ����һ���ڵ��Transform,ɾ�����������ӽڵ�~
    public void RemoveChildren(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            Destroy(t.GetChild(i).gameObject);
        }
            
    }
}
