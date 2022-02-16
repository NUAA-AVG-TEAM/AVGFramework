using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoroutineManagement;

namespace PanelDisplayManagement
{
    /// <summary>
    /// 管理当前正在显示的Panel,基于dic和stack
    /// 一个panel = .prefab(=>GameObj) + .cs(=>BasePanel = name + path + event)
    /// </summary>
    public class PanelDisplayManager
    {
        /// <summary>
        /// 存放所有的panel的GameObj,以name为key
        /// </summary>
        private Dictionary<string, GameObject> panelsDic;
        /// <summary>
        /// 当前显示panel的obj的显示逻辑顺序
        /// </summary>
        private Stack<BasePanel> panelsStack;

        private GameObject targetGUI;

        /// <summary>
        /// 实现为单例
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
        /// 设置渲染目标GUI
        /// </summary>
        /// <param name="targetGUIName">目标GUI的名字</param>
        public void SetTartGUI(GameObject targetGUI)
        {
            this.targetGUI = targetGUI;
        }


        /// <summary>
        /// 在 dic 中尝试寻找 panelName 对应的项， 如果有 ，返回 gameObj , 如果没有 ， 返回 空
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public GameObject GetPanel(string panelName)
        {
            // 前置条件检查
            if (targetGUI == null)
            {
                Debug.LogError("targetGUI do not exist!");
                return null;
            }
            if (string.IsNullOrEmpty(panelName))
            {
                return null;
            }

            // 如果有，直接返回
            if (panelsDic.ContainsKey(panelName))
            {
                return panelsDic[panelName];
            }
            // 如果没有，返回空
            else
            {
                Debug.LogWarning($"try to get a panel : {panelName} that  not exist");
                return null;
            }
        }

        /// <summary>
        /// 在 dic 中新加入一个 panel , 如果其不存在，在 dic 中加入记录，并新建其对应的 obj
        /// </summary>
        /// <param name="panelName">panel名称</param>
        private void AddPanel(string panelName)
        {
            // 前置条件检查
            if (targetGUI == null)
            {
                Debug.LogError("targetGUI do not exist!");
            }
            if (string.IsNullOrEmpty(panelName))
            {
                Debug.LogError($"AddPanel with name null or empty");
            }

            // 如果没有，新加入
            if (!panelsDic.ContainsKey(panelName))
            {
                Debug.Log($"path : {PNConvert.ToFabPath(panelName)}");
                GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>(PNConvert.ToFabPath(panelName)), targetGUI.transform);
                panelsDic.Add(panelName, panelObj);
            }
        }

        /// <summary>
        /// 在 dic 中删除一个对应的 panel
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
        /// 添加一个显示panel
        /// 
        /// 每一次的PUSH，相当于开启一次协程的时间处理,PUSH = PAUSE + SHOW
        /// </summary>
        /// <param name="newPanel">要显示的panel</param>
        public IEnumerator Push(BasePanel newPanel)
        {
            Debug.Log("show");
            // 记录 新panel , 创建与之对应的 GameObj
            AddPanel(newPanel.name);
            newPanel.panelObj = GetPanel(newPanel.name);

            // 新 panel 入栈
            if (panelsStack.Count > 0)
            {
                yield return CoroutineManager.GetInstance().StartCoroutine(panelsStack.Peek().OnPause());
            }
            panelsStack.Push(newPanel);
            yield return CoroutineManager.GetInstance().StartCoroutine(newPanel.OnShow());
            yield break;
        }

        /// <summary>
        /// 弹出当前栈顶 panel
        /// </summary>
        public IEnumerator Pop()
        {
            if (panelsStack.Count > 0)
            {
                // 栈顶出栈 = pop + onremove + if only then delete in dic
                BasePanel panel = panelsStack.Pop();
                yield return CoroutineManager.GetInstance().StartCoroutine(panel.OnRemove());
                // 如果该 panel 对应的 obj 在 显示栈 未有使用了，从 dic 中删除其 obj
                if (!panelsStack.Contains(panel))
                {
                    DeletePanel(panel.name);
                }
                // 下一恢复
                if (panelsStack.Count > 0)
                {
                    CoroutineManager.GetInstance().StartCoroutine(panelsStack.Peek().OnResume());
                }
                else// 如果没有下一个了，说明栈中已经没有显示了，此时应该清除 协程
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


