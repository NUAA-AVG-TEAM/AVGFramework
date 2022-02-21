using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 协程 管理器 ， 管理全局协程事件
/// </summary>
namespace CoroutineManagement
{
    public class CoroutineManager
    {
        /// <summary>
        /// 内部类 Mono 
        /// 因为需要一个继承自 MonoBehaviour 的类才能开启协程，此处使用 mono 作为实现工具
        /// </summary>
        private class Mono : MonoBehaviour { }
        private static Mono mono;

        /// <summary>
        /// 实现为 单例
        /// </summary>
        private static CoroutineManager instance;
        private CoroutineManager()
        {
            GameObject gameObj = new GameObject("Coroutine");
            GameObject.DontDestroyOnLoad(gameObj);
            mono = gameObj.AddComponent<Mono>();
        }
        public static CoroutineManager GetInstance()
        {
            if (instance == null)
            {
                instance = new CoroutineManager();
            }
            return instance;
        }

        // 功能函数
        /// <summary>
        /// 开始协程
        /// </summary>
        /// <param name="routine">协程函数</param>
        /// <returns></returns>
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            if (routine == null)
                return null;
            return mono.StartCoroutine(routine);
        }

        //终止协程
        public void StopCoroutine(ref Coroutine routine)
        {
            if (routine != null)
            {
                mono.StopCoroutine(routine);
                routine = null;
            }
        }

       public void StopAllCoroutine()
       {
            mono.StopAllCoroutines();
       }
    }
}

