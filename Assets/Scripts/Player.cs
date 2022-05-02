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

    public void Update()
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
            float tr = PlayerPrefs.GetFloat("TimeR");
            PlayerPrefs.SetFloat("time", BoardManager.Instance.tiempoTrans);
            if (lr<l)
            {
                PlayerPrefs.SetInt("LvR",l);
            }
            Debug.Log("tr:"+tr);
            Debug.Log("t:"+ BoardManager.Instance.tiempoTrans);
            Debug.Log("level"+ l);


            if (l<5)
            {
                PlayerPrefs.SetInt("Lvl", l + 1);
                if (((BoardManager.Instance.tiempoTrans < tr && lr <= l)|| lr < l) && tr!=0)
                {
                    PlayerPrefs.SetFloat("TimeR", BoardManager.Instance.tiempoTrans);

                }
                if (tr==0 && lr <=l)
                {
                    PlayerPrefs.SetFloat("TimeR", BoardManager.Instance.tiempoTrans);
                }
                SceneManager.LoadScene("Game");
            }
            else
            {
                if (tr!=0 && (tr > BoardManager.Instance.tiempoTrans || lr <= l))
                {
                    PlayerPrefs.SetFloat("TimeR", BoardManager.Instance.tiempoTrans);
                }
                if (tr==0)
                {
                    PlayerPrefs.SetFloat("TimeR", BoardManager.Instance.tiempoTrans);
                }
                PlayerPrefs.SetInt("Win", 1);
                SceneManager.LoadScene("EndScreen");
            }
            
        }
        




    }
}
