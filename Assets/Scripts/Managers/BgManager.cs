using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour
{
    private static BgManager instance;

    public static BgManager GetInstance
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

    public void ChangeBg(string _newBg)
    {

    }

    public void MoveBg(int _type)
    {

    }

    public void DeleteBg()
    {

    }


}
