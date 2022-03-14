using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDisplayManagement;
using UnityEngine.UI;
using CoroutineManagement;
public class SavePanel : BasePanel
{
    private static SavePanel instance;
    public SavePanel() : base("Prefabs/UI/Panel/SavePanel") { }
    public static SavePanel GetInstance
    {
        get { return instance; }
    }
    sealed public override IEnumerator OnShow()
    {
        // 父类OnShow
        yield return CoroutineManager.GetInstance().StartCoroutine(base.OnShow());
        // 绑定按钮事件
        if (panelObj != null)
        {
            //绑定存档按钮
            for(int i=0;i<=8;i++)
            {

            }
        }
    }
}
