using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSceneController : MonoBehaviour
{
    private void Start()
    {
        //10초후에 시작씬으로 이동
        Invoke("MoveTitleScene",10.0f);
    }

    private void MoveTitleScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
