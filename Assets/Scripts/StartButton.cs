using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Text timeR;
    public Text LvR;
    public Text nText;
    public Text mText;
    public int n;
    public int m;
    public TimeSpan time;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("TimeR"))
        {
            PlayerPrefs.SetFloat("TimeR", 0);
            PlayerPrefs.SetInt("LvR", 1);
        }


        PlayerPrefs.GetFloat("TimeR");
        float lr = PlayerPrefs.GetInt("LvR");
        LvR.text = "Best Lv: " + lr;
        float tr = PlayerPrefs.GetFloat("TimeR");
        time = TimeSpan.FromSeconds(tr);
        timeR.text = "Best Time:" + time.ToString("mm':'ss':'fff");
    }


    void Update()
    {

    }

    public void changeN(float n)
    {
        this.n = (int)n;
        nText.text = "n: " + n;
    }

    public void changeM(float m)
    {
        this.m = (int)m;
        mText.text = "m: " + m;
    }
    public void StartGame()
    {
        PlayerPrefs.SetFloat("time", 0);
        PlayerPrefs.SetInt("n", n);
        PlayerPrefs.SetInt("m", m);
        PlayerPrefs.SetInt("Lvl", 1);
        SceneManager.LoadScene("Game");
    }

    public void RestartProgress()
    {
        PlayerPrefs.SetFloat("TimeR", 0);
        PlayerPrefs.SetInt("LvR", 1);
        PlayerPrefs.GetFloat("TimeR");
        float lr = PlayerPrefs.GetInt("LvR");
        LvR.text = "Best Lv: " + lr;
        float tr = PlayerPrefs.GetFloat("TimeR");
        time = TimeSpan.FromSeconds(tr);
        timeR.text = "Best Time:" + time.ToString("mm':'ss':'fff");
    }
}
