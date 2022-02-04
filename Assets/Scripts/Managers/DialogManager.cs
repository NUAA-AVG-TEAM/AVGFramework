using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    // 有可能有改文字显示速度的需要,如果需要的话直接读取SettingVarManager的相应变量即可（）
    private static DialogManager instance;

    public static DialogManager GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void PlayText(string _text)
    {

    }
}
