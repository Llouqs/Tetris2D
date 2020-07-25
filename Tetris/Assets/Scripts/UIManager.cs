using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Color UIColor;
    public Color UIFieldColor;
    public Font UIFont;
    public Text[] UIElements;

    public void Start()
    {
        GameObject tetrisField = GameObject.Find("TetrisField");
        tetrisField.GetComponent<Image>().color = UIFieldColor;
        UIColor.a = 1;
        foreach (Text element in UIElements)
        {
            element.font = UIFont;
            element.color = UIColor;
        }
    }
    public void UpdateScores(int scores, int rows)
    {
        UIElements[0].text = scores.ToString();
        UIElements[1].text = rows.ToString();
    }

    public void UpdateSpeed(float speed)
    {
        UIElements[2].text = speed.ToString("0");
    }
}
