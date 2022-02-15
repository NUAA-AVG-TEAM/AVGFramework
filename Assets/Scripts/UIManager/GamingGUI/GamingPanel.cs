﻿///@author Folivora Li
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using DG.Tweening;
using TMPro;
public class GamingPanel : MonoBehaviour
{
    private static GamingPanel instance;
    private bool isAllowJmpMsg; //是否允许跳过未读信息
    private bool isChangeTxtColor;   //是否改变已读文字颜色
    private bool isStopCV;  //点击鼠标时CV是否停止
    private bool isCancelAuto;  //点击鼠标时是否停止自动状态
    private float textSpeed = 0.05f; //文字显示速度
    private float autoSpeed = 0.05f;   //auto文字播放速度
    private bool isFinishCV;    //是否等待CV播放完成了之后再进行下一幕
    private string lang;    //当前的语言
    List<Hashtable> instrPack;  //指令配表
    public Button btn;  //面板上的button
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI personName;
    public Button autoBtn;
    public Button saveBtn;
    public Button qSaveBtn;
    public Button loadBtn;
    public Button qLoadBtn;
    public Button lastSceneBtn;
    public Button nextSceneBtn;
    public Button skipBtn;
    public Button logBtn;
    public Button settingBtn;
    public Button hideBtn;
    private bool _isClick;
    private bool isSkip = false;  //是否处在跳过模式
    private int msg = 0;
    private bool IsSkip
    {
        set
        {
            isSkip = value;
            //补改贴图逻辑
        }

    }


    private bool isAuto = false; //是否处在自动模式
    private bool isHide = false; //是否处在隐藏模式
    public GameObject textBg;
    private int nowIndex;

    private int sceneIndex;   //当前场景名称的索引
    private int groupIndex;   //当前指令对话所在组的位置

    private bool isGaming;

    public Image bg;    // 背景图片
    public Image imgMask;  //生成的纯黑图片
    public static GamingPanel GetInstance
    {
        get { return instance; }
    }

    public void JumpScene(int _sceneIndex)
    {
        sceneIndex = _sceneIndex;
        nowIndex = _sceneIndex;
        groupIndex = 0;
        LogPanel.GetInstance.OnLeave();
        UIManager.GetInstance.GetSMachine.ChangeState("GamingGUI");
    }


    /// <summary>
    /// 凡是涉及到改isSkip的，都要改相应UI，现在没写逻辑，后补
    /// </summary>
    public void KeyboardBind()
    {
        // 绑定所有按键事件

        // 按空格
        void JumpSingle()
        {
            btn.onClick.Invoke();
        }
        KeyboardManager.GetInstance.Connect(0, KeyCode.Return, "jumpsingle", JumpSingle);

        void Jump()
        {
            IsSkip = true;
        }
        KeyboardManager.GetInstance.Connect(1, KeyCode.LeftControl, "jump", Jump);

        void CancelJump()
        {
            isSkip = false;

        }
        KeyboardManager.GetInstance.Connect(2, KeyCode.LeftControl, "canceljump", CancelJump);

        void Log()
        {
            logBtn.onClick.Invoke();
        }
        KeyboardManager.GetInstance.Connect(2, KeyCode.UpArrow, "log", Log);
    }
    
    public void KeyboardUnBind()
    {
        // 解绑所有按键事件
        KeyboardManager.GetInstance.DisConnect(0, KeyCode.Return, "jumpsingle");
        KeyboardManager.GetInstance.DisConnect(1, KeyCode.LeftControl, "jump");
        KeyboardManager.GetInstance.DisConnect(2, KeyCode.LeftControl, "canceljump");
        KeyboardManager.GetInstance.DisConnect(2, KeyCode.UpArrow, "log");


    }
    

