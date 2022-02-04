using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    private static SaveData[] saveData;
    private static SaveData[] qSaveData;
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

    public SaveData[] GetSave()
    {
        return saveData;
    }

    public SaveData[] GetQSave()
    {
        return qSaveData;
    }

    public void ChangeSaveData(SaveData[] _data)
    {

    }

    public void ChangeQSaveData(SaveData[] _data)
    {

    }

}
