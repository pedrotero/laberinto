using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndText : MonoBehaviour
{
    public Text t;
    // Start is called before the first frame update
    void Start()
    {
        int win = PlayerPrefs.GetInt("Win");
        if (win>0)
        {
            t.color = Color.yellow;
            t.text = "YOU WIN";
        }
        else
        {
            t.text = "YOU DIED";
        }
    }


    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
