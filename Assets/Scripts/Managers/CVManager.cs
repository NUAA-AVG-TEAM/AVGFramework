using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVManager : MonoBehaviour
{
    private static CVManager instance;

    public static CVManager GetInstance
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

    public void PlayCV(string _name, bool _isSkip)
    {

    }

    public void StopCV(string _name)
    {

    }

    public void SetVolume(int _name, int _volume)
    {

    }
}
