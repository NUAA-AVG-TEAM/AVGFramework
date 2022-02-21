using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Э�� ������ �� ����ȫ��Э���¼�
/// </summary>
namespace CoroutineManagement
{
    public class CoroutineManager
    {
        /// <summary>
        /// �ڲ��� Mono 
        /// ��Ϊ��Ҫһ���̳��� MonoBehaviour ������ܿ���Э�̣��˴�ʹ�� mono ��Ϊʵ�ֹ���
        /// </summary>
        private class Mono : MonoBehaviour { }
        private static Mono mono;

        /// <summary>
        /// ʵ��Ϊ ����
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

        // ���ܺ���
        /// <summary>
        /// ��ʼЭ��
        /// </summary>
        /// <param name="routine">Э�̺���</param>
        /// <returns></returns>
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            if (routine == null)
                return null;
            return mono.StartCoroutine(routine);
        }

        //��ֹЭ��
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

