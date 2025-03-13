using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : MonoBehaviour
{
    [Header("RegenerateArea")]
    [SerializeField] private List<Bounds> regenerateAreaA;
    [SerializeField] private List<GameObject> regenerateResource;
    [SerializeField] private Color gizmoColor =new Color(1,0,0,0.3f);

    private void OnDrawGizmosSelected()
    {
        if(regenerateAreaA == null)
        {
            return;
        }
        Gizmos.color = gizmoColor;
        foreach(var area in regenerateAreaA)
        {
            Gizmos.DrawWireCube(area.center, area.size);
        }
    }
}
