using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public int n;
    public Tile[,] tiles;
    public int m;
    public Tile tilePrefab;
    public KeyCode up;
    public KeyCode left;
    public KeyCode down;
    public KeyCode right;
    public Player p;
    private Tile startTile;
    private Tile endTile;
    private Random r = new Random();
    public Pathfinder pf;

    private void Awake()
    {
        var center = new Vector2((float)n / 2 - 0.5f, (float)n / 2 - 0.5f);
        Camera.main.transform.position = new Vector3(center.x, center.y, -5);


        tiles = new Tile[n, n];
        gm = this;
        //generar tablero
        for (int x = 0; x < n; x++)
        {
            for (int y = 0; y < n; y++)
            {
                tiles[x, y] = Instantiate(tilePrefab, new Vector3(x, y, 1), Quaternion.identity);
                Debug.Log(tiles[x, y]);
            }
        }
        startTile = GetNode(r.Next(0, n - 1), r.Next(0, n - 1));
        Debug.Log(startTile);

        endTile = GetNode(r.Next(0, n - 1), r.Next(0, n - 1));
        while (endTile.pos != startTile.pos)
        {
            endTile = GetNode(r.Next(0, n - 1), r.Next(0, n - 1));
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        p.setPos(startTile.x, startTile.y);
        int c = m;
        int g = 4*m;
        while (g>0)
        {
            
            int x = r.Next(0,n-1);
            int y = r.Next(0,n-1);
            if (IsOcc(x,y))
            {

                tiles[x, y].solid = true;
                List<Tile> path = pf.FindPath(startTile.x, startTile.y, endTile.x, endTile.y);

                if (path.Count>0)
                {
                    tiles[x, y].GetComponent<SpriteRenderer>().color = Color.gray;
                    m--;
                }
                else
                {
                    tiles[x, y].solid = false;
                }
                g--;
            }
            if (c<=0)
            {
                g = 0;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(up) || Input.GetKeyDown(left) || Input.GetKeyDown(down) || Input.GetKeyDown(right))
        {
            int x1 = p.x;
            int y1 = p.y;
            Debug.Log("x1:" + x1 + " y1:" + y1);
            if (Input.GetKeyDown(up))
            {
                y1 = p.x + 1;
            }
            else if (Input.GetKeyDown(left))
            {
                x1 = p.x - 1;
            }
            else if (Input.GetKeyDown(down))
            {
                y1 = p.x - 1;
            }
            else if (Input.GetKeyDown(right))
            {
                x1 = p.x + 1;
            }
            if (!GetNode(x1, y1).solid)
            {
                p.setPos(x1, y1);
            }
        }
    }


    public Tile GetNode(int x, int y)
    {
        return tiles[x,y];
    }


    public bool IsOcc(int x, int y)
    {
        Tile t = GetNode(x,y);
        if (t != startTile && t != endTile && !t.solid)
        {
            return false;
        }
        return true;
    }

}
