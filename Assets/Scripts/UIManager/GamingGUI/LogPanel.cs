using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LogPanel : MonoBehaviour
{
    private static LogPanel instance;
    List<Hashtable> instrPack;  //指令配表
    public GameObject prefabsPanel;
    
    public GameObject logSv;   // log面板下的Scroll View下的Content
    public GameObject sceneSv;  // scene面板下的Scroll View下的Content

    public GameObject logPnl;   //log面板
    public GameObject scenePnl; //scene面板

    public Button toSceneBtn;   //log上的sceneBtn
    public Button toLogBtn;     //scene上的logBtn

    public Button logBackBtn;   //log上的返回
    public Button sceneBackBtn; //scene上的返回

    public Image Bg;
    public Image bgText;    //bgText;
    public Image bg;    //图片的背景，现在先空下
    public Image btnsPanel;    //按钮面板 不用panel，获取不到太蛋疼了

    public Scrollbar barLog;    //滚动条
    public Scrollbar barScene;
    private float fadeTime = 0.5f;
    int pageNum;        // 屏幕渲染选项数量


    public static LogPanel GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        instrPack = ExcelManager.GetExcel("InstructionPack");
        pageNum = 4;
        ButtonBind();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    ///
    /// 不作为状态机去调用
    /// </summary>
    /// 
    private void RenderPanel()
    {
        int i = GamingPanel.GetInstance.GetInstructionIndex();
        Stack<LogMessage> sGroup = new Stack<LogMessage>();
        Stack<LogMessage> sScene = new Stack<LogMessage>();

        // 压栈
        while(i > 3)
        {
            if (instrPack[i]["type"] == null)
            {
                i--;
                continue;
            }
            string tp = instrPack[i]["type"].ToString().ToUpper();
            // 遍历到文字指令
            if (tp == "PTX")
            {
                LogMessage tmp = new LogMessage();
                tmp.index = i;
                tmp.ID = instrPack[i]["ID"].ToString();
                sGroup.Push(tmp);
                
            }
            else if(tp == "GST")
            {
                
            }
            else if(tp == "SST")
            {
                LogMessage tmp = new LogMessage();
                tmp.index = i;
                tmp.ID = instrPack[i]["ID"].ToString();
                sScene.Push(tmp);
            }
            else if(tp == "CST")
            {
                break;
            }
            if (instrPack[i]["groupIndex"] != null)
            {
                i = (int)instrPack[i]["groupIndex"];
            }
            else
            {
                i--;
            }
        }

        // 退栈，开始渲染面板
        while(sGroup.Count > 0)
        {
            LogMessage tmp = sGroup.Pop();
            GameObject obj = Instantiate(prefabsPanel, logSv.transform);
            obj.transform.Find("Text").GetComponent<Text>().text = LanguageManager.GetInstance.GetText(tmp.ID);
            // 绑定跳转事件
            obj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate
                {
                    // 加一个显示UI事件，逻辑是生成一个UI并显示，这个UI的逻辑也需要绑定~，后补
                    GamingPanel.GetInstance.JumpScene(tmp.index);
                }
            );

        }
        while (sScene.Count > 0)
        {
            LogMessage tmp = sScene.Pop();
            GameObject obj = Instantiate(prefabsPanel, logSv.transform);
            obj.transform.Find("Text").GetComponent<Text>().text = LanguageManager.GetInstance.GetText(tmp.ID);
            // 绑定跳转事件
            obj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate
            {
                // 加一个显示UI事件，逻辑是生成一个UI并显示，这个UI的逻辑也需要绑定~，后补
                GamingPanel.GetInstance.JumpScene(tmp.index);
            }
            );

        }


    }

    public IEnumerator OnEnter()
    {
        //解绑事件
        GamingPanel.GetInstance.KeyboardUnBind();
        //先渲染画面

        RenderPanel();
        //将滚动条调到最下方~
        barLog.value = 0;
        barScene.value = 0;

        //等一帧才会刷新~
        yield return new WaitForEndOfFrame();
        //首先将自己的透明度调成0
        bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, bg.color.a);
        btnsPanel.color = new Color(btnsPanel.color.r, btnsPanel.color.g, btnsPanel.color.b, btnsPanel.color.a);
        instance.gameObject.SetActive(true);
        logPnl.SetActive(true);
        //将背景版透明度调成0,自己透明度调成1
        bgText.DOFade(0, fadeTime);
        btnsPanel.DOFade(0, fadeTime);
        bg.DOFade(1, fadeTime);
        yield break;
    }

    public void ButtonBind()
    {
        toLogBtn.onClick.AddListener(delegate
        {
            Debug.Log("To LogPanel");
            scenePnl.SetActive(false);
            logPnl.SetActive(true);
        });

        toSceneBtn.onClick.AddListener(delegate
        {
            Debug.Log("To ScenePanel");
            scenePnl.SetActive(false);
            logPnl.SetActive(true);
        });

        logBackBtn.onClick.AddListener(delegate
        {
            Debug.Log("Log Back");
            OnLeave();
        });

        sceneBackBtn.onClick.AddListener(delegate
        {
            Debug.Log("Scene Back");
            OnLeave();
        });
    }

    /// <summary>
    /// 从该UI切换到另一个UI时触发
    /// 
    /// </summary>
    public void OnLeave()
    {



        //将背景版透明度调成0,自己透明度调成1
        // 记得加一个canvas group~
        bgText.DOFade(1, fadeTime);
        btnsPanel.DOFade(1, fadeTime);
        bg.DOFade(0, fadeTime).OnComplete(()=> {
            scenePnl.SetActive(false);
            logPnl.SetActive(false);
            //把组件下的所有东西去掉，
            FunctionUtil.RemoveChildren(logSv.transform);
        });

        
        
        // 绑定事件
        GamingPanel.GetInstance.KeyboardBind();
        
    }
}
