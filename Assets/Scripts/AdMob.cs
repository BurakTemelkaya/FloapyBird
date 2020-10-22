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

    public int Heal;
    void Start()
    {
        BannerReklam();
        MobileAds.Initialize(reklam => { });
        CreateAndLoadRewardedAd();
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

        AdRequest request = new AdRequest.Builder().Build();

        banner.LoadAd(request);

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
        Heal = PlayerPrefs.GetInt("Heal");
        Heal++;
        PlayerPrefs.SetInt("Heal",Heal);
    }
    public void DestroyBanner()
    {
        banner.Destroy();
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


}
