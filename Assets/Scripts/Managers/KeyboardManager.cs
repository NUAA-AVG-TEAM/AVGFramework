/// 键盘绑定事件
/// 大体逻辑：
/// qutiGUI打开时，所有键盘事件无效
/// 在这里用一个队列管理所

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    // mode 0: 按下 1:长按 2: 抬起
    // 考虑到unity是伪多线程，应该不会对效率产生影响~
    //private Dictionary<KeyCode, Dictionary<string, System.Action>> dict;
    public List<Dictionary<KeyCode, Dictionary<string, System.Action>>> list;
    private int maxMode;
    // Start is called before the first frame update
    private void Awake()
    {
        // 初始化
        maxMode = 3;
        list = new List<Dictionary<KeyCode, Dictionary<string, System.Action>>>();
        for (int i = 0; i < maxMode; i++)
        {
            list.Add(new Dictionary<KeyCode, Dictionary<string, System.Action>>());
        }



    }
    void Start()
    {

    }

    // 讲道理应该用字典= = 但是鉴于也没有什么新花样了就硬写了
    bool JudgeMode(int _mode, KeyCode _key)
    {
        if (_mode == 0)
        {
            return Input.GetKeyDown(_key);
        }
        else if (_mode == 1)
        {
            return Input.GetKey(_key);
        }
        else
        {
            return Input.GetKeyUp(_key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 把已绑定的全执行一遍
        if (QuitGUI.GetInstance.gameObject.activeSelf)
        {
            return;
        }
        // 键盘按下事件
        for (int i = 0; i < maxMode; i++)
        {
            var dict = list[i];
            foreach (KeyValuePair<KeyCode, Dictionary<string, System.Action>> kvp in dict)
            {
                if (JudgeMode(i, kvp.Key))
                {
                    foreach (KeyValuePair<string, System.Action> _kvp in kvp.Value)
                    {
                        _kvp.Value();
                    }
                }
            }
        }

    }

    public void Connect(int _mode, KeyCode _key, string _name, System.Action _func)
    {
        // 首先查找 _key 是否在这里
        if (_mode >= maxMode || _mode < 0)
        {
            Debug.LogError("传进来的_mode参数不能大于等于最大模式数或小于0！");
            return;
        }
        var dict = list[_mode];
        if (dict.ContainsKey(_key))
        {
            // 再查找有没有相应名称的函数

            // 遍历字典
            Dictionary<string, System.Action> _dict = dict[_key];
            if (_dict.ContainsKey(_name))
            {
                Debug.LogError("已绑定相应名称的函数，请换个函数名称再试一下QAQ");
                return;
            }
            else
            {
                _dict.Add(_name, _func);
            }
        }
        else
        {
            // 直接加
            Dictionary<string, System.Action> _dict = new Dictionary<string, System.Action>();
            _dict.Add(_name, _func);
            dict.Add(_key, _dict);
            return;
        }


    }

    public void DisConnect(int _mode, KeyCode _key, string _name)
    {
        if (_mode >= maxMode || _mode < 0)
        {
            Debug.LogError("传进来的_mode参数不能大于等于最大模式数或小于0！");
            return;
        }
        var dict = list[_mode];
        // 首先查找 _key 是否在这里
        if (dict.ContainsKey(_key))
        {
            // 再查找有没有相应名称的函数

            // 遍历字典
            Dictionary<string, System.Action> _dict = dict[_key];
            if (_dict.ContainsKey(_name))
            {
                dict[_key].Remove(_name);
                Debug.Log("解绑成功！");
                return;
            }
            else
            {
                Debug.LogWarning("未绑定过此函数~");
                return;
            }
        }
        else
        {
            // 返回没有此函数~
            Debug.LogWarning("未绑定过此函数~");
            return;
        }

    }

    public bool HasConnected(int _mode, KeyCode _key, string _name)
    {
        if (_mode >= maxMode || _mode < 0)
        {
            Debug.LogError("传进来的_mode参数不能大于等于最大模式数或小于0！");
            return false;
        }
        // 首先查找 _key 是否在这里
        var dict = list[_mode];
        if (dict.ContainsKey(_key))
        {
            // 再查找有没有相应名称的函数

            // 遍历字典
            Dictionary<string, System.Action> _dict = dict[_key];
            if (_dict.ContainsKey(_name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
