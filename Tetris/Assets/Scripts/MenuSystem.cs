using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public GameObject TetrisWindowPrefab;
    public void PlayAgain()
    {
        SceneManager.LoadScene("Level");
    }

    public void CreateNewTetris()
    {
        Instantiate(TetrisWindowPrefab, transform.position, Quaternion.identity).transform.SetParent(GameObject.Find("Canvas").transform);
    }
}
