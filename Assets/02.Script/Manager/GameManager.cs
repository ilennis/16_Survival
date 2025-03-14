using Unity.VisualScripting;
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
                instance.AddComponent<DataManager>();
            }
            return instance;
        }
    }

    private static GameManager instance;

    public Player Player { get; set; }

    public Inventory Inventory { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
