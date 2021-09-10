using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{

    public List<Button> levels;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }
       
        LockOpen();
    }
    public void LockOpen()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("level"); i++)
        {
            levels[i].interactable = true;
        }
    }

    public string levelID(int id)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }
    public void LevelOpen(int id)
    {
        SceneManager.LoadScene(levelID(id));
    }
    // clear all prefs
    public void ClearPref() 
    {
        PlayerPrefs.DeleteKey("level");
        SceneManager.LoadScene(1);
    }
}
