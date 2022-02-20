using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoroutineManagement;
using PanelDisplayManagement;

public class MainGUI : MonoBehaviour
{
    private static MainGUI instance;
    private static string targetGUIName = "MainGUI";

    public static MainGUI GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }
   
    /// <summary>
    /// UIManager状态机切换到该UI时触发
    /// </summary>
    public void OnEnter()
    {
        // GameObject.Find(targetGUIName).SetActive(true);
        instance.gameObject.SetActive(true);
        PanelDisplayManager.GetInstance().SetTartGUI(instance.gameObject);
        CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new MainPanel()));
    }

    /// <summary>
    /// 从该UI切换到另一个UI时触发
    /// </summary>
    public void OnLeave()
    {
        // GUI从当前切走
        PanelDisplayManager.GetInstance().SetTartGUI(null);

        // 关 GUI
        instance.gameObject.SetActive(false);
    }
}
