using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreater : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize = 10f;
    public GameObject cellSprite;
    public GameObject[,] allCells;
    // Start is called before the first frame update
    void Start()
    {
        allCells = new GameObject[width, height];
        //cellSprite.transform.localScale = new Vector3(cellSize, cellSize, cellSize);
        BuildGrid();
    }

    // Update is called once per frame
    void BuildGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i * cellSize, j * cellSize);
                GameObject tile = Instantiate(cellSprite, tempPosition, Quaternion.identity);
                tile.transform.parent = transform;
                tile.name = "( Cell: " + i + ", " + j + " )";
                allCells[i, j] = tile;
            }
        }
    }
}
