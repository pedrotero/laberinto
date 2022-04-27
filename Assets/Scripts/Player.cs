using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int x;
    public int y;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPos(int x1, int y1)
    {
        
        x = x1;
        y = y1;
        GetComponent<Transform>().position = new Vector3(x, y);
    }

    Tile getCurrentTile()
    {
        return GameManager.gm.GetNode(x, y);
    }
}
