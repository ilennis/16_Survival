using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool IsClear
    {
        set
        {
            if (value)
            {
                SceneManager.LoadScene("ClearScene");
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
