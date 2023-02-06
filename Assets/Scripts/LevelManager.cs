using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    bool circularNavigation = true;

    /// <summary>
    /// Returns Current Scene Index
    /// </summary>
    /// <returns>Current Scene Index.</returns>



    public int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public int GetLastScene()
    {
        return SceneManager.sceneCountInBuildSettings - 1;
    }

    /// <summary>
    /// Navigates to First Scene
    /// </summary>

    public void FirstScene()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Navigates to Last Scene
    /// </summary>

    public void LastScene()
    {
        ///SceneManager.GetAllScenes.lenght - 1
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    /// <summary>
    /// Navigates to Next Scene from Current One.
    /// </summary>
    public void NextScene()
    {
        int currentScene = GetCurrentScene();
        int lastScene = GetLastScene();

        // Si la escena actual NO ES LA ULTIMA escena
        if (currentScene < lastScene)
        {
            // Cargue la siguiente escena
            SceneManager.LoadScene(currentScene + 1);
        }

        // Si esta permitido navegar circularmente
        else if (circularNavigation)
        {
            //Cargue la primera escena
            FirstScene();
        }
    }

    /// <summary>
    /// Navigates to Previous Scene from Current One.
    /// </summary>

    public void PreviousScene()
    {
        int currentScene = GetCurrentScene();

        if (currentScene > 0)
        {
            SceneManager.LoadScene(currentScene - 1);
        }
    }

    public void Restart()
    {
        PreviousScene();
    }

    public void Quit()
    {
        FirstScene();
    }

}
