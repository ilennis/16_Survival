using UnityEngine;
using UnityEngine.SceneManagement;

public class StorySceneController : MonoBehaviour
{
    private void Start()
    {
        Invoke("MoveMainScene", 10.0f);         //10초후에 메인씬으로 이동
        TriggerViewStory();         //스토리를 읽었다는 트리거 발동 
    }

    private void MoveMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void TriggerViewStory()
    {
        PlayerPrefs.SetInt("isStoryViewed", 1);         //스토리를 읽었다는 정보를 저장
        PlayerPrefs.Save();
    }
}
