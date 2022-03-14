using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoroutineManagement;
using PanelDisplayManagement;
public class SaveLoadGUI : MonoBehaviour
{
    // Start is called before the first frame update
    private static SaveLoadGUI instance;
    private static StateMachine sMachine;
    public static SaveLoadGUI GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        sMachine = new StateMachine("UIManager");

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// UIManager״̬���л�����UIʱ����
    /// </summary>
    public void OnEnter()
    {
        instance.gameObject.SetActive(true);
        PanelDisplayManager.GetInstance().SetTartGUI(instance.gameObject);
        CoroutineManager.GetInstance().StartCoroutine(PanelDisplayManager.GetInstance().Push(new SavePanel()));
    }

    /// <summary>
    /// �Ӹ�UI�л�����һ��UIʱ����
    /// </summary>
    public void OnLeave()
    {
        // GUI�ӵ�ǰ����
        PanelDisplayManager.GetInstance().SetTartGUI(null);

        // �� GUI
        instance.gameObject.SetActive(false);
    }
}
