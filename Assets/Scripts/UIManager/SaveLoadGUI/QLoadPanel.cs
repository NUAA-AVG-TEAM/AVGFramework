using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using CoroutineManagement;
public class QLoadPanel : BasePanel
{
    private static QLoadPanel instance;
    public QLoadPanel() : base("Prefabs/UI/Panel/QLoadPanel") { }
    public static QLoadPanel GetInstance
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
            for (int i = 0; i <= 8; i++)
            {
                int z = new int();
                z = i;
                GetOrAddComponetInChild<Button>("file" + z.ToString()).onClick.AddListener(() =>
                {
                    Debug.Log(z.ToString());
                    var path = Path.Combine(Application.persistentDataPath, "qsaveFile" + z.ToString());//存档路径
                    DataManager.GetInstance.loadFile(path);
                });
            }
        
        }
    }
}
