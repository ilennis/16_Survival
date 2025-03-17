using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAmount : MonoBehaviour
{
    public Regenerate regenerate;
    public int areaCode;
    
    private void OnDestroy()
    {
        if(areaCode == 1001)
        {
            regenerate.resourceAmountA -= 1;
        }
        else if(areaCode == 1002)
        {
            regenerate.resourceAmountB -= 1;
        }
        else if(areaCode == 1003)
        {
            regenerate.resourceAmountC -= 1;
        }
    }

}
