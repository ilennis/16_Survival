using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor; // SceneAsset 사용하려면 필요
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public SceneAsset scene;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (PlayerPrefs.GetInt("isStoryViewed") == 1)
            {
                SceneManager.LoadScene(scene.name);
            }
            else
            {
                SceneManager.LoadScene("StoryScene");
            }
        }
    }
}
