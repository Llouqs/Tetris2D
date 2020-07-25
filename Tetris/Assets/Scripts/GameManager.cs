using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] Shapes;
    private GameObject shape;
    private Vector2 previewShapePosition = new Vector2(15, 12);
    public Color[] palette;

    void Start()
    {
        var currentShapeNumber = Random.Range(0, Shapes.Length);
        var currentShape = Shapes[currentShapeNumber];
        //foreach(GameObject tmpChild in currentShape.transform)
        //{
        //    tmpChild.GetComponent<SpriteRenderer>().color = palette[currentShapeNumber];
        //}
        Instantiate(currentShape, transform.position, Quaternion.identity).transform.SetParent(transform);
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
        var currentShapeNumber = Random.Range(0, Shapes.Length);
        var currentShape = Shapes[currentShapeNumber];
        //foreach (GameObject tmpChild in currentShape.transform)
        //{
        //    tmpChild.GetComponent<SpriteRenderer>().color = palette[currentShapeNumber];
        //}
        shape = Instantiate(currentShape, transform.position, Quaternion.identity);
        shape.transform.SetParent(transform);
        shape.GetComponent<GameShape>().enabled = false;
    }
}
