using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoroutineManagement;
using PanelDisplayManagement;

public class OpenAnimationGUI : MonoBehaviour
{
    private static OpenAnimationGUI instance;
    private static string targetGUIName = "OpenAnimationGUI";

    public static OpenAnimationGUI GetInstance
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
        // 开 GUI
        instance.gameObject.SetActive(true);

        // 加入初始的 panel
        PanelDisplayManager.GetInstance().SetTartGUI(instance.gameObject);
        CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new OpenAnimationPanel()));
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
