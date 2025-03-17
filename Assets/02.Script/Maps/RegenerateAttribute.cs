using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerateAttribute : MonoBehaviour
{
    public OneDayCycle dayCycle;
    //public Regenerate regenerate;

    private void Awake()
    {
        RegenerateManager.Instance.RegenerateAttribute = this;
    }
}
