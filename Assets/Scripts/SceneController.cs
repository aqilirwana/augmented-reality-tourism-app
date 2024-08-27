using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnLanguageSelected(int index)
    {
        switch (index)
        {
            case 0: // English
                SceneManager.LoadScene("Home"); // Replace with your English scene name
                Debug.Log("Home");
                break;
            case 1: // Arabic
                SceneManager.LoadScene("Home - Arabic"); // Replace with your Arabic scene name
                Debug.Log("Home - Arabic");
                break;
            default:
                break;
        }
    }
}
