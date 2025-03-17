using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RegenerateManager : MonoBehaviour
{
    private static RegenerateManager _instance;
    public static RegenerateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("RegenerateManager").AddComponent<RegenerateManager>();
            }
            return _instance;
        }
    }
    private RegenerateAttribute _regenerateAttribute;
    public RegenerateAttribute RegenerateAttribute
    {
        get { return _regenerateAttribute; }
        set { _regenerateAttribute = value; }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
