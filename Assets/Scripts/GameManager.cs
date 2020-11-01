using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    public int Bird, BirdDeger, Heal, ZamanMenu, UseHeal, Hscore, score, FPS, SettingsDropValue,Quality;

    public float volume;

    public Text ScoreText, LastScore, HighScoreText, HighScore;

    public Text MenuHighScore,HighTime,HealText,UsedHeal,AdControlText;

    public GameObject RedBird, BlueBird, YellowBird, DeathScene,GameScreen,AdControl,SettingsPanel,ExitPanel;

    public Bird Birdy;

    public Dropdown dropdown,SettingsDropdown,QualityDropDown;

    public Scrollbar ScrolBar;

    private static AdMob ad = null;

    private void Awake()
    {
        if (ad == null)
        {
            ad = GameObject.Find("AdMob").GetComponent<AdMob>();
        }
        else if (ad!=null)
        {
            Destroy(ad);
        }
    }
    void Start()
    {
        
        if (!PlayerPrefs.HasKey("Save"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
            PlayerPrefs.SetInt("Quality", 1);
            PlayerPrefs.SetInt("FPS", 60);
            PlayerPrefs.SetInt("SettingsDV", 1);
            PlayerPrefs.SetInt("Save", 1);
        }
        if (PlayerPrefs.HasKey("HighZaman"))
        {
            HighTime.text = PlayerPrefs.GetString("HighZaman");
        }
        else
        {
            HighTime.text = "0";
        }
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        Application.targetFrameRate = PlayerPrefs.GetInt("FPS");       
        

        dropdown.value = PlayerPrefs.GetInt("Dif");
        Heal = PlayerPrefs.GetInt("Heal");
        HealText.text = Heal.ToString();
        MenuHighScore.text = PlayerPrefs.GetInt("HScore").ToString();
        
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
            GameScreen.SetActive(true);
            Birdy.isDead = false;
            Birdy.rb2D.transform.position = new Vector2(0,0);
            Time.timeScale = 1;            
        }
    }
    public void UpdateScore()
    {
        score += PlayerPrefs.GetInt("Dif") + 1;
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
        int dif=index;      
        PlayerPrefs.SetInt("Dif", dif);
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
        QualityDropDown.value = PlayerPrefs.GetInt("Quality");
    }
    public void SettingsSave()
    {
        if (SettingsDropValue==0)
            FPS = 30;
        else if (SettingsDropValue == 1)
            FPS = 60;
        else
            FPS=120;
        volume = ScrolBar.value;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("SettingsDV", SettingsDropValue);              
        PlayerPrefs.SetInt("Quality", Quality);
        QualitySettings.SetQualityLevel(Quality);
        PlayerPrefs.SetInt("FPS", FPS);
        Application.targetFrameRate = FPS;
        PlayerPrefs.SetInt("Save", 1);
        SettingsClose();
    }
    public void SettingsClose()
    {
        SettingsPanel.SetActive(false);
        Time.timeScale = 1;
        BirdSkin();
    }
    public void QualitySetting(int kalite)
    {Quality = kalite;}
    public void ExitPanelOpen()
    {ExitPanel.SetActive(true);}
    public void ExitPanelClose()
    {ExitPanel.SetActive(false);}
    public void Exit()
    { Application.Quit(); }
    public void AdVideoWatch()
    {
        ad.UserChoseToWatchAd();
    }
    public void AdControlPanelOpen()
    {
        AdControl.SetActive(true);
    }


    
}
