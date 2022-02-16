using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDisplayManagement;
using UnityEngine.UI;
using CoroutineManagement;

public class AppreciatePanel : BasePanel
{
/* DATA */
    
    // panel 预制体位置
    public AppreciatePanel() : base("Prefabs/AppreciateGUIPart/AppreciatePanel") { }
    // CG 部分
    /// CG dic
    /// ...
    /// CG : 当前处于的 CG 页码 , 0 表示尚未生成
    private int nowCGPage = 0;
    /// CG : PageCtrlButton 预制体位置
    private string pageCtrlButtonPath = "Prefabs/UI/EButton/BtnPageCtrl";
    /// CG : CGButton 预制体位置
    private string cgButtonPath = "Prefabs/UI/EButton/BtnCG";

    // MUSIC部分
    /// Music dic
    /// ...
    /// 当前播放的 music id , 0 表示当前未在播放
    /// int nowMusicID = 0;
    /// MusicButton 预制体位置
    private string musicButtonPath = "Prefabs/UI/EButton/BtnMusic";


/* LOGIC */
    /// <summary>
    /// 进入 本panel 时的逻辑
    /// </summary>
    /// <returns></returns>
    sealed public override IEnumerator OnShow()
    {
        // 先关闭点击
        GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
        GetOrAddComponent<CanvasGroup>().alpha = 0f;

        // 退出按钮
        if (panelObj != null)
        {
            GetOrAddComponetInChild<Button>("BtnExit").onClick.AddListener(() =>
            {
                Debug.Log("BtnExit is clicked");
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
            });
        }

        // 初始化 CG
        InitCG();

        // 初始化 Music
        InitMusic();

        // 父类Onshow
        yield return CoroutineManager.GetInstance().StartCoroutine(base.OnShow());
    }

    /// <summary>
    /// 初始化 CG 部分
    /// </summary>
    private void InitCG()
    {
        /// 获取 CG dictionary
        int cgNums = 15;

        // 动态生成 PageControl
        /// 获取 PageCtrlTable
        GameObject pageCtrlTable = FindFirstChildGameObject("PageCtrlTable");
        /// 获取 数量
        /// ...
        /// 生成
        if (pageCtrlTable != null)
        {
            for (int i = 1; i < cgNums / 9 + 2; i++)
            {
                GameObject newBtnPageCtrl = GameObject.Instantiate(Resources.Load<GameObject>(pageCtrlButtonPath), pageCtrlTable.transform);
                // 需要做一些初始化工作
                newBtnPageCtrl.GetComponentInChildren<Text>().text = $"page \n {i}";
                // 添加按钮点击事件 , 这里估计 添加 有延迟？ 直接用 I 会所有都成 最大的 I   
                int tmpPage = i;
                newBtnPageCtrl.GetComponent<Button>().onClick.AddListener(() =>
                {
                    // 这里添加每个 pageCtrl 按钮的点击逻辑
                    Debug.Log("pageCtrl");
                    // 点击时应该打开新页面
                    OpenCG(tmpPage);
                    // ClearCG();
                });
            }
        }
        
        // 动态生成 CG ebutton
        /// 默认生成第一页
        OpenCG(1);
    }

    /// <summary>
    /// 打开指定页码的CG
    /// </summary>
    /// <param name="page"></param>
    private void OpenCG(int page)
    {
        // 判断是否需要重新生成
        if (page == nowCGPage)
        {
            return;
        }
        // 清除
        ClearCG();

        // 获取 CGTable
        GameObject cgTable = FindFirstChildGameObject("CGTable");
        int cgNum = 15;
        // 动态生成 CGButton
        if (cgTable != null)
        {
            for ( int i = 1 , id = (page-1)*9+1 ; i<= 9&&id <= cgNum; i++ , id++ )
            {
                GameObject newBtnCG = GameObject.Instantiate(Resources.Load<GameObject>(cgButtonPath), cgTable.transform);
                // 需要做一些初始化工作
                /// 添加按钮点击事件
                newBtnCG.GetComponent<Button>().onClick.AddListener(() =>
                {
                    // 这里添加每个 CG 按钮的点击逻辑
                    Debug.Log("CG");
                });
                /// 设置字
                newBtnCG.GetComponentInChildren<Text>().text = $"id : {id}";
            }
        }   

        nowCGPage = page;
    }

    /// <summary>
    /// 清除当前 所有显示的CG
    /// </summary>
    private void ClearCG()
    {
        // 获取 CGTable
        GameObject cgTable = FindFirstChildGameObject("CGTable");
        // 清除 CGButton
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
    /// 初始化 Music 部分
    /// </summary>
    private void InitMusic()
    {
        // 动态生成 music ebutton
        /// 获取 table
        GameObject musicTable = FindFirstChildGameObject("MusicTable");
        /// 获取数量 ， 这里后面改成 dic.count
        int musicNums = 50;
        /// 生成
        if (musicTable != null)
        {
            for (int i = 0; i < musicNums; i++)
            {
                GameObject newMusicButton = GameObject.Instantiate(Resources.Load<GameObject>(musicButtonPath), musicTable.transform);
                // 可能需要做一些初始化工作
                newMusicButton.GetComponentInChildren<Text>().text = $"Music ID : {i}";
                // 绑定点击事件
                newMusicButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    // 这里添加每个 MusicButton 的执行逻辑
                    Debug.Log("PlayMusic");
                });
            }
        }
    }
}