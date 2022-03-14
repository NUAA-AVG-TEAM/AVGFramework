using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDisplayManagement;
using UnityEngine.UI;
using CoroutineManagement;
using UnityEditor;

public class MainPanel : BasePanel
{
    public MainPanel() : base("Prefabs/UI/Panel/MainPanel") { }

    sealed public override IEnumerator OnShow()
    {
        // 父类OnShow
        yield return CoroutineManager.GetInstance().StartCoroutine(base.OnShow());
        Debug.Log("123123");
        // 绑定按钮事件
        if (panelObj != null)
        {
            GetOrAddComponetInChild<Button>("BtnStart").onClick.AddListener(() =>
            {
                Debug.Log("BtnStart is clicked");
                // 切换 GamingGUI
                /// 将当前状态下的 panel 全部POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new StartPanel()));
                UIManager.GetInstance.GetSMachine.ChangeState("GamingGUI");
            });

            GetOrAddComponetInChild<Button>("BtnLoad").onClick.AddListener(() =>
            {
                Debug.Log("BtnLoad is clicked");
                // 切换 GamingGUI
                /// 将当前状态下的 panel 全部POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new LoadPanel()));
                UIManager.GetInstance.GetSMachine.ChangeState("SaveLoadGUI");
            });

            GetOrAddComponetInChild<Button>("BtnAppreciate").onClick.AddListener(() =>
            {
                Debug.Log("BtnAppreciate is clicked");
                // 切换 AppreciateGUI
                /// 将当前状态下的 panel 全部POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
                //CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new AppreciatePanel()));
                UIManager.GetInstance.GetSMachine.ChangeState("AppreciateGUI");
            });

            GetOrAddComponetInChild<Button>("BtnSystem").onClick.AddListener(() =>
            {
                Debug.Log("BtnSystem is clicked");
                // 切换 SettingGUI
                /// 将当前状态下的 panel 全部POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new SystemPanel()));
                UIManager.GetInstance.GetSMachine.ChangeState("SettingGUI");
            });

            GetOrAddComponetInChild<Button>("BtnExit").onClick.AddListener(() =>
            {
                Debug.Log("BtnExit is clicked");

                #if UNITY_EDITOR    //在编辑器模式下
                EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
            });
        }
    }
}
