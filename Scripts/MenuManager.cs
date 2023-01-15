using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class MenuManager : MonoBehaviour
{
    public List<GameObject> enabledObjects;
    public GameObject disableObjects;
    public void GoToScene(string _level)
    {
        Debug.Log($"Going to scene: {_level}!");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(_level);
        Time.timeScale = 1.0f;
    }

    public void CloseGame()
    {
        Debug.Log("Closing Game!");
        Application.Quit();
    }

    public void unPause()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void startScene()
    {
        for (int i = 0; i < enabledObjects.Count; i++)
        {
            enabledObjects[i].SetActive(true);
            disableObjects.SetActive(false);
        }
    }
}