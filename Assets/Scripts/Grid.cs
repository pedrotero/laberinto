using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Grid : ScriptableObject
{
    private int width;
    private int height;
    private int cellSize;
    private Cell cellPrefab;
    private Cell[,] gridArray;
    public Cell startCell;
    public Cell endCell;

    #pragma warning disable CA2200
    public Grid(int width, int height, int cellSize, Cell cellPrefab)
    {
        
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.cellPrefab = cellPrefab;

        generateBoard();
    }

    private void generateBoard()
    {
        Cell cell;
        gridArray = new Cell[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var p = new Vector3(i, j, 5);
                cell = Instantiate(cellPrefab, p, Quaternion.identity);
                cell.Init(this, (int)p.x, (int)p.y, true);
                cell.SetColor(Color.blue);

                gridArray[i, j] = cell;
            }
        }

        var center = new Vector2((float)height / 2 - 0.5f, (float)width / 2 - 0.5f);

        Camera.main.transform.position = new Vector3(center.x, center.y, -5);
    }

    internal int GetHeight()
    {
        return height;
    }

    internal int GetWidth()
    {
        return width;
    }

    

    public Cell GetGridObject(int x, int y)
    {
        return gridArray[x, y];
    }

    internal float GetCellSize()
    {
        return cellSize;
    }

    public bool IsEmpty(int x, int y)
    {
        Cell c = GetGridObject(x, y);
        foreach (Enemy e in BoardManager.Instance.enemies)
        {
            if (c == e.CurrentCell)
            {
                return false;
            }
        }
        if (c==startCell || c==endCell || !c.isWalkable) return false;
        else return true;
    }
}
