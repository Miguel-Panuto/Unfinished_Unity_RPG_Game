using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    public void CallNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenSpecificScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
