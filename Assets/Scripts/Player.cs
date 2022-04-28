using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
