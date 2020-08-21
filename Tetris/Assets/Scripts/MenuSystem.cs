using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("Level");
    }
}
