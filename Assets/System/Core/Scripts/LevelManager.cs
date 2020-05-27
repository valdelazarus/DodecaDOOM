
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    string sceneToLoad;
    int indexToLoad;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "Game")
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            PlayerPrefs.SetInt("Score", 0);
        }
    }

    //load scene based on scene name
    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        anim.SetTrigger("loadScene");
    }

    //load scene based on build index
    public void LoadScene(int buildIndex)
    {
        indexToLoad = buildIndex;
        sceneToLoad = "";
        anim.SetTrigger("loadScene");
    }
    
    //load selected scene - animation event in fade panel animation 
    public void LoadSelectedScene()
    {
        if (sceneToLoad != "")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            SceneManager.LoadScene(indexToLoad);
        }
    }

    //load next scene based on build index
    public void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene(nextScene);
    }

    //quit app
    public void QuitGame()
    {
        Application.Quit();
    }

    
}
