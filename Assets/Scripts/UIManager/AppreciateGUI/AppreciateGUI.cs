using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoroutineManagement;
using PanelDisplayManagement;

public class AppreciateGUI : MonoBehaviour
{
    private static AppreciateGUI instance;
    private static string targetGUIName = "AppreciateGUI";

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
        // �� GUI
        instance.gameObject.SetActive(true);

        // �����ʼ�� panel
        PanelDisplayManager.GetInstance().SetTartGUI(instance.gameObject);
        CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new AppreciatePanel()));
    }

    /// <summary>
    /// �Ӹ�UI�л�����һ��UIʱ����
    /// </summary>
    public void OnLeave()
    {
        // GUI�ӵ�ǰ����
        PanelDisplayManager.GetInstance().SetTartGUI(null);

        // �� GUI
        instance.gameObject.SetActive(false);
    }
}
