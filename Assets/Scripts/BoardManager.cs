using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private Enemy EnemyPrefab;
    public Grid grid;
    public Player player;
    public KeyCode up;
    public KeyCode left;
    public KeyCode down;
    public KeyCode right;
    public int n;
    public int m;
    public PathManager pf;
    public List<Enemy> enemies = new List<Enemy>();
    public int enemycount;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        grid = new Grid(10, 10, 1, CellPrefab);

        player = Instantiate(PlayerPrefab, new Vector2(0, 0), Quaternion.identity);
        player.ChangeCell(grid.GetGridObject(Random.Range(0, n), Random.Range(0, n)));
        grid.startCell = player.CurrentCell;
        grid.endCell = grid.GetGridObject(Random.Range(0, n), Random.Range(0, n));
        while (grid.IsEmpty(grid.endCell.x,grid.endCell.y))
        {
            grid.endCell = grid.GetGridObject(Random.Range(0, n), Random.Range(0, n));
        }
        grid.endCell.SetColor(Color.yellow);
        while (m>0)
        {
            Cell c = grid.GetGridObject(Random.Range(0, n), Random.Range(0, n));
            if (grid.IsEmpty(c.x,c.y))
            {
                c.SetWalkable(false);
                if (pf.FindPath(grid,grid.startCell.x,grid.startCell.y,grid.endCell.x,grid.endCell.y) == null)
                {
                    c.SetWalkable(true);
                }
                else
                {
                    m--;
                }
            }
        }

        while (enemycount > 0)
        {
            Cell c = grid.GetGridObject(Random.Range(0, n), Random.Range(0, n));
            if (grid.IsEmpty(c.x,c.y))
            {
                Enemy e = Instantiate(EnemyPrefab, new Vector2(n, n), Quaternion.identity);
                e.ChangeCell(c);
                enemycount--;
            }
        }
    }

     private void Update()
     {
        if (Input.GetKeyDown(up) || Input.GetKeyDown(left) || Input.GetKeyDown(down) || Input.GetKeyDown(right))
        {
            Cell c = grid.GetGridObject(player.CurrentCell.x, player.CurrentCell.y);
            
            if (Input.GetKeyDown(up))
            {

                c = grid.GetGridObject(player.CurrentCell.x, player.CurrentCell.y+1);
            }
            else if (Input.GetKeyDown(left))
            {
                c = grid.GetGridObject(player.CurrentCell.x-1, player.CurrentCell.y);
            }
            else if (Input.GetKeyDown(down))
            {
                c = grid.GetGridObject(player.CurrentCell.x, player.CurrentCell.y-1);
            }
            else if (Input.GetKeyDown(right))
            {

                c = grid.GetGridObject(player.CurrentCell.x+1, player.CurrentCell.y);
            }
            if (c.isWalkable)
            {
                player.ChangeCell(c);
            }
        }
     }



}
