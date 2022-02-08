using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
    private static VideoManager instance;
    // 将一个 VideoPlayer 附加到主摄像机。
    

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
    {   GameObject camera = GameObject.Find("Main Camera");
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCameraAlpha = 1;
        videoPlayer.isLooping = false;
        //videoPlayer.canSetTime = true;
    }

    public void PlayVideo(string _name,UnityEngine.Video.VideoPlayer vp)
    {
        vp.url = _name;
        vp.Prepare();
        Debug.Log("Video Prepared and Play");
        vp.Play();
    }

    public void StopVideo(string _name,UnityEngine.Video.VideoPlayer vp)
    {
        vp.Pause();
    }

    public void DeleteVideo(UnityEngine.Video.VideoPlayer vp)
    {
        vp.url = "";
    }

    public void EndVideo(string _name,UnityEngine.Video.VideoPlayer vp)
    {
        vp.time = vp.clip.length - 0.1f;
        vp.Pause();
    }
}
