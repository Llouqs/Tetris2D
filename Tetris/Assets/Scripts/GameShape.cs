using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameShape : MonoBehaviour
{
    public Vector3 shapeRotation;
    public static int GameBoardHeight = 20;
    public static int GameBoardWidth = 10;

    private float previousFallTime;
    private const float keyDelay = 0.1f;
    private float timePassed = 0f;
    private static float fallTime = 0.9f;
    private static Transform[,] gameGrid = new Transform[GameBoardWidth, GameBoardHeight];
    private static int numberOfRowsThisTurn;
    private static int numberOfAllRows;
    private static int curentScores;
    private static int rowsForUpdateDifficult;

    void Update()
    {
        control();
        if (Time.time - previousFallTime > (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ? 0.05f : fallTime))
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
                else FindObjectOfType<GameManager>().SpawnShape();
            }
            previousFallTime = Time.time;
        }
        UpdateScores();
        UpdateDifficult();
    }

    private void UpdateDifficult()
    {
        if (rowsForUpdateDifficult > 5 && fallTime >= 0.15f)
        {
            fallTime -= 0.1f;
            FindObjectOfType<GameManager>().UpdateSpeed(fallTime * 10);
            rowsForUpdateDifficult = 0;
        }

        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (fallTime <= 1.6f) fallTime += 0.1f;
            FindObjectOfType<GameManager>().UpdateSpeed(fallTime * 10);
        }

        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (fallTime >= 0.15f) fallTime -= 0.1f;
            FindObjectOfType<GameManager>().UpdateSpeed(fallTime * 10);
        }

    }

    void control()
    {
        timePassed += Time.deltaTime;
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && timePassed >= keyDelay)
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!CheckCollisionWalls()) transform.position += new Vector3(1, 0, 0);
            timePassed = 0f;
        }

        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && timePassed >= keyDelay)
        {
            transform.position += new Vector3(1, 0, 0);
            if (!CheckCollisionWalls()) transform.position += new Vector3(-1, 0, 0);
            timePassed = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(shapeRotation), new Vector3(0, 0, 1), 90);
            if (!CheckCollisionWalls())
                transform.RotateAround(transform.TransformPoint(shapeRotation), new Vector3(0, 0, 1), -90);
        }

    }
    void UpdateScores()
    {
        if (numberOfRowsThisTurn > 0)
        {
            switch (numberOfRowsThisTurn)
            {
                case 1: curentScores += 4; break;
                case 2: curentScores += 12; break;
                case 3: curentScores += 40; break;
                case 4: curentScores += 120; break;
            }
            numberOfRowsThisTurn = 0;
            FindObjectOfType<GameManager>().UpdateScores(curentScores, numberOfAllRows);
        }
    }

    void CheckCompleteLines()
    {
        for (int i = GameBoardHeight - 1; i >= 0; i--)
        {
            if (ComleteLine(i))
            {
                DeleteLine(i);
                LinesDown(i);
            }
        }
    }

    void LinesDown(int i)
    {
        for (int k = i; k < GameBoardHeight; k++)
        {
            for (int j = 0; j < GameBoardWidth; j++)
            {
                if (gameGrid[j, k] != null)
                {
                    gameGrid[j, k - 1] = gameGrid[j, k];
                    gameGrid[j, k] = null;
                    gameGrid[j, k - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < GameBoardWidth; j++)
        {
            Destroy(gameGrid[j, i].gameObject);
            gameGrid[j, i] = null;
        }
    }

    bool ComleteLine(int i)
    {
        for (int j = 0; j < GameBoardWidth; j++)
        {
            if (gameGrid[j, i] == null) return false;
        }
        numberOfRowsThisTurn++;
        numberOfAllRows++;
        rowsForUpdateDifficult++;
        return true;
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            gameGrid[roundedX, roundedY] = children;
        }
    }

    bool CheckCollisionWalls()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedX < 0 || roundedX >= GameBoardWidth || roundedY < 0 || roundedY >= GameBoardHeight) return false;
            if (gameGrid[roundedX, roundedY] != null) return false;
        }
        return true;
    }
    bool IsGameOver()
    {
        for (int i = GameBoardWidth - 1; i >= 0; i--)
        {
            if (gameGrid[i, GameBoardHeight - 2] != null)
                return true;
        }
        return false;
    }
    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}