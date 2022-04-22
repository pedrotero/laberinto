using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int n;
    public Tile[,] tiles;
    public int m;
    public Tile tilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        n = 10;
        tiles = new Tile[n, n];
        instance = this;
        //generar tablero
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tiles[i, j] = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
