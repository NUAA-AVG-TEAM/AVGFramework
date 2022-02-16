using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDisplayManagement;
using UnityEngine.UI;
using CoroutineManagement;

public class OpenAnimationPanel : BasePanel
{
/* DATA */
    // panel 预制体位置
    public OpenAnimationPanel() : base("Prefabs/UI/Panel/OpenAnimationPanel") { }
    // 当前播放的帧，0表示尚未开始播放
    int nowFrame = 0;
    // 总帧数
    int totalFrame = 15;
    // fPS
    float fps = 5.0f;
 
/* LOGIC */
    sealed public override IEnumerator OnShow()
    {
        // 父类Onshow
        yield return CoroutineManager.GetInstance().StartCoroutine(base.OnShow());
        // 播放动画
        yield return CoroutineManager.GetInstance().StartCoroutine(PlayAnimation());
        // 绑定按钮事件
        if (panelObj != null)
        {
            GetOrAddComponetInChild<Button>("BtnEnter").onClick.AddListener(() =>
            {
                Debug.Log("BtnEnter is clicked");
                // 切换 MAINGUI
                /// 将当前状态下的 panel 全部POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new MainPanel()));
                UIManager.GetInstance.GetSMachine.ChangeState("MainGUI");
                // timeexit = 所有东西的减出时间 = PDM.stack.count * ( 1 / speed )
                // CoroutineManager.GetInstance().StartCoroutine(UIManager.GetInstance.GetSMachine.ChangeState("MainGUI", 1, 0));
            });
        }
    }

    /// <summary>
    /// 播放下一帧
    /// </summary>
    private void NextFrame()
    {
        // 获取图片贴图位置
        Button btnNext =  GetOrAddComponetInChild<Button>("BtnEnter");
        // btnNext.image.sprite = 新图片
        nowFrame++;
        Debug.Log($"Frame : {nowFrame} / {totalFrame}");
    }

    /// <summary>
    /// 自动播放
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayAnimation()
    {
        nowFrame = 0;
        while ( nowFrame < totalFrame )
        {
            NextFrame();
            yield return new WaitForSecondsRealtime(1 / fps);
        }
        yield break;
    }
}
