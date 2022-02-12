/// ���̰��¼�
/// �����߼���
/// qutiGUI��ʱ�����м����¼���Ч
/// ��������һ�����й�����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    // mode 0: ���� 1:���� 2: ̧��
    // ���ǵ�unity��α���̣߳�Ӧ�ò����Ч�ʲ���Ӱ��~
    //private Dictionary<KeyCode, Dictionary<string, System.Action>> dict;
    private List<Dictionary<KeyCode, Dictionary<string, System.Action>>> list;

    private static KeyboardManager instance;
    public static KeyboardManager GetInstance
    {
        get { return instance; }
    }

    private int maxMode;
    // Start is called before the first frame update
    private void Awake()
    {
        // ��ʼ��
        instance = this;
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

    // ������Ӧ�����ֵ�= = ���Ǽ���Ҳû��ʲô�»����˾�Ӳд��
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
        // ���Ѱ󶨵�ȫִ��һ��
        if (QuitGUI.GetInstance.gameObject.activeSelf)
        {
            return;
        }
        // ���̰����¼�
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
        // ���Ȳ��� _key �Ƿ�������
        if (_mode >= maxMode || _mode < 0)
        {
            Debug.LogError("��������_mode�������ܴ��ڵ������ģʽ����С��0��");
            return;
        }
        var dict = list[_mode];
        if (dict.ContainsKey(_key))
        {
            // �ٲ�����û����Ӧ���Ƶĺ���

            // �����ֵ�
            Dictionary<string, System.Action> _dict = dict[_key];
            if (_dict.ContainsKey(_name))
            {
                Debug.LogError("�Ѱ���Ӧ���Ƶĺ������뻻��������������һ��QAQ");
                return;
            }
            else
            {
                _dict.Add(_name, _func);
            }
        }
        else
        {
            // ֱ�Ӽ�
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
            Debug.LogError("��������_mode�������ܴ��ڵ������ģʽ����С��0��");
            return;
        }
        var dict = list[_mode];
        // ���Ȳ��� _key �Ƿ�������
        if (dict.ContainsKey(_key))
        {
            // �ٲ�����û����Ӧ���Ƶĺ���

            // �����ֵ�
            Dictionary<string, System.Action> _dict = dict[_key];
            if (_dict.ContainsKey(_name))
            {
                dict[_key].Remove(_name);
                Debug.Log("���ɹ���");
                return;
            }
            else
            {
                Debug.LogWarning("δ�󶨹��˺���~");
                return;
            }
        }
        else
        {
            // ����û�д˺���~
            Debug.LogWarning("δ�󶨹��˺���~");
            return;
        }

    }

    public bool HasConnected(int _mode, KeyCode _key, string _name)
    {
        if (_mode >= maxMode || _mode < 0)
        {
            Debug.LogError("��������_mode�������ܴ��ڵ������ģʽ����С��0��");
            return false;
        }
        // ���Ȳ��� _key �Ƿ�������
        var dict = list[_mode];
        if (dict.ContainsKey(_key))
        {
            // �ٲ�����û����Ӧ���Ƶĺ���

            // �����ֵ�
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
