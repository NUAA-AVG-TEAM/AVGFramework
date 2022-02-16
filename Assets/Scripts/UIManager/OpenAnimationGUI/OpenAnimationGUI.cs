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
        instance.gameObject.SetActive(true);
        PanelDisplayManager.GetInstance().SetTartGUI(instance.gameObject);
        CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new OpenAnimationPanel()));
    }

    /// <summary>
    /// 从该UI切换到另一个UI时触发
    /// </summary>
    public void OnLeave()
    {
        PanelDisplayManager.GetInstance().PopAll();
        PanelDisplayManager.GetInstance().SetTartGUI(null);

        CoroutineManager.GetInstance().StopAllCoroutine();
        instance.gameObject.SetActive(false);
    }


}
