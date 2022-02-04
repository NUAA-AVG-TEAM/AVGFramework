using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private static SpriteManager instance;

    public static SpriteManager GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void CreateSprite(string _name, string _picName, Hashtable _params, bool _isSkip)
    {

    }

    private void DeleteSprite(string _name, bool _isSkip)
    {

    }

    private void MoveSprite(string[] _name, int _type, Hashtable _params, bool _isSkip)
    {

    }

    private void ChangeSprite(string[] _name, string[] _picName, Hashtable _params, double _doutTime = 0.3, bool _isSkip = false)
    {

    }
}
