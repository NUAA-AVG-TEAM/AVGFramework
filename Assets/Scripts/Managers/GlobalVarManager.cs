using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class GlobalVarManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GlobalVarManager instance;
    public GlobalVarManager GetInstance
    {
        get { return instance; }
    }
    private GlobalData data; 
    private void Awake()
    {
        // 读取本地存档，如果没有的话就新建一个空json文件
        RestoreData();

    }
    
    public void Finished()
    {
        data.isFinish = true;
    }

    // 设置DialogID
    public void SetDialogID(string ID)
    {
        if(!data.dialogList.ContainsKey(ID))
        {
            data.dialogList.Add(ID, 0);
        }
        else
        {
            data.dialogList[ID]++;
        }
    }

    // 返回该Dialog是否是第一次读取
    public bool IsReadDialog(string ID)
    {
        if (!data.dialogList.ContainsKey(ID))
        {
            return false;
        }
        else
        {
            return data.dialogList[ID] > 0;
        }
    }
    
    public void SetCG(int _index)
    {
        if (data.cgList.Contains(_index))
        {
            return;
        }
        else
        {
            data.cgList.Add(_index);
        }
    }

    // 返回目前CG表的引用
    public HashSet<int> GetCGList()
    {
        HashSet<int> res = FunctionUtil.DeepCopyByReflection<HashSet<int>>(data.cgList);
        return res;
    }
    void Start()
    {
        
    }

    // 存档，后面的_data改成默认的存档~
    public void SaveData(int _type = 1)
    {
        string tmp;
        if (_type == 0)
        {
            tmp = "";
        }
        else
        {
            tmp = JsonMapper.ToJson(data);
            
        }
        Debug.Log("存档内容为:");
        Debug.Log(tmp);
        string tempPath = Application.persistentDataPath + "/Save/temp.json";
        FileStream file = File.Open(tempPath, FileMode.OpenOrCreate);
        StreamWriter sw = new StreamWriter(file);
        sw.WriteLine(tmp);
        Debug.Log("存档成功!");
        sw.Close();
        file.Close();


    }
    // 读档
    void RestoreData()
    {
        Debug.Log("准备读取存档....");
        string tempPath = Application.persistentDataPath + "/Save/temp.json";
        if (!File.Exists(tempPath))
        {
            SaveData(0);
        }
        Debug.Log("1");

        StreamReader sr = new StreamReader(tempPath);
        string _data = sr.ReadToEnd();
        Debug.Log(data);
        data = JsonUtility.FromJson<GlobalData>(_data);
        Debug.Log("读取存档成功!");
        sr.Close();
    }
}
