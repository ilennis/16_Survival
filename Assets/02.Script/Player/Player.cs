using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    private void Awake()
    {
        GameManager.Instance.Player = this;
        CharacterManager.Instance.Player = this;

        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
