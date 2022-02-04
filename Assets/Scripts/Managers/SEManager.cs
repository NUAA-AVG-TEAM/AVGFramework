using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    private static SEManager instance;

    public static SEManager GetInstance
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySE(string _name, bool _isSkip)
    {

    }

    public void StopSE(string _name)
    {

    }

    public void SetVolume(int _volume)
    {

    }
}
