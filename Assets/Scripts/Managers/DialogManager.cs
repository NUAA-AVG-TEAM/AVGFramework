using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class DialogManager : MonoBehaviour
{
    // 有可能有改文字显示速度的需要,如果需要的话直接读取SettingVarManager的相应变量即可（）
    private static DialogManager instance;
    public TextMeshProUGUI text;//绑定gamepanel下的Text组件
    public Button Compbt;//一键完成按钮
    public float speed;//文字出现的速度
    private GamingPanel gp;//引用UIManager里的GamingPanel
    private bool IfComplete;//是否被点击（直接显示全部text）
    private bool IfPrinting;//是否正在播放文字
    private string TextContent;//临时文字内容
    private int TempTime;

    public static DialogManager GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        IfPrinting = false;
        IfComplete = false;
        //text = gp.LogPanel.Text;
        instance = this;
        speed = 0;
        
    }
    private void EndPlay()
    {
        TextContent = "";
        TempTime = 0;
        speed = 0;
        IfComplete = false;
        IfPrinting = false;
    }
    private void Comp()
    {
        IfComplete = true;
        //IfPrinting = false;
    }
    public IEnumerator PlayText(string _text,float gaptime,bool IsSkip = false)
    {
        Compbt.onClick.AddListener(Comp);//先绑定按钮
        text.text = "";//清空text
        TextContent = _text;
        speed = gaptime;
        IfPrinting = true;
        while(IfPrinting)
        {
            yield return new WaitForSeconds(gaptime);
            if(IfComplete)
            {
                text.text = TextContent;
                EndPlay();
            }
            else
            {
                if(TempTime < TextContent.Length)
                {
                    text.text = text.text + TextContent[TempTime];
                    TempTime += 1; 
                }
                else
                {
                    EndPlay();
                }  
            }
        }
        Compbt.onClick.RemoveListener(Comp);//解绑按钮
    }
    //需要加一个对按钮的管理事件
    
}
