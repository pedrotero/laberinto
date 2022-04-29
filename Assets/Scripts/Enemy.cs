using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Cell CurrentCell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        Player.OnMove += ChasePlayer;
    }

    private void OnDisable()
    {
        Player.OnMove -= ChasePlayer;
    }

    public void ChangeCell(Cell c)
    {

        CurrentCell = c;
        GetComponent<Transform>().position = new Vector2(c.x, c.y);
    }

    public void ChasePlayer()
    {
        Cell c = BoardManager.Instance.player.CurrentCell;
        List<Cell> clist = BoardManager.Instance.pf.FindPath(BoardManager.Instance.grid, CurrentCell.x, CurrentCell.y, c.x, c.y);
        if (clist != null && clist.Count > 1)
        {
            ChangeCell(clist[1]);
        }
    }
}
