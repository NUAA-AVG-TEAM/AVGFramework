using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDisplayManagement;
using UnityEngine.UI;
using CoroutineManagement;

public class AppreciatePanel : BasePanel
{
/* DATA */
    
    // panel Ԥ����λ��
    public AppreciatePanel() : base("Prefabs/AppreciateGUIPart/AppreciatePanel") { }
    // CG ����
    /// CG dic
    /// ...
    /// CG : ��ǰ���ڵ� CG ҳ�� , 0 ��ʾ��δ����
    private int nowCGPage = 0;
    /// CG : PageCtrlButton Ԥ����λ��
    private string pageCtrlButtonPath = "Prefabs/UI/EButton/BtnPageCtrl";
    /// CG : CGButton Ԥ����λ��
    private string cgButtonPath = "Prefabs/UI/EButton/BtnCG";

    // MUSIC����
    /// Music dic
    /// ...
    /// ��ǰ���ŵ� music id , 0 ��ʾ��ǰδ�ڲ���
    /// int nowMusicID = 0;
    /// MusicButton Ԥ����λ��
    private string musicButtonPath = "Prefabs/UI/EButton/BtnMusic";


/* LOGIC */
    /// <summary>
    /// ���� ��panel ʱ���߼�
    /// </summary>
    /// <returns></returns>
    sealed public override IEnumerator OnShow()
    {
        // �ȹرյ��
        GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
        GetOrAddComponent<CanvasGroup>().alpha = 0f;

        // �˳���ť
        if (panelObj != null)
        {
            GetOrAddComponetInChild<Button>("BtnExit").onClick.AddListener(() =>
            {
                Debug.Log("BtnExit is clicked");
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
            });
        }

        // ��ʼ�� CG
        InitCG();

        // ��ʼ�� Music
        InitMusic();

        // ����Onshow
        yield return CoroutineManager.GetInstance().StartCoroutine(base.OnShow());
    }

    /// <summary>
    /// ��ʼ�� CG ����
    /// </summary>
    private void InitCG()
    {
        /// ��ȡ CG dictionary
        int cgNums = 15;

        // ��̬���� PageControl
        /// ��ȡ PageCtrlTable
        GameObject pageCtrlTable = FindFirstChildGameObject("PageCtrlTable");
        /// ��ȡ ����
        /// ...
        /// ����
        if (pageCtrlTable != null)
        {
            for (int i = 1; i < cgNums / 9 + 2; i++)
            {
                GameObject newBtnPageCtrl = GameObject.Instantiate(Resources.Load<GameObject>(pageCtrlButtonPath), pageCtrlTable.transform);
                // ��Ҫ��һЩ��ʼ������
                newBtnPageCtrl.GetComponentInChildren<Text>().text = $"page \n {i}";
                // ��Ӱ�ť����¼� , ������� ��� ���ӳ٣� ֱ���� I �����ж��� ���� I   
                int tmpPage = i;
                newBtnPageCtrl.GetComponent<Button>().onClick.AddListener(() =>
                {
                    // �������ÿ�� pageCtrl ��ť�ĵ���߼�
                    Debug.Log("pageCtrl");
                    // ���ʱӦ�ô���ҳ��
                    OpenCG(tmpPage);
                    // ClearCG();
                });
            }
        }
        
        // ��̬���� CG ebutton
        /// Ĭ�����ɵ�һҳ
        OpenCG(1);
    }

    /// <summary>
    /// ��ָ��ҳ���CG
    /// </summary>
    /// <param name="page"></param>
    private void OpenCG(int page)
    {
        // �ж��Ƿ���Ҫ��������
        if (page == nowCGPage)
        {
            return;
        }
        // ���
        ClearCG();

        // ��ȡ CGTable
        GameObject cgTable = FindFirstChildGameObject("CGTable");
        int cgNum = 15;
        // ��̬���� CGButton
        if (cgTable != null)
        {
            for ( int i = 1 , id = (page-1)*9+1 ; i<= 9&&id <= cgNum; i++ , id++ )
            {
                GameObject newBtnCG = GameObject.Instantiate(Resources.Load<GameObject>(cgButtonPath), cgTable.transform);
                // ��Ҫ��һЩ��ʼ������
                /// ��Ӱ�ť����¼�
                newBtnCG.GetComponent<Button>().onClick.AddListener(() =>
                {
                    // �������ÿ�� CG ��ť�ĵ���߼�
                    Debug.Log("CG");
                });
                /// ������
                newBtnCG.GetComponentInChildren<Text>().text = $"id : {id}";
            }
        }   

        nowCGPage = page;
    }

    /// <summary>
    /// �����ǰ ������ʾ��CG
    /// </summary>
    private void ClearCG()
    {
        // ��ȡ CGTable
        GameObject cgTable = FindFirstChildGameObject("CGTable");
        // ��� CGButton
        Debug.Log(cgTable.transform.childCount);
        if (cgTable != null && cgTable.transform.childCount > 0)
        {
            for (int i = 0; i < cgTable.transform.childCount; i++)
            {
                GameObject.Destroy(cgTable.transform.GetChild(i).gameObject);
            }
        }
    }


    /// <summary>
    /// ��ʼ�� Music ����
    /// </summary>
    private void InitMusic()
    {
        // ��̬���� music ebutton
        /// ��ȡ table
        GameObject musicTable = FindFirstChildGameObject("MusicTable");
        /// ��ȡ���� �� �������ĳ� dic.count
        int musicNums = 50;
        /// ����
        if (musicTable != null)
        {
            for (int i = 0; i < musicNums; i++)
            {
                GameObject newMusicButton = GameObject.Instantiate(Resources.Load<GameObject>(musicButtonPath), musicTable.transform);
                // ������Ҫ��һЩ��ʼ������
                newMusicButton.GetComponentInChildren<Text>().text = $"Music ID : {i}";
                // �󶨵���¼�
                newMusicButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    // �������ÿ�� MusicButton ��ִ���߼�
                    Debug.Log("PlayMusic");
                });
            }
        }
    }
}