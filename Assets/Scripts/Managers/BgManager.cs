using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BgManager : MonoBehaviour
{
    private static BgManager instance;
    private Dictionary<string,Hashtable> BgTable;
    public Image Bg;
    public Button Bt;
    public int TargetNum;
    public float TargetTime;
    private string Path;
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
        BgTable = ExcelManager.GetExcel("Bg.xlsx");
        //Tween tween = Bg.Color.DOFade(0.3,2);
        //Tween tween2 = Bg.DOFade(1,2);
        TargetNum = 1;
        TargetTime = 1;
        Path = " ";//需要改成图片文件夹的路径+//
    }

    public IEnumerator ChangeBg(string _newBg)
    {
        yield return new WaitForSeconds(2);
        Bg.DOFade(0,1);
        StartCoroutine(ChangeBgtest(Path+BgTable[_newBg]["Path"]));
        Bg.DOFade(1,1);
    }
    private IEnumerator ChangeBgtest(string path)
    {
        Texture2D _tex = (Texture2D)Resources.Load(path);
        Sprite spr = Sprite.Create(_tex,new Rect(0,0,_tex.width,_tex.height),new Vector2(0.5f,0.5f));
        Bg.sprite = spr;
        yield return spr;
    }
    public void MoveBg(int _type)
    {

    }

    public void DeleteBg()
    {

    }
    /*private void pellUp(){
        Color DeltaColor = new Color(0,0,0,0.2);
        float Ctime;
        Ctime = 0; 
        while(Ctime<TargetTime)
        {
            Ctime += Time.time;
            if(Bg.Color.a<TargetNum)
            {
                Bg.Color += DeltaColor;
            }
        }
    }
    private void pellDonw(){
        Color DeltaColor = new Color(0,0,0,0.2);
        float Ctime;
        Ctime = 0; 
        while(Ctime<TargetTime)
        {
            Ctime += Time.time;
            if(Bg.Color.a>TargetNum)
            {
                Bg.Color -= DeltaColor;
            }
        }
    }*/
}
