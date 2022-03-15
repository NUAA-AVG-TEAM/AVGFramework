using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDisplayManagement;
using UnityEngine.UI;
using System.IO;
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
                int z = new int();
                z = i;
                GetOrAddComponetInChild<Button>("file" + z.ToString()).onClick.AddListener(() =>
                {
                    Debug.Log(z.ToString());
                    var path = Path.Combine(Application.persistentDataPath, "saveFile" + z.ToString());//存档路径
                    DataManager.GetInstance.saveFile(path);//读取数据填上
                });
            }
        }
    }
}
