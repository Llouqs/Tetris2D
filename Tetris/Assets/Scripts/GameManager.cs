﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject[] shapes;
    private GameObject _currentShape;
    private GameObject _nextShape;
    private Vector2 _previewShapePosition = new Vector2(15, 12);
    [SerializeField] private const int GameBoardHeight = 200;
    [SerializeField] private const int GameBoardWidth = 100;
    private Transform[,] _gameGrid = new Transform[GameBoardWidth, GameBoardHeight];
    private int _numberOfRowsThisTurn = 0;
    private int _numberOfAllRows = 0;
    private int _currentScores = 0;
    private int _rowsForUpdateDifficult = 0;
    public UIManager uiManager;
    private float _previousFallTime = 0f;
    private const float KeyDelay = 0.1f;
    private float _timePassed = 0f;
    private float _fallTime = 0.9f;
    public KeyCode upSpeed;
    public KeyCode downSpeed;
    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode moveDown;
    public KeyCode rotateKey;

    void Start()
    {
        var currentShapeNumber = Random.Range(0, shapes.Length);
        var currentShape = shapes[currentShapeNumber];
        Instantiate(currentShape, transform.position, Quaternion.identity).transform.SetParent(transform);
        SpawnPreviewShape();
    }

    void Update()
    {
        Control();
        if (Time.time - _previousFallTime > (Input.GetKey(moveDown) ? 0.05f : _fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            // if (!CheckCollisionWalls())
            // {
            //     transform.position += new Vector3(0, 1, 0);
            //     AddToGrid();
            //     CheckCompleteLines();
            //     this.enabled = false;
            //     if (IsGameOver())
            //         GameOver();
            //     else _gameManager.SpawnShape();
            // }
            _previousFallTime = Time.time;
        }
        UpdateDifficult();
        UpdateScores();
    }

    public void SpawnShape()
    {
        _currentShape.transform.localPosition = transform.position;
        _currentShape.GetComponent<GameShape>().enabled = true;
        SpawnPreviewShape();
    }
    
    private void SpawnPreviewShape()
    {
        var currentShapeNumber = Random.Range(0, shapes.Length);
        var currentShape = shapes[currentShapeNumber];
        _currentShape = Instantiate(currentShape, transform.position, Quaternion.identity);
        _currentShape.transform.SetParent(transform);
        _currentShape.GetComponent<GameShape>().enabled = false;
    }
    
    private void Control()
    {
        _timePassed += Time.deltaTime;
        if (Input.GetKey(moveLeft) && _timePassed >= KeyDelay)
        {
            transform.position += new Vector3(-1, 0, 0);
            //if (!CheckCollisionWalls()) transform.position += new Vector3(1, 0, 0);
            _timePassed = 0f;
        }

        else if (Input.GetKey(moveRight) && _timePassed >= KeyDelay)
        {
            transform.position += new Vector3(1, 0, 0);
            //if (!CheckCollisionWalls()) transform.position += new Vector3(-1, 0, 0);
            _timePassed = 0f;
        }

        if (Input.GetKeyDown(rotateKey))
        {
            transform.RotateAround(transform.TransformPoint(_currentShape.GetComponent<GameShape>().shapeRotation), new Vector3(0, 0, 1), 90);
            //if (!CheckCollisionWalls())
            //    transform.RotateAround(transform.TransformPoint(_currentShape.GetComponent<GameShape>().shapeRotation), new Vector3(0, 0, 1), -90);
        }

    }
    
    private void UpdateDifficult()
    {
        if (_rowsForUpdateDifficult > 5 && _fallTime >= 0.15f)
        {
            _fallTime -= 0.1f;
            uiManager.UpdateSpeed(_fallTime * 10);
            _rowsForUpdateDifficult = 0;
        }

        if (Input.GetKeyDown(downSpeed))
        {
            if (_fallTime <= 1.6f) _fallTime += 0.1f;
            uiManager.UpdateSpeed(_fallTime * 10);
        }

        if (Input.GetKeyDown(upSpeed))
        {
            if (_fallTime >= 0.15f) _fallTime -= 0.1f;
            uiManager.UpdateSpeed(_fallTime * 10);
        }
    }
    private void UpdateScores()
    {
        if (_numberOfRowsThisTurn > 0)
        {
            switch (_numberOfRowsThisTurn)
            {
                case 1: _currentScores += 4; break;
                case 2: _currentScores += 12; break;
                case 3: _currentScores += 40; break;
                case 4: _currentScores += 120; break;
            }
            _numberOfRowsThisTurn = 0;
            uiManager.UpdateScores(_currentScores, _numberOfAllRows);
        }
    }
}
