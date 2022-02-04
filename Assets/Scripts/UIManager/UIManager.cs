using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static UIManager instance;
    private static StateMachine sMachine;

    public static UIManager GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;

        // 添加状态
        sMachine.AddState("OpenAnimationGUI", OpenAnimationGUI.GetInstance.OnEnter, OpenAnimationGUI.GetInstance.OnLeave);
        sMachine.AddState("MainGUI", MainGUI.GetInstance.OnEnter, OpenAnimationGUI.GetInstance.OnLeave);
        sMachine.AddState("SaveLoadGUI", SaveLoadGUI.GetInstance.OnEnter, OpenAnimationGUI.GetInstance.OnLeave);
        sMachine.AddState("GamingGUI", GamingGUI.GetInstance.OnEnter, OpenAnimationGUI.GetInstance.OnLeave);
        sMachine.AddState("SettingGUI", SettingGUI.GetInstance.OnEnter, OpenAnimationGUI.GetInstance.OnLeave);
        sMachine.AddState("AppreciateGUI", AppreciateGUI.GetInstance.OnEnter, OpenAnimationGUI.GetInstance.OnLeave);
        
        // 设置默认状态
        // 注意要先添加状态再设置默认状态~
        sMachine.SetDefaultState("OpenAnimationGUI");
        

        
    }
    
    public StateMachine GetSMachine
    {
        get { return sMachine; }
    }
    void Start()
    {
        // 开启状态机
        sMachine.StartSM();

        // 若要改变状态，则先获取状态机 => UIManager.GetInstance.GetSMachine
        // 再执行状态机下的ChangeState函数 => UIManager.GetInstance.GetSMachine.ChangeState("xxx");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
