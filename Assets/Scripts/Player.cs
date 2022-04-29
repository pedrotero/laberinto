using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ref: https://drive.google.com/file/d/1WiF2LwM-6WvEnas9vw32YrYPly9K0Qrv/view

public class Player : MonoBehaviour
{
    public Cell CurrentCell;

    public delegate void Move();
    public static event Move OnMove;
    private void Awake()
    {

    }
    public void ChangeCell(Cell c)
    {
        
        CurrentCell = c;
        GetComponent<Transform>().position = new Vector2(c.x,c.y);
        if (OnMove != null)
        {
            OnMove();
        }
        if (CurrentCell == BoardManager.Instance.grid.endCell)
        {
            int lr = PlayerPrefs.GetInt("LvR");
            int l = PlayerPrefs.GetInt("Lvl");
            BoardManager.Instance.enemycount++;
            PlayerPrefs.SetInt("en", BoardManager.Instance.enemycount);
            float tr = PlayerPrefs.GetFloat("TimeR");
            if (BoardManager.Instance.tiempoTrans <= tr && lr < l)
            {
                PlayerPrefs.SetFloat("TimeR", BoardManager.Instance.tiempoTrans);
            }

            PlayerPrefs.SetInt("Lvl", l + 1);
            SceneManager.LoadScene("Game");
        }
        foreach (Enemy e in BoardManager.Instance.enemies)
        {
            if (CurrentCell == e.CurrentCell)
            {
                Debug.Log("Perdió");
                break;
            }
        }




    }
}
