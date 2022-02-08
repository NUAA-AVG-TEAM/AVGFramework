using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGUI : MonoBehaviour
{
    private static QuitGUI instance;
    

    public static QuitGUI GetInstance
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
}
