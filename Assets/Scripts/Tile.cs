using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool solid = false;
    public int gCost, hCost, fCost;
    public Tile pastTile;
    public int x;
    public int y;
    public Vector2 pos;
    // Start is called before the first frame update

    private void Awake()
    {
        x = (int)GetComponent<Transform>().position.x;
        y = (int)GetComponent<Transform>().position.y;
        pos = GetComponent<Transform>().position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    internal void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
