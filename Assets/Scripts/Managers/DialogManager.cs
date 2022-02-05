using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    // 有可能有改文字显示速度的需要,如果需要的话直接读取SettingVarManager的相应变量即可（）
    private static DialogManager instance;
    public Text text;//绑定gamepanel下的Text组件
    public float speed;//文字出现的速度
    private GamingPanel gp;//引用UIManager里的GamingPanel
    private bool IfComplete;//是否被点击（直接显示全部text）
    private bool IfPrinting;//是否正在播放文字
    private string TextContent;//临时文字内容
    private float timer;//记录时间
    public static DialogManager GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        IfPrinting = false;
        IfComplete = false;
        text = gp.LogPanel.Text;
        instance = this;
        timer = 0;
    }
    private void Update() 
    {
        if(!IfComplete)
        {
            if(IfPrinting)
            {
                int TempTime;
                TempTime = (int)(speed*timer); 
                if(TempTime <= TextContent.Length())
                {
                    text.Text = TextContent.Substring(0,TempTime);
                    timer += Time.deltaTime; 
                }
                else
                {
                    EndPlay();
                }      
            }    
        }
        else
        {
            text.Text = TextContent;
            EndPlay();
        }  
    }
    private void EndPlay()
    {
        TextContent = "";
        IfComplete = false;
        IfPrinting = false;
        timer = 0;
    }
    private void PlayText(string _text)
    {
        text.Text = "";//清空text
        TextContent = _text;
        IfPrinting = true;
    }
    //需要加一个对按钮的管理事件
    private void OnGUI() 
    {
        
    }
}
