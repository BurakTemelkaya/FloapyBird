﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    public int Bird, BirdDeger, Heal, ZamanMenu, UseHeal, Hscore, score, FPS, SettingsDropValue;

    public float volume;

    public Text ScoreText, LastScore, HighScoreText, HighScore;

    public Text MenuHighScore,HighTime,HealText,UsedHeal,AdControlText;

    public GameObject RedBird, BlueBird, YellowBird, DeathScene,AdControl,SettingsPanel;

    public Spawner spawn;

    public Bird Birdy;

    public Dropdown dropdown,SettingsDropdown;

    public Scrollbar ScrolBar;

    private int dif;

    
    void Start()
    {
        FPS = PlayerPrefs.GetInt("FPS");
        Application.targetFrameRate = FPS;

        dif = PlayerPrefs.GetInt("Dif");
        dropdown.value = dif;
        Heal = PlayerPrefs.GetInt("Heal");
        HealText.text = Heal.ToString();
        MenuHighScore.text = PlayerPrefs.GetInt("HScore").ToString();
        HighTime.text = PlayerPrefs.GetString("HighZaman");
        Bird = PlayerPrefs.GetInt("SkinDegeri");       
        BirdSkin();
    }

    public void HealUse()
    {
        Heal = PlayerPrefs.GetInt("Heal");
        if (Heal == 0)
        {
            HealText.color = Color.red;
            AdControl.SetActive(true);
            AdControlText.text = "It seems you have no heal, you can earn heal by watching the ads.";
        }
        else
        {
            Destroy(GameObject.Find("Borular(Clone)"));
            Heal--;
            UseHeal++;
            UsedHeal.text = UseHeal.ToString();
            PlayerPrefs.SetInt("Heal", Heal);
            PlayerPrefs.SetInt("UseHeal", UseHeal);
            DeathScene.SetActive(false);
            Birdy.isDead = false;
            Birdy.rb2D.transform.position = new Vector2(0,0);
            Time.timeScale = 1;            
        }
    }
    public void UpdateScore()
    {            
        score += dif+1;
        ScoreText.text = score.ToString();
    }

    public void HealUpdate()
    {
        Heal = PlayerPrefs.GetInt("Heal");
        HealText.text = Heal.ToString();
    }

    public void BirdSkin()
    {
        RedBird.SetActive(false);
        BlueBird.SetActive(false);
        YellowBird.SetActive(false);
        if (Bird == 1)
        {
            RedBird.SetActive(true);       
            BirdDeger = 1;         
        }
        else if (Bird == 2)
        {
            BlueBird.SetActive(true);
            BirdDeger = 2;
        }
        else
        {
            YellowBird.SetActive(true);
            BirdDeger = 3;
        }
        
    }
    public void arttir()
    {
        Bird++;
        if (Bird>=4)
        {
            Bird = 1;
        }
        BirdSkin();
    }

    public void azalt()
    {
        Bird--;
        if (Bird <= 0)
        {
            Bird = 3;
        }
        BirdSkin();
    }

    public void HighScoreControl()
    {

        Hscore = PlayerPrefs.GetInt("HScore");
        if (Hscore<score)
        {
            Hscore = score;
            PlayerPrefs.SetInt("HScore", Hscore);
            HighScoreText.fontSize = (30);
            HighScoreText.text = "NEW HIGH SCORE";         
        }
        else
        {
            HighScoreText.text = "HIGH SCORE";
        }

        HighScore.text = Hscore.ToString();
        LastScore.text = score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Oyun");
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("SkinDegeri", BirdDeger);
        SceneManager.LoadScene("Oyun");
        
        
    }
    
    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void AdControlClose()
    {
        AdControl.SetActive(false);
    }

    public void Diffuculty(int index)
    {
        PlayerPrefs.SetInt("Dif", index);
    }

    public void FPSVoid(int fps)
    {      
        SettingsDropValue = fps;
    }

    public void SettingsPanelOpen()
    {
        Time.timeScale = 0;
        RedBird.SetActive(false);
        BlueBird.SetActive(false);
        YellowBird.SetActive(false);

        SettingsPanel.SetActive(true);

        SettingsDropdown.value = PlayerPrefs.GetInt("SettingsDV");
        ScrolBar.value = PlayerPrefs.GetFloat("Volume");
    }
    public void SettingsSave()
    {
        if (SettingsDropValue==0)
        {
            FPS = 30;
        }
        else if (SettingsDropValue == 1)
        {
            FPS = 60;
        }
        volume = ScrolBar.value;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("SettingsDV", SettingsDropValue);
        Application.targetFrameRate = FPS;
        PlayerPrefs.SetInt("FPS",FPS);
        
        SettingsClose();
    }
    public void SettingsClose()
    {
        SettingsPanel.SetActive(false);
        Time.timeScale = 1;
        BirdSkin();
    }


    
}