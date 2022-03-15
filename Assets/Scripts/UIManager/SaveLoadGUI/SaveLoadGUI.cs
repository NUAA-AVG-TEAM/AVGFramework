using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CoroutineManagement;
using PanelDisplayManagement;
public class SaveLoadGUI : MonoBehaviour
{
    // Start is called before the first frame update
    private static SaveLoadGUI instance;
    private static StateMachine sMachine;
    public Button save;
    public Button load;
    public Button qload;
    public GameObject panel;
    public static SaveLoadGUI GetInstance
    {
        get { return instance; }
    }
    
    private void Awake()
    {
        instance = this;
        sMachine = new StateMachine("SaveloadManager");
        sMachine.AddState("SaveGUI",Save_OnEnter,OpenAnimationGUI.GetInstance.OnLeave);
        sMachine.AddState("LoadGUI", Load_OnEnter, OpenAnimationGUI.GetInstance.OnLeave);
        sMachine.AddState("QloadGUI", Qload_OnEnter, OpenAnimationGUI.GetInstance.OnLeave);
        sMachine.SetDefaultState("SaveGUI");
        sMachine.StartSM();
        save.onClick.AddListener(delegate
        {
            Debug.Log("saveload");
            CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
            sMachine.ChangeState("SaveGUI");
        });
        load.onClick.AddListener(delegate
        {
            CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
            sMachine.ChangeState("LoadGUI");
        });
        qload.onClick.AddListener(delegate
        {
            CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Pop());
            sMachine.ChangeState("QloadGUI");
        });
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        panel.transform.SetAsLastSibling();
    }

    /// <summary>
    /// UIManager状态机切换到该UI时触发
    /// </summary>
    public void OnEnter()
    {
        instance.gameObject.SetActive(true);
    }

    /// <summary>
    /// 从该UI切换到另一个UI时触发
    /// </summary>
    public void Save_OnEnter()
    {
        PanelDisplayManager.GetInstance().SetTartGUI(instance.gameObject);
        CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new SavePanel()));
        panel.transform.SetAsLastSibling();
    }
    public void Load_OnEnter()
    {
        PanelDisplayManager.GetInstance().SetTartGUI(instance.gameObject);
        CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new LoadPanel()));
        panel.transform.SetAsLastSibling();
    }
    public void Qload_OnEnter()
    {
        PanelDisplayManager.GetInstance().SetTartGUI(instance.gameObject);
        CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new QLoadPanel()));
        panel.transform.SetAsLastSibling();
    }
}
