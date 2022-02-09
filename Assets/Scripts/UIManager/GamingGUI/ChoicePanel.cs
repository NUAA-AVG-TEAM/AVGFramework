using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
public class ChoicePanel : MonoBehaviour
{
    private static ChoicePanel instance;
    private static int nextIndex;
    public Button choicePrefab;
    public static ChoicePanel GetInstance
    {
        get { return instance; }
    }

    public void ResetIndex()
    {
        nextIndex = 0;
    }

    public int GetIndex()
    {
        return nextIndex;
    }
    // 只绑定按钮事件，返回的是指令值~
    // choice : 选项
    // id : 选项ID
    void ChoiceNormal(string _params)
    {
        // nextIndex = 0;
        Hashtable ht = JsonMapper.ToObject<Hashtable>(_params);
        int num = ht.Count;
        for(int i = 0; i < num; i++)
        {
            string tmp = "choice" + (i + 1).ToString();
            int idx = (int)ht[tmp];
            string id = "id" + (i + 1).ToString();
            //生成按钮并绑定按钮事件
            Button btn = Instantiate(choicePrefab, instance.gameObject.transform);
            btn.transform.Find("Text").GetComponent<Text>().text = LanguageManager.GetInstance.GetText(id);
            btn.onClick.AddListener(delegate
            {
                //改变量，choicepanel去掉
                nextIndex = idx;
                instance.gameObject.SetActive(false);
            });

        }
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// GamingGUI状态机切换到该UI时触发
    /// </summary>
    public void OnEnter()
    {

    }

    /// <summary>
    /// 从该UI切换到另一个UI时触发
    /// </summary>
    public void OnLeave()
    {

    }
}
