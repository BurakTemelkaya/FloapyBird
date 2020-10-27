using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Android;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdMob : MonoBehaviour
{
    public BannerView banner;
    private RewardedAd rewardedAd;

    public Text AdControlText;

    public GameObject AdControl;

    public GameManager managerGame;

    private int BanAd;
    void Start()
    {
        MobileAds.Initialize(reklam => { });
        if (PlayerPrefs.GetInt("Ban")!=1)
        {
            BannerReklam();
        }
        if (PlayerPrefs.GetInt("Rew")!=1)
        {
            CreateAndLoadRewardedAd();
        }
        

    }

    public void BannerReklam()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        banner = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();//reklam isteği

        banner.LoadAd(request);// reklam isteğini yükleme     

        BannerShow();
        PlayerPrefs.SetInt("Ban",1);
    }

    public void BannerShow()
    {       
        banner.Show();
    }
    public void BannerHide()
    {
       if(PlayerPrefs.GetInt("Ban") == 1)
        {
            banner.Hide();
        }
    }
    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        rewardedAd = new RewardedAd(adUnitId);

        
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        
    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        int Heal;
        Heal = PlayerPrefs.GetInt("Heal");
        Heal++;
        PlayerPrefs.SetInt("Heal",Heal);
        PlayerPrefs.SetInt("Rew",1);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        managerGame.HealUpdate();
        AdControl.SetActive(true);
        AdControlText.text = "Congratulations, you earned heal. :)";
        CreateAndLoadRewardedAd();
    }
    public void UserChoseToWatchAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            AdControl.SetActive(true);
            AdControlText.text = "Sorry, Ad video could not be uploaded. Try again later :(";
        }
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Ban", 0);
        PlayerPrefs.SetInt("Rew", 0);
    }


}
