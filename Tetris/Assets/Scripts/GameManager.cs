using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] Shapes;
    public Text Scores;
    public Text GameSpeed;
    public Text NumberRows;
    private GameObject shape;
    private Vector2 previewShapePosition = new Vector2(15, 12);

    void Start()
    {
        Instantiate(Shapes[Random.Range(0, Shapes.Length)], transform.position, Quaternion.identity);
        SpawnPreviewShape();
    }

    public void SpawnShape()
    {
        shape.transform.localPosition = transform.position;
        shape.GetComponent<GameShape>().enabled = true;
        SpawnPreviewShape();
    }
    
    public void SpawnPreviewShape()
    {
        shape = Instantiate(Shapes[Random.Range(0, Shapes.Length)], previewShapePosition, Quaternion.identity);
        shape.GetComponent<GameShape>().enabled = false;
    }

    public void UpdateScores(int scores, int rows)
    {
        Scores.text = scores.ToString();
        NumberRows.text = rows.ToString();
    }

    public void UpdateSpeed(float speed)
    {
        GameSpeed.text = speed.ToString("0");
    }
}
