using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public Cell CurrentCell;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChasePlayer", 1, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
            if (CurrentCell == BoardManager.Instance.player.CurrentCell)
            {
                int lr = PlayerPrefs.GetInt("LvR");
                int l = PlayerPrefs.GetInt("Lvl");
                float tr = PlayerPrefs.GetFloat("TimeR");
                if (((BoardManager.Instance.tiempoTrans < tr && lr <= l) || lr < l) && tr != 0)
                {
                    PlayerPrefs.SetFloat("TimeR", BoardManager.Instance.tiempoTrans);
                }
                if (lr < l)
                {
                    PlayerPrefs.SetInt("LvR", l);
                }
                PlayerPrefs.SetInt("Win", 0);
                SceneManager.LoadScene("EndScreen");
                Debug.Log("Perdió");
            }
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
