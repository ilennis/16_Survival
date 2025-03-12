using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.Player = this;
    }
}
