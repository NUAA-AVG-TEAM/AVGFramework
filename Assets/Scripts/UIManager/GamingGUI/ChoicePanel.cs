using System.Collections;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
using DG.Tweening;
public class ChoicePanel : MonoBehaviour
{
    private static ChoicePanel instance;
    private static int nextIndex;
    public Button choicePrefab;
    private float fadetime = 0.5f;
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
    public void ChoiceNormal(string _params)
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
                OnLeave();
            });

        }
        OnEnter();
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

        // Panel淡入
        var panel = instance.gameObject.GetComponent<Image>();
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
        panel.DOFade(1, fadetime);
        instance.gameObject.SetActive(true);
    }

    /// <summary>
    /// 从该UI切换到另一个UI时触发
    /// </summary>
    public void OnLeave()
    {
        var panel = instance.gameObject.GetComponent<Image>();
        panel.DOFade(1, fadetime).OnComplete(() =>
        {
            instance.gameObject.SetActive(false);
        });
    }
}
