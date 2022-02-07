using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingGUI : MonoBehaviour
{
    private static GamingGUI instance;
    private static StateMachine sMachine;
    public static GamingGUI GetInstance
    {
        get { return instance; }
    }

   
    /*
    private void Awake()
    {
        instance = this;

        // 初始化状态机：
        sMachine = new StateMachine("GamingGUI");
        sMachine.AddState("GamingPanel",GamingPanel.GetInstance.OnEnter, GamingPanel.GetInstance.OnLeave);
        sMachine.AddState("LogPanel", LogPanel.GetInstance.OnEnter, LogPanel.GetInstance.OnLeave);

        sMachine.SetDefaultState("GamingPanel");
                
    }
    */

    public GameObject GetUIInstance()
    {
        return instance.gameObject;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// UIManager状态机切换到该UI时触发
    /// </summary>
    public void OnEnter()
    {
        instance.gameObject.SetActive(true);
        // sMachine.StartSM();
        GamingPanel.GetInstance.OnEnter();
        
    }

    /// <summary>
    /// 从该UI切换到另一个UI时触发
    /// </summary>
    public void OnLeave()
    {
        GamingPanel.GetInstance.OnLeave();
        instance.gameObject.SetActive(false);
        // sMachine.StopSM();
    }


}
