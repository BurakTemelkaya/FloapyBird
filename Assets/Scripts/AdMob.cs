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

    private void Start()
    {
        MobileAds.Initialize(reklam => { });

        if (!PlayerPrefs.HasKey("Ban"))
        {
            BannerReklam();
            
        }
        if (!PlayerPrefs.HasKey("Rew"))
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

        banner.OnAdLoaded += HandleOnAdLoaded;//Reklam isteği yüklendiğinde

        banner.LoadAd(request);// reklam isteğini yükleme      
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        banner.Show();
        PlayerPrefs.SetInt("Ban", 1);
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

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        
    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        PlayerPrefs.SetInt("Rew", 1);
    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        int Heal;
        Heal = PlayerPrefs.GetInt("Heal");
        Heal++;
        PlayerPrefs.SetInt("Heal",Heal);
        AdControl.SetActive(true);
        AdControlText.text = "Congratulations, you earned heal. :)";
    }
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        AdControl.SetActive(true);
        AdControlText.text = "You didn't win any rewards for closing the ad early. :(";
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        managerGame.HealUpdate();       
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Ban");
        PlayerPrefs.DeleteKey("Rew");
    }
    


}
