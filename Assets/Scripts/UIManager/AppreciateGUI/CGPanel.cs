using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDisplayManagement;
using UnityEngine.UI;
using CoroutineManagement;

public class CGPanel : BasePanel
{
    /* DATA */
    // panel 预制体位置
    public CGPanel() : base("Prefabs/UI/Panel/CGPanel") { }
    // 当前播放的帧，0表示尚未开始播放
    int nowFrame = 0;
    // 总帧数
    int totalFrame = 5;
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
            GetOrAddComponetInChild<Button>("BtnExit").onClick.AddListener(() =>
            {
                Debug.Log("BtnExit is clicked");
                // 切换 AppreciateGUI
                /// 将当前状态下的 panel 全部POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
              
            });
        }
    }

    /// 播放下一帧
    private void NextFrame()
    {
        // 获取图片贴图位置
        Button btnNext = GetOrAddComponetInChild<Button>("BtnExit");
        // btnNext.image.sprite = 新图片
        nowFrame++;
        Debug.Log($"Frame : {nowFrame} / {totalFrame}");
    }

    /// 自动播放
    private IEnumerator PlayAnimation()
    {
        nowFrame = 0;
        while (nowFrame < totalFrame)
        {
            NextFrame();
            yield return new WaitForSecondsRealtime(1 / fps);
        }
        yield break;
    }
}
