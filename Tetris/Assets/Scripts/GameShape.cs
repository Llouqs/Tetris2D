using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameShape : MonoBehaviour
{
    public Vector3 shapeRotation;
    /*
    void Update()
    {
        Control();
        if (Time.time - _previousFallTime > (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ? 0.05f : _fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!CheckCollisionWalls())
            {
                transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                CheckCompleteLines();
                this.enabled = false;
                if (IsGameOver())
                    GameOver();
                else _gameManager.SpawnShape();
            }
            _previousFallTime = Time.time;
        }
        UpdateScores();
        UpdateDifficult();
    }

    private void UpdateDifficult()
    {
        if (_rowsForUpdateDifficult > 5 && _fallTime >= 0.15f)
        {
            _fallTime -= 0.1f;
            _uiManager.UpdateSpeed(_fallTime * 10);
            _rowsForUpdateDifficult = 0;
        }

        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (_fallTime <= 1.6f) _fallTime += 0.1f;
            _uiManager.UpdateSpeed(_fallTime * 10);
        }

        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (_fallTime >= 0.15f) _fallTime -= 0.1f;
            _uiManager.UpdateSpeed(_fallTime * 10);
        }

    }

    private void Control()
    {
        _timePassed += Time.deltaTime;
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && _timePassed >= KeyDelay)
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!CheckCollisionWalls()) transform.position += new Vector3(1, 0, 0);
            _timePassed = 0f;
        }

        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && _timePassed >= KeyDelay)
        {
            transform.position += new Vector3(1, 0, 0);
            if (!CheckCollisionWalls()) transform.position += new Vector3(-1, 0, 0);
            _timePassed = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(shapeRotation), new Vector3(0, 0, 1), 90);
            if (!CheckCollisionWalls())
                transform.RotateAround(transform.TransformPoint(shapeRotation), new Vector3(0, 0, 1), -90);
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
            _uiManager.UpdateScores(_currentScores, _numberOfAllRows);
        }
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
                if (GameGrid[j, k] == null) continue;
                GameGrid[j, k - 1] = GameGrid[j, k];
                GameGrid[j, k] = null;
                GameGrid[j, k - 1].transform.position -= new Vector3(0, 1, 0);
            }
        }
    }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < GameBoardWidth; j++)
        {
            Destroy(GameGrid[j, i].gameObject);
            GameGrid[j, i] = null;
        }
    }

    private bool CompleteLine(int i)
    {
        for (var j = 0; j < GameBoardWidth; j++)
        {
            if (GameGrid[j, i] == null) return false;
        }
        _numberOfRowsThisTurn++;
        _numberOfAllRows++;
        _rowsForUpdateDifficult++;
        return true;
    }

    private void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            var position = children.transform.position;
            var roundedX = Mathf.RoundToInt(position.x);
            var roundedY = Mathf.RoundToInt(position.y);

            GameGrid[roundedX, roundedY] = children;
        }
    }

    private bool CheckCollisionWalls()
    {
        foreach (Transform children in transform)
        {
            var position = children.transform.position;
            var roundedX = Mathf.RoundToInt(position.x);
            var roundedY = Mathf.RoundToInt(position.y);
            if (roundedX < 0 || roundedX >= GameBoardWidth || roundedY < 0 || roundedY >= GameBoardHeight) return false;
            if (GameGrid[roundedX, roundedY] != null) return false;
        }
        return true;
    }

    private bool IsGameOver()
    {
        for (var i = GameBoardWidth - 1; i >= 0; i--)
        {
            if (GameGrid[i, GameBoardHeight - 2] != null)
                return true;
        }
        return false;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }*/
}