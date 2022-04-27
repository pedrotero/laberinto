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
    private const int MOVE_COST = 10;
    private List<Tile> openList;
    private List<Tile> closedList;
    private Random r = new Random();

    private void Awake()
    {
        var center = new Vector2((float)n / 2 - 0.5f, (float)n / 2 - 0.5f);
        Camera.main.transform.position = new Vector3(center.x, center.y, -5);

        n = 10;
        tiles = new Tile[n, n];
        gm = this;
        //generar tablero
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tiles[i, j] = Instantiate(tilePrefab, new Vector3(i, j, 1), Quaternion.identity);
            }
        }
        startTile = GetTile(new Vector2(r.Next(), r.Next()));
        Debug.Log(startTile);

        endTile = GetTile(new Vector2(r.Next(), r.Next()));
        while (endTile != startTile)
        {
            endTile = GetTile(new Vector2(r.Next(), r.Next()));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(p.t.position);
        Debug.Log(startTile.pos);

        p.t.position = startTile.pos;
        int c = m;
        int g = 4*m;
        while (g>0)
        {
            canEnd();
            
            int x = r.Next(0,n);
            int y = r.Next(0,n);
            Vector2 vtest = new Vector2(x, y);
            if (IsOcc(vtest))
            {
                tiles[x, y].solid = true;
                if (canEnd())
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
            if (c==0)
            {
                g = 0;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 nextPos = p.t.position;
        if (Input.GetKeyDown(up))
        {
            nextPos=p.t.position + (Vector3) Vector2.up;

        }else if (Input.GetKeyDown(left))
        {
            nextPos = p.t.position + (Vector3)Vector2.left;
        }
        else if (Input.GetKeyDown(down))
        {
            nextPos = p.t.position + (Vector3)Vector2.down;
        }
        else if (Input.GetKeyDown(right))
        {
            nextPos = p.t.position + (Vector3)Vector2.right;
        }
        //bool w = !getTile(nextPos).solid;
        //p.t.position = w ? (Vector3)nextPos : p.t.position;
    }

    public bool canEnd()
    {

        openList = new List<Tile> { startTile };
        closedList = new List<Tile>();

        for (int x = 0; x < n; x++)
        {
            for (int y = 0; y < n; y++)
            {

                Tile pathNode = tiles[x,y];
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.pastTile = null;
            }
        }

        startTile.gCost = 0;
        startTile.hCost = CalculateDistanceCost(startTile, endTile);
        startTile.CalculateFCost();
        Debug.Log(openList.Count);

        while (openList.Count > 0)
        {
            Debug.Log(openList.Count);
            Tile currentNode = GetLowestFCostNode(openList);
            if (currentNode == endTile)
            {
                // Reached final node
                Debug.Log("Reach the end");
                return true;
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Tile neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (neighbourNode.solid)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.pastTile = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endTile);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        Debug.Log("Did not reach the end");
        return false;
    }


    private List<Tile> GetNeighbourList(Tile currentNode)
    {
        List<Tile> neighbourList = new List<Tile>();

        if (currentNode.x - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
        if (currentNode.x + 1 < n) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        if (currentNode.y + 1 < n) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    public Tile GetNode(int x, int y)
    {
        return tiles[x,y];
    }

    private int CalculateDistanceCost(Tile a, Tile b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        return MOVE_COST * xDistance + MOVE_COST*yDistance;
    }

    private Tile GetLowestFCostNode(List<Tile> pathNodeList)
    {
        Tile lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

     public Tile GetTile(Vector2 pos)
    {
        int x = (int)Math.Floor(pos.x);
        int y = (int)Math.Floor(pos.y);
        if (x >= 0 && x < n && y >= 0 && y < n)
        {
            return tiles[x, y];
        }
        else
        {
            return null;
        }
    }

    public bool IsOcc(Vector2 pos)
    {
        Tile t = GetTile(pos);
        if (t != startTile && t != endTile && !t.solid)
        {
            return false;
        }
        return true;
    }

}
