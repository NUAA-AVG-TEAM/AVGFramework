using Excel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System;
using LitJson;
using UnityEngine;
using System.Diagnostics;
using System.Reflection;
using System.Text;

public class PerExcel
{
    public string excelName;
    public string type;
    public int colNum;
    public int rowNum;
    public dynamic pack;

    // 目前支持4种类型：string, bool, int, double
    public PerExcel(string _excelName, DataRowCollection _pack, string _type, int _colNum, int _rowNum)
    {
        excelName = _excelName;
        type = _type.ToLower();
        colNum = _colNum;
        rowNum = _rowNum - 3;
        // UnityEngine.Debug.Log(colNum);
        // UnityEngine.Debug.Log(rowNum);
        // 创建一个新的字典
        if (_type.ToLower() == "dict")
        {
            pack = new Dictionary<string, Hashtable>();
            for (int i = 3; i < _rowNum; i++)
            {
                Hashtable row = new Hashtable();
                for (int j = 0; j < _colNum; j++)
                {
                    row.Add(_pack[1][j], GetValue(_pack[0][j].ToString(), _pack[i][j]));
                }
                if (pack.ContainsKey(_pack[i][0].ToString()))
                {
                    UnityEngine.Debug.LogError("键重复！！请修改键！！重复键名：" + _pack[i][0]);
                }
                pack.Add(_pack[i][0].ToString(), row);
            }
        }
        else if (_type.ToLower() == "vector")
        {
            pack = new List<Hashtable>();
            // 插4个空，使目前的下标和excel表里的下标相同
            for(int i = 0; i < 4; i++)
            {
                Hashtable row = new Hashtable();
                pack.Add(row);
            }
            for (int i = 3; i < _rowNum; i++)
            {
                Hashtable row = new Hashtable();
                for (int j = 0; j < _colNum; j++)
                {
                    row.Add(_pack[1][j], GetValue(_pack[0][j].ToString().ToLower(), _pack[i][j]));
                }
                pack.Add(row);
            }
        }
        else
        {
            UnityEngine.Debug.LogError("Init配表type发生错误" + "错误类型:" + _type);
        }
    }
    dynamic GetValue(string _type, dynamic _value)
    {
        if (_type == "int")
        {
            return Convert.ToInt32(_value.ToString());
        }
        else if (_type == "double")
        {
            return Convert.ToDouble(_value.ToString());
        }
        else if (_type == "string")
        {
            return _value.ToString();
        }
        else if (_type == "bool")
        {
            return Convert.ToBoolean(_value.ToString());
        }
        else if (_type == "hashtable")
        {
            string json = _value.ToString();
            Hashtable jd = JsonMapper.ToObject<Hashtable>(json);
            foreach (DictionaryEntry de in jd)
            {
                UnityEngine.Debug.Log(de.Value);
            }
            return jd;
        }
        else
        {
            UnityEngine.Debug.LogError("目前不能转换成" + _value.ToString() + "类型的数据，请检查配表");
            return "error";
        }
    }
}

public class AllExcels
{
    public Dictionary<string, PerExcel> allExcels;
    public AllExcels()
    {
        allExcels = new Dictionary<string, PerExcel>();
    }

}


