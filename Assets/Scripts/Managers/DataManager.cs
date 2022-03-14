using UnityEngine;
using System;
using System.IO;
public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static SaveData saveData;//当前存档的信息
    public static DataManager GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    /*public void testSave()
    {
        SaveData[] test1 = new SaveData[2];
        SaveData test = new SaveData();
        test.index = 1;
        test.nowDate = DateTime.Now.ToString();
        test.dialog = "Hello World!";
        test.sceneIndex = 1;
        test.bgID = "bg_path";
        test1[0] = test;
        test1[1] = test;
        saveData = test1;
        print(JsonUtility.ToJson(saveData[0]));
        ChangeSaveData(saveData);
    }*/
    public SaveData GetSave()
    {
        return saveData;
    }
    public void saveDate_Init()
    {
        var path = Path.Combine(Application.persistentDataPath, "saveFile_last");
        try
        {
            //读取最近一次存档，最近一次存档与最后一次save结果一致
            var json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        catch
        {
            //若为第一次读取存档，则设置saveData为初始值
            SaveData test = new SaveData();
            test.index = 1;
            test.nowDate = DateTime.Now.ToString();
            test.dialog = "Hello World!";
            test.sceneIndex = 1;
            test.bgID = "bg_path";
            saveData = test;
        }
    }
    //将数据读进savedata里
    public void loadFile(string path)
    {
        try
        {
            var json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
            print(json);
        }
        catch
        {
            Debug.LogError("Load Failed");
        }
    }  
    //存档
    public void saveFile(SaveData data,string path)
    {
        var json = JsonUtility.ToJson(data);
        try
        {
            File.WriteAllText(path,json);
            Debug.Log("Save Successfully");
        }
        catch 
        {
            Debug.LogError("Save Failed");
        }
    }

}
