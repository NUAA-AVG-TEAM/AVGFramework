using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private string defaultLang;
    private Dictionary<string, Hashtable> pack;
    private string lang;
    private List<Hashtable> p1;
    private static LanguageManager instance;
    public static LanguageManager GetInstance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
        defaultLang = "CHS";
        pack = ExcelManager.GetExcel("LanguagePack");

        // 这里lang从SettingVarManager读取，后补
        lang = "";
    }

    public void SetLanguage(string _lang)
    {
        Debug.Log("当前语言从" + lang + "->" + _lang);
        lang = _lang;
    }

    public string GetText(string _id)
    {
        /*
        assert(not string.isnilorempty(_id), '[LanguageUtil] 翻译ID为空，请检查策划表和LanguagePack')
        assert(
            Config.LanguagePack[_id],
            string.format('[LanguageUtil] LanguagePack[%s] 不存在对应翻译ID，请检查策划表和LanguagePack', _id)
        )
        local text = Config.LanguagePack[_id][lang]
        -- print(text)
        if string.isnilorempty(text) then
            print(string.format('[LanguageUtil] LanguagePack[%s][%s] 不存在对应语言翻译内容，默认使用中文', _id, lang))
            text = '*'..Config.LanguagePack[_id][defaultLang]
        end
        return text
        */
        if (string.IsNullOrWhiteSpace(_id))
        {
            Debug.LogError("翻译ID为空，请检查策划表和LanguagePack");
        }
        if (!pack.ContainsKey(_id))
        {
            Debug.LogError("尝试索引id为：" + _id + ",但不存在对应翻译ID，请检查策划表和LanguagePack");
        }
        string text = (string)pack[_id][lang];
        if (string.IsNullOrWhiteSpace(text))
        {
            Debug.LogWarning("不存在对应语言翻译内容，默认使用中文, id为：" + _id);
            text = "*" + (string)pack[_id][defaultLang];
        }
        return text;
    }
}
