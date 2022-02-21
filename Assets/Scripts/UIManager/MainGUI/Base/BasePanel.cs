using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoroutineManagement;

public class BasePanel
{
    public string name;

    public string fabPath;

    public GameObject panelObj = null;

    public BasePanel(string fabPath)
    {
        this.fabPath = fabPath;
        this.name = PNConvert.ToName(fabPath);
    }

    public float fadeInSpeed = 1.0f;
    public float fadeOutSpeed = 1.0f;

    // 辅助功能函数

    /// <summary>
    /// 在面板获取或添加一个T类型的组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <returns>组件</returns>
    public T GetOrAddComponent<T>() where T : Component
    {
        Debug.Assert(panelObj != null);

        if (panelObj.GetComponent<T>() == null)
        {
            panelObj.AddComponent<T>();
        }

        return panelObj.GetComponent<T>();
    }

    /// <summary>
    /// 在面板查找对应名称的子对象
    /// </summary>
    /// <param name="objName">子对象名称</param>
    /// <returns>子对象</returns>
    public GameObject FindFirstChildGameObject(string objName)
    {
        Debug.Assert(panelObj != null);

        Transform[] trans = panelObj.GetComponentsInChildren<Transform>();
        foreach (Transform t in trans)
        {
            if (t.name == objName)
            {
                return t.gameObject;
            }
        }

        Debug.LogWarning($"{panelObj.name} has no subObj named {objName}");
        return null;
    }

    /// <summary>
    /// 根据名称在子对象中获取或添加一个组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="objName">子对象名称</param>
    /// <returns>组件</returns>
    public T GetOrAddComponetInChild<T>(string objName) where T : Component
    {
        Debug.Assert(panelObj != null);

        GameObject child = FindFirstChildGameObject(objName);
        if (child != null)
        {
            if (child.GetComponent<T>() == null)
            {
                child.AddComponent<T>();
            }

            return child.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }


    // 事件节点函数

    public virtual IEnumerator OnShow()
    {
        Debug.Log($"Panel: {name} Show");
        GetOrAddComponent<CanvasGroup>().alpha = 0f;
        /// 实现淡入效果 ///
        // 先关闭点击
        GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
        // 淡入
        while (GetOrAddComponent<CanvasGroup>().alpha < 1.0f)
        {
            GetOrAddComponent<CanvasGroup>().alpha += Time.deltaTime * fadeInSpeed;
            // Debug.Log($"alpha: {GetOrAddComponent<CanvasGroup>().alpha}");
            yield return null;
        }
        // 开启点击 
        GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public virtual IEnumerator OnPause()
    {
        Debug.Log($"Panel: {name} Pause");
        GetOrAddComponent<CanvasGroup>().interactable = false;
        yield return null;
    }

    public virtual IEnumerator OnResume()
    {
        Debug.Log($"Panel: {name} Resume");
        GetOrAddComponent<CanvasGroup>().interactable = true;
        yield return null;
    }

    public virtual IEnumerator OnRemove()
    {
        Debug.Log($"Panel: {name} Remove");
        /// 实现淡出效果 ///
        // 先关闭点击
        GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
        // 淡出
        while (GetOrAddComponent<CanvasGroup>().alpha > 0f)
        {
            GetOrAddComponent<CanvasGroup>().alpha -= Time.deltaTime * fadeOutSpeed;
            // Debug.Log($"alpha: {GetOrAddComponent<CanvasGroup>().alpha}");
            yield return null;
        }
    }
}
