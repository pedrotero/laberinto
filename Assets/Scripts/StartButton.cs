using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Text timeR;
    public Text LvR;
    public int n;
    public int m;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Lvl"))
        {
            PlayerPrefs.SetFloat("TimeR", 0);
            PlayerPrefs.SetFloat("LvR", 0);
        }
        PlayerPrefs.SetInt("Lvl", 0);
        PlayerPrefs.GetFloat("TimeR");
        LvR.text = "Best Lv:" + PlayerPrefs.GetFloat("LvR");
        timeR.text = "Best Time:" + PlayerPrefs.GetFloat("TimeR").ToString("mm':'ss':'fff");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeN(int n)
    {
        this.n = n;
    }

    public void changeM(int m)
    {
        this.m = m;
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("n",n);
        PlayerPrefs.SetInt("m", m);
        PlayerPrefs.SetInt("en", 1);
        SceneManager.LoadScene("Game");
    }
}
