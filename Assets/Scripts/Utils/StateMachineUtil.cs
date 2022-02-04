using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//

public class StateMachine
{
    private string machineName;
    private string defaultState;
    private string nowState;
    private string lastState;
    private HashSet<string> states;
    private Dictionary<string, System.Action> enterFunc;
    private Dictionary<string, System.Action> leaveFunc;
    private bool isPlaying;

    public StateMachine(string _name)
    {
        machineName = _name;
        enterFunc = new Dictionary<string, System.Action>();
        leaveFunc = new Dictionary<string, System.Action>();
        states = new HashSet<string>();
        nowState = "none";
        lastState = "none";
        defaultState = "none";
        enterFunc.Add("none", Pass);
        leaveFunc.Add("none", Pass);
        states.Add("none");
        isPlaying = false;
    }

    void Pass()
    {

    }

    // 设置默认状态
    public void SetDefaultState(string _defaultState)
    {
        defaultState = _defaultState;
    }
       
    // 添加状态，目前暂不支持一次添加多个状态和缺省进入离开事件，若无则挂一个空函数即可
    public void AddState(string _state, System.Action _enterFunc, System.Action _leaveFunc)
    {
        if (states.Contains(_state))
        {
            Debug.LogError(_state + "初始化Error!此状态已经存在");
            return;
        }
        states.Add(_state);
        enterFunc.Add(_state, _enterFunc);
        leaveFunc.Add(_state, _leaveFunc);
    }

    // 改变状态机状态
    public void ChangeState(string _newState)
    {
        if (!isPlaying)
        {
            Debug.LogWarning("状态机还没开启！改变状态无效");
            return;
        }
        // 退出当前状态
        if (!states.Contains(_newState))
        {
            Debug.LogError(_newState + "此状态不存在！");
            return;
        }
        leaveFunc[nowState]();
        lastState = nowState;
        nowState = _newState;
        enterFunc[nowState]();
    }

    // 开始启动状态机
    public void StartSM()
    {
        if (isPlaying)
        {
            Debug.LogWarning("当前状态机名称" + machineName + " 状态机正在运行....无法再次启动该状态机");
            return;
        }
        if (defaultState == "none")
        {
            Debug.LogError("当前默认状态为空，请设置默认状态！");
            return;
        }
        nowState = defaultState;
        if (!states.Contains(nowState))
        {
            Debug.LogError(nowState + "此状态不存在！");
            return;
        }
        enterFunc[nowState]();
        isPlaying = true;
        

        lastState = "none";
        Debug.Log("状态机开始运行...." + "当前状态" + nowState);
    }

    // 停止状态机
    public void StopSM()
    {
        if (!isPlaying)
        {
            Debug.LogWarning("当前状态机名称" + machineName + " 状态机已停止....无法再次停止该状态机");
        }
        leaveFunc[nowState]();
        isPlaying = false;
        nowState = "none";
        lastState = "none";
        Debug.Log("状态机已经停止");
    }

    // 获得当前状态名
    public string GetState()
    {
        return nowState;
    }

    // 获得状态机名称
    public string GetName()
    {
        return machineName;
    }

    // 获得当前工作状态
    public bool IsWorking()
    {
        return isPlaying;
    }

    // 获得上一个状态
    public string GetLastState()
    {
        return lastState;
    }

    public string GetDefaultState()
    {
        return defaultState;
    }
}