    private void ButtonBind()
    {
        autoBtn.onClick.AddListener(delegate
        {
            Debug.Log("auto ->" + !isAuto);
            isAuto = !isAuto;
            // 更改Auto的贴图
        });

        saveBtn.onClick.AddListener(delegate
        {
            // 去saveloadGUI
            StateMachine sMachine = UIManager.GetInstance.GetSMachine;
            sMachine.ChangeState("SaveLoadGUI");
            ////// SaveLoadGUI.GetInstance.GetSMachine.ChangeState("SavePanel");

        });

        loadBtn.onClick.AddListener(delegate
        {
            // 去saveloadGUI
            StateMachine sMachine = UIManager.GetInstance.GetSMachine;
            sMachine.ChangeState("SaveLoadGUI");
            ////// SaveLoadGUI.GetInstance.GetSMachine.ChangeState("LoadPanel");
        });

        qSaveBtn.onClick.AddListener(delegate
        {
            ////// SaveLoadGUI.GetInstance.ChangeQSaveData(GetGameMessage());
        });

        qLoadBtn.onClick.AddListener(delegate
        {
            // 去saveloadGUI
            StateMachine sMachine = UIManager.GetInstance.GetSMachine;
            sMachine.ChangeState("SaveLoadGUI");
            ////// SaveLoadGUI.GetInstance.GetSMachine.ChangeState("QLoadPanel");
        });

        // 往前找
        lastSceneBtn.onClick.AddListener(delegate
        {
            // 后补UI逻辑
            int groupnum = 0;
            int i = nowIndex;
            while (i > 3)
            {
                if (instrPack[i]["type"].ToString().ToUpper() == "GST")
                {
                    groupnum++;
                }
                // 场景开始，且不是第一句话
                else if (instrPack[i]["type"].ToString().ToUpper() == "SST")
                {
                    if (groupnum > 1)
                    {
                        JumpScene(i);
                        break;
                    }
                }
                // 场景结束，直接跳到该场景开始的位置
                // groupIndex找的是场景开始的
                else if (instrPack[i]["type"].ToString().ToUpper() == "SED")
                {
                    // 在这里找一下Index即可~
                    JumpScene((int)instrPack[i]["groupIndex"]);
                    break;
                }
                // 章节开始，不能跳
                else if (instrPack[i]["type"].ToString().ToUpper() == "CST")
                {
                    Debug.Log("这是最开始的场景，不能往前跳哦~");
                    break;

                }
                else
                {
                    i = (instrPack[i]["groupIndex"] == null) ? (i - 1) : (int)instrPack[i]["groupIndex"];
                }
            }
        });

        nextSceneBtn.onClick.AddListener(delegate
        {
            for (int i = nowIndex; i < 0x3f3f3f; i++)
            {
                // 场景开始
                if (instrPack[i]["type"].ToString().ToUpper() == "SST")
                {
                    JumpScene(i);
                    break;
                }
                // 章节结束，不能跳
                else if (instrPack[i]["type"].ToString().ToUpper() == "CED")
                {
                    Debug.Log("这是最后一个场景，无法跳转！");
                    break;
                }
            }
        });

        logBtn.onClick.AddListener(delegate
        {
            // 不用状态机来管理
            // 直接打开，并解绑事件

            StartCoroutine(LogPanel.GetInstance.OnEnter());
            //解绑键盘事件，后补
        });

        skipBtn.onClick.AddListener(delegate
        {
            isSkip = !isSkip;
            // 后补改贴图

        });

        settingBtn.onClick.AddListener(delegate
        {
            UIManager.GetInstance.GetSMachine.ChangeState("SettingGUI");
        });

        hideBtn.onClick.AddListener(delegate
        {
            textBg.SetActive(false);
            // 后补按钮SetActiveFalse

        });

        //按钮悬浮事件，后补
        UIEventListener autoListener = autoBtn.gameObject.AddComponent<UIEventListener>();
        
        autoListener.OnMouseEnter += delegate (GameObject _object)
        {

        };
        autoListener.OnMouseExit += delegate (GameObject _object)
        {

        };

        

    }
    private void Awake()
    {
        instance = this;
        isGaming = false;
        instrPack = ExcelManager.GetExcel("InstructionPack");
        ButtonBind();
    }

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
        //绑定键盘事件
    }

    /// <summary>
    /// GamingGUI状态机切换到该UI时触发
    /// </summary>
    public void OnEnter()
    {
        // 图片透明度设置成0
        bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 0);
        instance.gameObject.SetActive(true);
        bg.DOFade(1, 0.5f);
        Debug.Log("enter gamingpanel");
        StartCoroutine("GameStart");
        // 提前记录一下设置里有关游戏进行的所有变量~,后补
        isGaming = true;


    }

    /// <summary>
    /// 从该UI切换到另一个UI时触发
    /// </summary>
    public void OnLeave()
    {
        isGaming = false;
        btn.onClick.RemoveAllListeners();
        StopCoroutine("GameStart");

        bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 0);
        // 目前图片的透明度变成0
        //不阻塞线程，两个显示同时触发即可~
        bg.DOFade(0, 0.5f).OnComplete(() => {
            gameObject.SetActive(false);
            bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 1);
        });
        

        
        

    }

    public void SetInstructionIndex(int _index)
    {
        nowIndex = _index;
        
    }

    public void SetSceneIndex(int _sceneIndex)
    {
        sceneIndex = _sceneIndex;
    }

    
    public int GetInstructionIndex()
    {
        return nowIndex;
    }

    // 返回当前组的索引
    public int GetGroupIndex()
    {
        return groupIndex;
    }

    public SaveData GetGameMessage()
    {
        SaveData data = new SaveData();
        data.index = groupIndex;
        for(int i = nowIndex;i < 0x3f3f3f3f ; i++)
        {
            if(instrPack[i]["type"].ToString().ToUpper() == "CBG")
            {
                data.bgID = instrPack[i]["ID"].ToString();
                break;
            }
        }
        data.nowDate = System.DateTime.Now.ToString();
        data.dialog = dialogText.text;
        data.sceneIndex = sceneIndex;
        return data;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_params"></param>
    /// <param name="_isSkip"></param>
    /// <returns></returns>
    /// 
    
    private IEnumerator Execute(Hashtable _params, bool _isSkip)
    {
        
        string type = _params["type"].ToString().ToUpper();
        Debug.Log(type);
        Hashtable ht;
        List<string> vt;
        /// <summary>
        /// PTX: Play Text
        /// PSE: PLay SE
        /// PBM: Play BGM
        /// PCV: Play CV
        /// SSE: Stop SE
        /// SBM: Stop BGM
        /// SCV: Stop CV
        /// CBG: Change Background
        /// MBG: Move BackGround
        /// PVD: Play Video
        /// SVD: Stop Video
        /// CSP: Create Sprite
        /// DSP: Delete Sprite
        /// MSP: Move Sprite
        /// CGSP: Change Sprite'
        /// GST: Group Start
        /// SST: Scene Start
        /// SED: Scene End
        /// CST: Chapter Start
        /// CED: Chapter End
        /// CHO: Choice Normal
        /// 记得加个鼠标点击缓冲事件，这个比较有必要还
        /// </summary>
        switch (type)
        {
            case "PTX":
                yield return DialogManager.GetInstance.PlayText(LanguageManager.GetInstance.GetText(_params["ID"].ToString()), isAuto ? autoSpeed : textSpeed, isSkip);
                Debug.Log("play text finish");
                break;

            case "PSE":
                SEManager.GetInstance.PlaySE(_params["ID"].ToString(), isSkip);
                break;

            case "PBM":
                BGMManager.GetInstance.PlayBGM(_params["ID"].ToString());
                break;

            case "PCV":
                CVManager.GetInstance.PlayCV(_params["ID"].ToString(), isSkip);
                break;

            case "SSE":
                SEManager.GetInstance.StopSE(_params["ID"].ToString());
                break;

            case "SBM":
                BGMManager.GetInstance.StopBGM(_params["ID"].ToString());
                break;

            case "SCV":
                CVManager.GetInstance.StopCV(_params["ID"].ToString());
                break;

            case "CBG":
                BgManager.GetInstance.ChangeBg(_params["ID"].ToString());
                break;

            case "MBG":
                ht = JsonMapper.ToObject<Hashtable>(_params["params"].ToString());
                BgManager.GetInstance.MoveBg((int)ht["type"]);
                break;

            case "PVD":
                //////VideoManager.GetInstance.PlayVideo(_params["ID"].ToString());
                break;

            case "SVD":
                //////VideoManager.GetInstance.StopVideo(_params["ID"].ToString());
                break;


            /// 创建立绘的时候一个一个创，创的是节点
            /// 删的时候也一个一个删，但是改变的时候就是一起改变= =
            case "CSP":
                ht = JsonMapper.ToObject<Hashtable>(_params["params"].ToString());
                // 先这么写着，之后再沟通
                ////// SpriteManager.GetInstance.CreateSprite(_params["ID"], ht["name"].ToString(), ht, isSkip);
                break;

            case "DSP":
                ht = JsonMapper.ToObject<Hashtable>(_params["params"].ToString());
                // 先这么写着，之后再沟通
                ////// SpriteManager.GetInstance.DeleteSprite(_params["ID"], isSkip);
                break;

            case "MSP":
                ht = JsonMapper.ToObject<Hashtable>(_params["params"].ToString());
                // 先这么写着，之后再沟通
                vt = JsonMapper.ToObject<List<string>>(_params["ID"].ToString());
                ////// SpriteManager.GetInstance.MoveSprite(vt, ht["name"].ToString(), ht, isSkip);
                break;

            case "CGSP":
                ht = JsonMapper.ToObject<Hashtable>(_params["params"].ToString());
                // 先这么写着，之后再沟通
                vt = JsonMapper.ToObject<List<string>>(_params["ID"].ToString());
                ////// SpriteManager.GetInstance.ChangeSprite(vt, ht["name"].ToString(), ht, ht["dout"], isSkip);
                break;

            case "GST":
                groupIndex = nowIndex;
                break;

            case "SST":
                sceneIndex = nowIndex;
                break;

            case "CHN":
                //普通Choice，直接修改下一个即可,这里阻塞线程，本质上是进入另一个协程
                ChoicePanel.GetInstance.ResetIndex();
                ChoicePanel.GetInstance.ChoiceNormal(_params["params"].ToString());
                yield return ChoicePanel.GetInstance.GetIndex() > 0;
                msg = ChoicePanel.GetInstance.GetIndex();
                break;

            case "CED":
                yield break;
            
        }

        yield break ;
        
    }

    void ChangeButtonImg()
    {

    }

    bool ClickEvent()
    {
        // 若背景未打开，先打开背景和所有按钮
        if(!textBg.activeSelf)
        {
            textBg.SetActive(true);
            return false;
        }

        if (isStopCV)
        {
            //停止当前播放的CV，后补

        }
        if (isCancelAuto)
        {
            isAuto = false;
        }
        return true;

    }

    IEnumerator GameStart()
    {
        
        // 需保证在执行协程之前，这些变量均初始化好，后补健壮处理
        // 假如是存档 / 继续游戏/ 开始游戏进来的，需要重置，重置前默认把groupIndex重置成0即可
        // 否则目前的状态可以利用，groupindex不用重置
        if (groupIndex == 0)
        {
            groupIndex = nowIndex;
        }

        // only for test
        nowIndex = 4;

        // test end
        int nextIndex = nowIndex + 1;
        

        // 按下按钮时，停止跳过模式，每次点击均执行
        // 反正update里每帧都做一次键盘事件的判断
        void StopSkip()
        {
            isSkip = false;
            //更改按钮贴图，后补
        }

        void GotoNext()
        {
            if (!ClickEvent())
            {
                return;
            }
            _isClick = true;
        }

        void LockButton()
        {
            autoBtn.interactable = false;
            saveBtn.interactable = false;
            qSaveBtn.interactable = false;
            loadBtn.interactable = false;
            qLoadBtn.interactable = false;
            lastSceneBtn.interactable = false;
            nextSceneBtn.interactable = false;
            skipBtn.interactable = false;
            logBtn.interactable = false;
            settingBtn.interactable = false;
            hideBtn.interactable = false;
            
                
        }

        void UnLockButton()
        {
            autoBtn.interactable = true;
            saveBtn.interactable = true;
            qSaveBtn.interactable = true;
            loadBtn.interactable = true;
            qLoadBtn.interactable = true;
            lastSceneBtn.interactable = true;
            nextSceneBtn.interactable = true;
            skipBtn.interactable = true;
            logBtn.interactable = true;
            settingBtn.interactable = true;
            hideBtn.interactable = true;
        }
        btn.onClick.AddListener(StopSkip);
        while (nowIndex != 0)
        {
            Hashtable nowInstr = instrPack[nowIndex];
            Debug.Log(nowInstr["nextIndex"]);
            nextIndex = nowInstr["nextIndex"] == null ? nowIndex + 1 : (int)nowInstr["nextIndex"];
            LockButton();
            yield return Execute(nowInstr, isSkip);
            /*
            if(msg == 1000001)
            {
                ChoicePanel.GetInstance.ResetIndex();
                ChoicePanel.GetInstance.ChoiceNormal(nowInstr["params"].ToString());
                yield return ChoicePanel.GetInstance.GetIndex() > 0;
                msg = ChoicePanel.GetInstance.GetIndex();
            }
            */
            // 留个坑，记得把面板打开
            if(!textBg.activeSelf)
            {
                textBg.SetActive(true);
            }

            // 当前指令为播放文字指令，此时已播放完文字
            if(nowInstr["type"].ToString().ToUpper() == "PTX")
            {
                Debug.Log(isSkip);
                Debug.Log("finish");
                if (!isSkip)
                {
                    // _time为自动模式的等待时间
                    int _time = 1;
                    UnLockButton();
                    // btn.onClick.AddListener(StopSkip);
                    _isClick = false;
                    yield return Wait(_time);
                    //若没被打断，且没处于自动模式
                    //处在auto模式下时，不允许隐藏面板
                    Debug.Log(123456);
                    if (!_isClick && !isAuto)
                    {
                        //停止协程直到isAuto,isSkip,isClick有一个为真时
                        btn.onClick.AddListener(GotoNext);
                        Debug.Log(isAuto);
                        Debug.Log(isSkip);
                        Debug.Log(_isClick || isSkip || _isClick);
                        yield return WaitForPlayNext();
                        btn.onClick.RemoveListener(GotoNext);
                        Debug.Log(3456);
                    }
                    if (isFinishCV)
                    {
                        // 获得当前CV是否播放完，播放完了就结束阻塞
                        yield return 0;
                    }
                }
                else
                {
                    yield return 1;
                }
            }
            
            nowIndex = nextIndex;

            // msg != 0说明是选择分支，msg即为下一条指令的地址
            if(msg > 0)
            {
                nowIndex = msg;
            }
            else if(msg < 0)
            {
                break;
            }
            yield return 1;
        }        
    }

    IEnumerator Wait(int _times)
    {
        _isClick = false;
        void ButtonClick()
        {
            if (!ClickEvent())
            {
                return;
            }
            _isClick = true;
        }
        // 目前每0.05秒检测一次
        float itv = 0.05f;
        int cycle = (int)(_times / itv);
        int i = 0;
        btn.onClick.AddListener(ButtonClick);
        while (!_isClick && i < cycle && !isSkip)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(itv);
            i++;
        }
        btn.onClick.RemoveListener(ButtonClick);
    }

    IEnumerator WaitForPlayNext()
    {
        while (!(isAuto || isSkip || _isClick))
        {
            yield return 0;
        }
        yield break;
    }

}
