using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameShape : MonoBehaviour
{
    public Vector3 shapeRotation;

    public void RotateShape(GameObject currentShape, bool isRotated)
    {
        if(isRotated)
            currentShape.transform.RotateAround(currentShape.transform.TransformPoint(shapeRotation), new Vector3(0, 0, 1), 90);
        else currentShape.transform.RotateAround(currentShape.transform.TransformPoint(shapeRotation), new Vector3(0, 0, 1), -90);
    }
}