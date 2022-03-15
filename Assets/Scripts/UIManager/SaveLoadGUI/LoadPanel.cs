using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using PanelDisplayManagement;
using UnityEngine.UI;
using CoroutineManagement;
public class LoadPanel : BasePanel
{
    private static LoadPanel instance;
    public LoadPanel() : base("Prefabs/UI/Panel/LoadPanel") { }
    public static LoadPanel GetInstance
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
            GetOrAddComponetInChild<Button>("last").onClick.AddListener(() =>
            {
                Debug.Log("Button is clicked");
                // 切换 GamingGUI

            });
            GetOrAddComponetInChild<Button>("next").onClick.AddListener(() =>
            {
                Debug.Log("Button is clicked");
                // 切换 GamingGUI
            });
            //绑定存档按钮
            for (int i = 1; i <= 8; i++)
            {
                int z = new int();
                z = i;
                GetOrAddComponetInChild<Button>("file" + z.ToString()).onClick.AddListener(() =>
                  {
                      Debug.Log(z.ToString());
                      var path = Path.Combine(Application.persistentDataPath, "saveFile" + z.ToString());//存档路径
                      DataManager.GetInstance.loadFile(path);
                  });
            }
        }
    }
}
