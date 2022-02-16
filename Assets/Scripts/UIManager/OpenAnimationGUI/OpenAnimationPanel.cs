using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDisplayManagement;
using UnityEngine.UI;
using CoroutineManagement;

public class OpenAnimationPanel : BasePanel
{
/* DATA */
    // panel Ԥ����λ��
    public OpenAnimationPanel() : base("Prefabs/UI/Panel/OpenAnimationPanel") { }
    // ��ǰ���ŵ�֡��0��ʾ��δ��ʼ����
    int nowFrame = 0;
    // ��֡��
    int totalFrame = 15;
    // fPS
    float fps = 5.0f;
 
/* LOGIC */
    sealed public override IEnumerator OnShow()
    {
        // ����Onshow
        yield return CoroutineManager.GetInstance().StartCoroutine(base.OnShow());
        // ���Ŷ���
        yield return CoroutineManager.GetInstance().StartCoroutine(PlayAnimation());
        // �󶨰�ť�¼�
        if (panelObj != null)
        {
            GetOrAddComponetInChild<Button>("BtnEnter").onClick.AddListener(() =>
            {
                Debug.Log("BtnEnter is clicked");
                // ���� MAINGUI
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
                // CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new MainPanel()));
                UIManager.GetInstance.GetSMachine.ChangeState("MainGUI");
            });
        }
    }

    /// <summary>
    /// ������һ֡
    /// </summary>
    private void NextFrame()
    {
        // ��ȡͼƬ��ͼλ��
        Button btnNext =  GetOrAddComponetInChild<Button>("BtnEnter");
        // btnNext.image.sprite = ��ͼƬ
        nowFrame++;
        Debug.Log($"Frame : {nowFrame} / {totalFrame}");
    }

    /// <summary>
    /// �Զ�����
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayAnimation()
    {
        while ( nowFrame < totalFrame )
        {
            NextFrame();
            yield return new WaitForSecondsRealtime(1 / fps);
        }
        yield break;
    }
}