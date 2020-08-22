using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    private static string _currentScene = "Level1";
    public Text timerStart;
    public GameObject[] gameManagers;

    private void Start()
    {
        foreach (var obj in gameManagers)
        {
            obj.GetComponent<GameManager>().enabled = false;
        }
        StartCoroutine("TimerAgain");
    }

    // public void PlayAgain()
    // {
    //     timerStart.text = "3";
    //     InvokeRepeating("PlayAgainTimer", 10, 100);
    //     SceneManager.LoadScene(_currentScene);
    // }    
    public void PlayAgain()
    {
        SceneManager.LoadScene(_currentScene);
    }


    private IEnumerator TimerAgain()
    {
        for (var i = 2; i >= 0; i--)
        {
            yield return new WaitForSeconds(0.5f);
            timerStart.text = i.ToString();
            if (i == 0) {
                timerStart.text = "";
                foreach (var obj in gameManagers)
                {
                    obj.GetComponent<GameManager>().enabled = true;
                }
            }
        }
    }
    
    private void PlayAgainTimer()
    {
        var tmp = int.Parse(timerStart.text) - 1;
        timerStart.text = tmp.ToString();
    }

    public void OnePlayer()
    {
        _currentScene = "Level1";
        SceneManager.LoadScene("Level1");
    }
    public void TwoPlayer()
    {
        _currentScene = "Level2";
        SceneManager.LoadScene("Level2");
    }
    public void ThreePlayer()
    {
        _currentScene = "Level3";
        SceneManager.LoadScene("Level3");
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
