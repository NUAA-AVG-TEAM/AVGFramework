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

    // �������ܺ���

    /// <summary>
    /// ������ȡ�����һ��T���͵����
    /// </summary>
    /// <typeparam name="T">�������</typeparam>
    /// <returns>���</returns>
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
    /// �������Ҷ�Ӧ���Ƶ��Ӷ���
    /// </summary>
    /// <param name="objName">�Ӷ�������</param>
    /// <returns>�Ӷ���</returns>
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
    /// �����������Ӷ����л�ȡ�����һ�����
    /// </summary>
    /// <typeparam name="T">�������</typeparam>
    /// <param name="objName">�Ӷ�������</param>
    /// <returns>���</returns>
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


    // �¼��ڵ㺯��

    public virtual IEnumerator OnShow()
    {
        Debug.Log($"Panel: {name} Show");
        GetOrAddComponent<CanvasGroup>().alpha = 0f;
        /// ʵ�ֵ���Ч�� ///
        // �ȹرյ��
        GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
        // ����
        while (GetOrAddComponent<CanvasGroup>().alpha < 1.0f)
        {
            GetOrAddComponent<CanvasGroup>().alpha += Time.deltaTime * fadeInSpeed;
            // Debug.Log($"alpha: {GetOrAddComponent<CanvasGroup>().alpha}");
            yield return null;
        }
        // ������� 
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
        /// ʵ�ֵ���Ч�� ///
        // �ȹرյ��
        GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
        // ����
        while (GetOrAddComponent<CanvasGroup>().alpha > 0f)
        {
            GetOrAddComponent<CanvasGroup>().alpha -= Time.deltaTime * fadeOutSpeed;
            // Debug.Log($"alpha: {GetOrAddComponent<CanvasGroup>().alpha}");
            yield return null;
        }
    }
}
