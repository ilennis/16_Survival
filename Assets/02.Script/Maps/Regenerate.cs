using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : MonoBehaviour
{
    [Header("RegenerateArea")]
    [SerializeField] private List<Bounds> regenerateAreaA;
    [SerializeField] private List<Bounds> regenerateAreaB;
    [SerializeField] private List<Bounds> regenerateAreaC;
    [SerializeField] private List<Bounds> regenerateAreaD;
    [SerializeField] private List<GameObject> regenerateResource;

    public int resourceAmountA;
    public int resourceAmountB;
    public int resourceAmountC;
    public int resourceAmountD;

    void Respwon()
    {

    }

    private void OnDrawGizmosSelected()
    {
        if (regenerateAreaA == null)
        {
            return;
        }
        if (regenerateAreaB == null)
        {
            return;
        }
        if (regenerateAreaC == null)
        {
            return;
        }
        if (regenerateAreaD == null)
        {
            return;
        }
        foreach (var area in regenerateAreaA)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(area.center, area.size);
        }
        foreach (var area in regenerateAreaB)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(area.center, area.size);
        }
        foreach (var area in regenerateAreaC)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(area.center, area.size);
        }
        foreach (var area in regenerateAreaD)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(area.center, area.size);
        }
    }
}
