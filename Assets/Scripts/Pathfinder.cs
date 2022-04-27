using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// ref: https://www.youtube.com/watch?v=alU04hvz6L4&t=1065s

public class PathManager : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    public static PathManager Instance;
    private List<Tile> openList;
    private List<Tile> closedList;


    private void Awake()
    {
        Instance = this;
    }


    public List<Tile> FindPath(Grid grid, int startx, int starty, int endx, int endy)
    {
        Tile startTile = GameManager.gm.GetNode(startx,starty);
        Tile endTile = GameManager.gm.GetNode(endx, endy);

        openList = new List<Tile> { startTile };
        closedList = new List<Tile>();

        for (int x = 0; x < GameManager.gm.n; x++)
        {
            for (int y = 0; y < GameManager.gm.n; y++)
            {

                Tile pathNode = GameManager.gm.GetNode(x,y);
                if (!pathNode.solid)
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.pastTile = null;
            }
        }

        startTile.gCost = 0;
        startTile.hCost = CalculateDistanceCost(startTile, endTile);
        startTile.CalculateFCost();

        while (openList.Count > 0)
        {
            Tile currentNode = GetLowestFCostNode(openList);
            if (currentNode == endTile)
            {
                // Reached final node
                Debug.Log("Reach the end");
                return CalculatePath(endTile);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Tile neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable)
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
                //PathfindingDebugStepVisual.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
            }
        }
        Debug.Log("Did not reach the end");
        return null;
    }

    private List<Tile> CalculatePath(Tile endTile)
    {
        List<Tile> path = new List<Tile>();
        path.Add(endTile);
        Tile currentNode = endTile;
        while (currentNode.pastTile != null)
        {
            path.Add(currentNode.pastTile);
            currentNode = currentNode.pastTile;
        }
        path.Reverse();

        foreach (Tile c in path)
        {
            c.SetColor(Color.green);
            Debug.Log(c.ToString());
        }
        return path;
    }

    private List<Tile> GetNeighbourList(Tile currentNode)
    {
        List<Tile> neighbourList = new List<Tile>();

        if (currentNode.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // Left Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // Right Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    public Tile GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private int CalculateDistanceCost(Tile a, Tile b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
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


}
