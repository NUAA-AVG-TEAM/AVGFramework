using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppreciateGUI : MonoBehaviour
{
    private static AppreciateGUI instance;

    public static AppreciateGUI GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// UIManager״̬���л�����UIʱ����
    /// </summary>
    public void OnEnter()
    {

    }

    /// <summary>
    /// �Ӹ�UI�л�����һ��UIʱ����
    /// </summary>
    public void OnLeave()
    {

    }
}
