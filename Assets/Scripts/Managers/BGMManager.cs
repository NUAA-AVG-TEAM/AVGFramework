using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Excel;
using System.Data;
using System.IO;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;
    private DataSet BGMExcel = null; //BGM表较小，初始化时读取
    private AudioSource BGMPlayer = null;
    private static int BGM_volume = 0;

    public static BGMManager GetInstance
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
        GameObject camera = GameObject.Find("Main Camera");
        AudioSource BGMPlayer = camera.AddComponent<AudioSource>();
        //读取BGM表备用
        string Path = Application.dataPath + "/Excels/" + "BGM.xlsx";//BGM表所在路径
        DataSet BGMExcel = ReadExcel(Path);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBGM(string _name)
    {
        int rowNum = 0;
        rowNum = BGMExcel.Tables[0].Rows.Count;
        for (int i = 3; i < rowNum; i++)
        {
            string nvalue = BGMExcel.Tables[0].Rows[i][0].ToString();
            if(nvalue == _name)
            {
                string BGMPath = BGMExcel.Tables[0].Rows[i][1].ToString();
                AudioClip clip = Resources.Load<AudioClip>(BGMPath);//加载AudioClip资源文件
                BGMPlayer.clip = clip;
                BGMPlayer.loop = true; //循环播放BGM
                BGMPlayer.playOnAwake = true;//再次唤醒时播放声音
                BGMPlayer.spatialBlend = 0.0f;//设置为2D声音
                BGMPlayer.volume = BGM_volume;//设置音量
                BGMPlayer.enabled = true;
                BGMPlayer.Play();
                break;
            }
        }
    }

    public void StopBGM(string _name)
    {
        BGMPlayer.Stop();
    }

    public void SetVolume(int _volume)
    {
        BGM_volume = _volume;
    }

    static DataSet ReadExcel(string filePath)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        return result;
    }
}
