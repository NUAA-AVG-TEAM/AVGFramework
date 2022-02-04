using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Excel;
using System.Data;
using System.IO;
using System;
using LitJson;
public class ExcelManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static AllExcels allExcels;

    /// <summary>
    /// 引用时 ExcelManager.GetExcel
    /// </summary>
    public static dynamic GetExcel(string _name)
    {
        if (!allExcels.allExcels.ContainsKey(_name))
        {
            Debug.LogError("该配表未初始化！,请检查Init表或参数名称,配表名：" + _name);
        }
        Debug.Log("获得表：" + _name + "表的类型为" + allExcels.allExcels[_name].type);
        return allExcels.allExcels[_name].pack;
    }

    private void Awake()
    {
        /// 目前采取读总配表的方式初始化所有配表
        string root = Application.dataPath + "/Excels/" + "Init.xlsx";
        Debug.Log(root);
        /// 开始读Init表
        int columnNum = 0, rowNum = 0;//excel 行数 列数
        DataRowCollection collect = ReadExcel(root, ref columnNum, ref rowNum);
        allExcels = new AllExcels();
        Debug.Log(rowNum);
        Debug.Log(columnNum);
        for (int i = 1; i < rowNum; i++)
        {
            if (IsEmptyRow(collect[i], columnNum))
            {
                continue;
            }
            PerExcel excel = Read((Application.dataPath + "/" + collect[i][0].ToString()), collect[i][1].ToString(), collect[i][2].ToString());
            Debug.Log(excel);
            allExcels.allExcels.Add(collect[i][1].ToString(), excel);
        }
    }
    static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        //Tables[0] 下标0表示excel文件中第一张表的数据
        // Debug.Log(columnNum);
        columnNum = result.Tables[0].Columns.Count;
        rowNum = result.Tables[0].Rows.Count;
        return result.Tables[0].Rows;
    }

    static PerExcel Read(string _filePath, string _className, string _type)
    {

        int columnNum = 0, rowNum = 0;//excel 行数 列数
        DataRowCollection collect = ReadExcel(_filePath, ref columnNum, ref rowNum);
        UnityEngine.Debug.Log(_className + "初始化完毕！");
        PerExcel table = new PerExcel(_className, collect, _type, columnNum, rowNum);
        return table;

    }
    bool IsEmptyRow(DataRow collect, int columnNum)
    {
        for (int i = 0; i < columnNum; i++)
        {
            if (!collect.IsNull(i)) return false;
        }
        return true;
    }
}
