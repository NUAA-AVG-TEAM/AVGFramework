using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDisplayManagement;
using UnityEngine.UI;
using CoroutineManagement;

public class CGPanel : BasePanel
{
    /* DATA */
    // panel Ԥ����λ��
    public CGPanel() : base("Prefabs/UI/Panel/CGPanel") { }
    // ��ǰ���ŵ�֡��0��ʾ��δ��ʼ����
    int nowFrame = 0;
    // ��֡��
    int totalFrame = 5;
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
            GetOrAddComponetInChild<Button>("BtnExit").onClick.AddListener(() =>
            {
                Debug.Log("BtnExit is clicked");
                // �л� AppreciateGUI
                /// ����ǰ״̬�µ� panel ȫ��POP
                CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
              
            });
        }
    }

    /// ������һ֡
    private void NextFrame()
    {
        // ��ȡͼƬ��ͼλ��
        Button btnNext = GetOrAddComponetInChild<Button>("BtnExit");
        // btnNext.image.sprite = ��ͼƬ
        nowFrame++;
        Debug.Log($"Frame : {nowFrame} / {totalFrame}");
    }

    /// �Զ�����
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
