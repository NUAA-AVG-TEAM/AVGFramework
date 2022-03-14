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
        // ����OnShow
        yield return CoroutineManager.GetInstance().StartCoroutine(base.OnShow());
        Debug.Log("123123");
        // �󶨰�ť�¼�
        if (panelObj != null)
        {
            GetOrAddComponetInChild<Button>("BtnStart").onClick.AddListener(() =>
            {
                Debug.Log("BtnStart is clicked");
                // �л� GamingGUI
                /// ����ǰ״̬�µ� panel ȫ��POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new StartPanel()));
                UIManager.GetInstance.GetSMachine.ChangeState("GamingGUI");
            });

            GetOrAddComponetInChild<Button>("BtnLoad").onClick.AddListener(() =>
            {
                Debug.Log("BtnLoad is clicked");
                // �л� GamingGUI
                /// ����ǰ״̬�µ� panel ȫ��POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new LoadPanel()));
                UIManager.GetInstance.GetSMachine.ChangeState("SaveLoadGUI");
            });

            GetOrAddComponetInChild<Button>("BtnAppreciate").onClick.AddListener(() =>
            {
                Debug.Log("BtnAppreciate is clicked");
                // �л� AppreciateGUI
                /// ����ǰ״̬�µ� panel ȫ��POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
                //CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new AppreciatePanel()));
                UIManager.GetInstance.GetSMachine.ChangeState("AppreciateGUI");
            });

            GetOrAddComponetInChild<Button>("BtnSystem").onClick.AddListener(() =>
            {
                Debug.Log("BtnSystem is clicked");
                // �л� SettingGUI
                /// ����ǰ״̬�µ� panel ȫ��POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new SystemPanel()));
                UIManager.GetInstance.GetSMachine.ChangeState("SettingGUI");
            });

            GetOrAddComponetInChild<Button>("BtnExit").onClick.AddListener(() =>
            {
                Debug.Log("BtnExit is clicked");

                #if UNITY_EDITOR    //�ڱ༭��ģʽ��
                EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
            });
        }
    }
}
