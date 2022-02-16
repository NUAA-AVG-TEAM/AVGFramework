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
                
                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new StartPanel()));
            });

            GetOrAddComponetInChild<Button>("BtnLoad").onClick.AddListener(() =>
            {
                Debug.Log("BtnLoad is clicked");
                
                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new LoadPanel()));
            });

            GetOrAddComponetInChild<Button>("BtnAppreciate").onClick.AddListener(() =>
            {
                Debug.Log("BtnAppreciate is clicked");
                
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new AppreciatePanel()));
            });

            GetOrAddComponetInChild<Button>("BtnSystem").onClick.AddListener(() =>
            {
                Debug.Log("BtnSystem is clicked");

                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new SystemPanel()));
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
