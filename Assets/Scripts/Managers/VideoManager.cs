using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
    private static VideoManager instance;

    public static VideoManager GetInstance
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

    public void PlayVideo(string _name)
    {

    }

    public void StopVideo(string _name)
    {

    }

    public void DeleteVideo()
    {

    }

    public void EndVideo(string _name)
    {

    }
}
