using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    private void Awake()
    {
        GameManager.Instance.Player = this;
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
    }
}
