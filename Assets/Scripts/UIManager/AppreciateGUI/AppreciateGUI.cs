using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppreciateGUI : MonoBehaviour
{
    private static AppreciateGUI instance;

    public static AppreciateGUI GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// UIManager状态机切换到该UI时触发
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
