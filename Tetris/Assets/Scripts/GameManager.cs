using System;
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
    [SerializeField] private GameObject currentShape;
    [SerializeField] private GameObject nextShape;
    private readonly Vector3 _previewShapePosition = new Vector3(20, 120, 0);
    private readonly Vector3 _currentShapePosition = new Vector3(-5, 85, 0);
    [SerializeField] private const int GameBoardHeight = 20;
    [SerializeField] private const int GameBoardWidth = 10;
    private Transform[,] _gameGrid = new Transform[GameBoardWidth, GameBoardHeight];
    private int _numberOfRowsThisTurn = 0;
    private int _numberOfAllRows = 0;
    private int _currentScores = 0;
    private int _rowsForUpdateDifficult = 0;
    public UIManager uiManager;
    private float _previousFallTime;
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
        currentShape = shapes[currentShapeNumber];
        currentShape.transform.localScale = new Vector3(10, 10, 0);
        currentShape = Instantiate(currentShape, transform.position + _currentShapePosition, Quaternion.identity);
        currentShape.transform.SetParent(transform);
        SpawnPreviewShape();
    }

    void Update()
    {
        Control();
        if (Time.time - _previousFallTime > (Input.GetKey(moveDown) ? 0.05f : _fallTime))
        {
            currentShape.transform.localPosition += new Vector3(0, -10, 0);
            if (!CheckCollisionWalls())
            {
                currentShape.transform.localPosition += new Vector3(0, 10, 0);
                AddToGrid();
                CheckCompleteLines();
                if (IsGameOver())
                    GameOver();
                else
                    SpawnShape();
            }
            _previousFallTime = Time.time;
        }
        UpdateDifficult();
        UpdateScores();
    }
    private void CheckCompleteLines()
    {
        for (var i = GameBoardHeight - 1; i >= 0; i--)
        {
            if (!CompleteLine(i)) continue;
            DeleteLine(i);
            LinesDown(i);
        }
    }

    private void LinesDown(int i)
    {
        for (var k = i; k < GameBoardHeight; k++)
        {
            for (var j = 0; j < GameBoardWidth; j++)
            {
                if (_gameGrid[j, k] == null) continue;
                _gameGrid[j, k - 1] = _gameGrid[j, k];
                _gameGrid[j, k] = null;
                _gameGrid[j, k - 1].transform.position -= new Vector3(0, 10, 0);
            }
        }
    }
    private void DeleteLine(int i)
    {
        for (var j = 0; j < GameBoardWidth; j++)
        {
            Destroy(_gameGrid[j, i].gameObject);
            _gameGrid[j, i] = null;
        }
    }

    private bool CompleteLine(int i)
    {
        for (var j = 0; j < GameBoardWidth; j++)
        {
            if (_gameGrid[j, i] == null) return false;
        }
        _numberOfRowsThisTurn++;
        _numberOfAllRows++;
        _rowsForUpdateDifficult++;
        return true;
    }
    private void AddToGrid()
    {
        foreach (Transform children in currentShape.transform)
        {
            var position = children.transform.position - transform.position;
            var roundedX = Mathf.RoundToInt((position.x + 45) /10);
            var roundedY = Mathf.RoundToInt((position.y + 95) /10);

            _gameGrid[roundedX, roundedY] = children;
        }
    }
    
    private bool CheckCollisionWalls()
    {
        foreach (Transform children in currentShape.transform)
        {
            var position = children.transform.position - transform.position;
            var roundedX = Mathf.RoundToInt((position.x + 45) /10);
            var roundedY = Mathf.RoundToInt((position.y + 95) /10);
            if (roundedX < 0 || roundedX >= GameBoardWidth || roundedY < 0 || roundedY >= GameBoardHeight) return false;
            if (_gameGrid[roundedX, roundedY] != null) return false;
        }
        return true;
    }
    private bool IsGameOver()
    {
        for (var i = GameBoardWidth - 1; i >= 0; i--)
        {
            if (_gameGrid[i, GameBoardHeight - 2] != null)
                return true;
        }
        return false;
    }

    private void GameOver()
    {
        this.enabled = false;
        //SceneManager.LoadScene("GameOver");
    }

    private void SpawnShape()
    {
        nextShape.transform.localPosition = _currentShapePosition;
        nextShape.GetComponent<GameShape>().enabled = true;
        currentShape = nextShape;
        SpawnPreviewShape();
    }
    
    private void SpawnPreviewShape()
    {
        var nextShapeNumber = Random.Range(0, shapes.Length);
        nextShape = shapes[nextShapeNumber];
        nextShape.transform.localScale = new Vector3(10, 10, 0);
        nextShape = Instantiate(nextShape, transform.position + _previewShapePosition, Quaternion.identity);
        nextShape.transform.SetParent(transform);
        nextShape.GetComponent<GameShape>().enabled = false;
    }
    
    private void Control()
    {
        _timePassed += Time.deltaTime;
        if (Input.GetKey(moveLeft) && _timePassed >= KeyDelay)
        {
            currentShape.transform.localPosition += new Vector3(-10, 0, 0);
            if (!CheckCollisionWalls()) currentShape.transform.position += new Vector3(10, 0, 0);
            _timePassed = 0f;
        }

        else if (Input.GetKey(moveRight) && _timePassed >= KeyDelay)
        {
            currentShape.transform.localPosition += new Vector3(10, 0, 0);
            if (!CheckCollisionWalls()) currentShape.transform.position += new Vector3(-10, 0, 0);
            _timePassed = 0f;
        }

        if (Input.GetKeyDown(rotateKey))
        {
            currentShape.GetComponent<GameShape>().RotateShape(currentShape, true);
            if (!CheckCollisionWalls())
                currentShape.GetComponent<GameShape>().RotateShape(currentShape, false);
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
