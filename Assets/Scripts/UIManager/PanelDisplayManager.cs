using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoroutineManagement;

namespace PanelDisplayManagement
{
    /// <summary>
    /// ����ǰ������ʾ��Panel,����dic��stack
    /// һ��panel = .prefab(=>GameObj) + .cs(=>BasePanel = name + path + event)
    /// </summary>
    public class PanelDisplayManager
    {
        /// <summary>
        /// ������е�panel��GameObj,��nameΪkey
        /// </summary>
        private Dictionary<string, GameObject> panelsDic;
        /// <summary>
        /// ��ǰ��ʾpanel��obj����ʾ�߼�˳��
        /// </summary>
        private Stack<BasePanel> panelsStack;

        private GameObject targetGUI;

        /// <summary>
        /// ʵ��Ϊ����
        /// </summary>
        private static PanelDisplayManager instance;
        private PanelDisplayManager()
        {
            panelsDic = new Dictionary<string, GameObject>();
            panelsStack = new Stack<BasePanel>();
            targetGUI = null;
        }
        public static PanelDisplayManager GetInstance()
        {
            if (instance == null)
            {
                instance = new PanelDisplayManager();
            }
            return instance;
        }

        /// <summary>
        /// ������ȾĿ��GUI
        /// </summary>
        /// <param name="targetGUIName">Ŀ��GUI������</param>
        public void SetTartGUI(GameObject targetGUI)
        {
            this.targetGUI = targetGUI;
        }


        /// <summary>
        /// �� dic �г���Ѱ�� panelName ��Ӧ��� ����� ������ gameObj , ���û�� �� ���� ��
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public GameObject GetPanel(string panelName)
        {
            // ǰ���������
            if (targetGUI == null)
            {
                Debug.LogError("targetGUI do not exist!");
                return null;
            }
            if (string.IsNullOrEmpty(panelName))
            {
                return null;
            }

            // ����У�ֱ�ӷ���
            if (panelsDic.ContainsKey(panelName))
            {
                return panelsDic[panelName];
            }
            // ���û�У����ؿ�
            else
            {
                Debug.LogWarning($"try to get a panel : {panelName} that  not exist");
                return null;
            }
        }

        /// <summary>
        /// �� dic ���¼���һ�� panel , ����䲻���ڣ��� dic �м����¼�����½����Ӧ�� obj
        /// </summary>
        /// <param name="panelName">panel����</param>
        private void AddPanel(string panelName)
        {
            // ǰ���������
            if (targetGUI == null)
            {
                Debug.LogError("targetGUI do not exist!");
            }
            if (string.IsNullOrEmpty(panelName))
            {
                Debug.LogError($"AddPanel with name null or empty");
            }

            // ���û�У��¼���
            if (!panelsDic.ContainsKey(panelName))
            {
                Debug.Log($"path : {PNConvert.ToFabPath(panelName)}");
                GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>(PNConvert.ToFabPath(panelName)), targetGUI.transform);
                panelsDic.Add(panelName, panelObj);
            }
        }

        /// <summary>
        /// �� dic ��ɾ��һ����Ӧ�� panel
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        private bool DeletePanel(string panelName)
        {
            if (panelsDic.ContainsKey(panelName))
            {
                GameObject.Destroy(panelsDic[panelName]);
                panelsDic.Remove(panelName);
                return true;
            }
            else
            {
                Debug.LogWarning($"try to delete Panel : {panelName} is not exist");
                return false;
            }
        }

        /// <summary>
        /// ���һ����ʾpanel
        /// 
        /// ÿһ�ε�PUSH���൱�ڿ���һ��Э�̵�ʱ�䴦��,PUSH = PAUSE + SHOW
        /// </summary>
        /// <param name="newPanel">Ҫ��ʾ��panel</param>
        public IEnumerator Push(BasePanel newPanel)
        {
            Debug.Log("show");
            // ��¼ ��panel , ������֮��Ӧ�� GameObj
            AddPanel(newPanel.name);
            newPanel.panelObj = GetPanel(newPanel.name);

            // �� panel ��ջ
            if (panelsStack.Count > 0)
            {
                yield return CoroutineManager.GetInstance().StartCoroutine(panelsStack.Peek().OnPause());
            }
            panelsStack.Push(newPanel);
            yield return CoroutineManager.GetInstance().StartCoroutine(newPanel.OnShow());
            yield break;
        }

        /// <summary>
        /// ������ǰջ�� panel
        /// </summary>
        public IEnumerator Pop()
        {
            if (panelsStack.Count > 0)
            {
                // ջ����ջ = pop + onremove + if only then delete in dic
                BasePanel panel = panelsStack.Pop();
                yield return CoroutineManager.GetInstance().StartCoroutine(panel.OnRemove());
                // ����� panel ��Ӧ�� obj �� ��ʾջ δ��ʹ���ˣ��� dic ��ɾ���� obj
                if (!panelsStack.Contains(panel))
                {
                    DeletePanel(panel.name);
                }
                // ��һ�ָ�
                if (panelsStack.Count > 0)
                {
                    CoroutineManager.GetInstance().StartCoroutine(panelsStack.Peek().OnResume());
                }
                else// ���û����һ���ˣ�˵��ջ���Ѿ�û����ʾ�ˣ���ʱӦ����� Э��
                {
                    CoroutineManager.GetInstance().StopAllCoroutine();
                }
            }
            yield break;
        }

        public IEnumerator PopAll()
        {
            while (panelsStack.Count > 0)
            {
                yield return CoroutineManager.GetInstance().StartCoroutine(instance.Pop());
            }
            yield break;
        }
    }
}        


