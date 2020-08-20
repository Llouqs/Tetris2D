using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Color uiColor;
    public Font uiFont;
    public Text[] uiElements;
    public void Start()
    {
        uiColor.a = 1;
        foreach (var element in uiElements)
        {
            element.font = uiFont;
            element.color = uiColor;
        }
    }
    public void UpdateScores(int scores, int rows)
    {
        uiElements[0].text = scores.ToString();
        uiElements[1].text = rows.ToString();
    }

    public void UpdateSpeed(float speed)
    {
        uiElements[2].text = speed.ToString("0");
    }
}
