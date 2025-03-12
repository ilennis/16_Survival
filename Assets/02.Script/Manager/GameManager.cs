using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }

    private static GameManager instance;

    public Player Player { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
