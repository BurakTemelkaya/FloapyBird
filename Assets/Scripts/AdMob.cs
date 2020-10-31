﻿using System.Collections;
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

    bool ban,rew;

    private static AdMob obje = null;

    private void Awake()
    {
        if (obje==null)
        {
            obje = this;
            DontDestroyOnLoad(this);
        }
        else if(this != obje)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        MobileAds.Initialize(reklam => { });
        if (!ban)
        {
            BannerReklam();
        }
        if (!rew)
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

        banner.OnAdLoaded += HandleOnAdLoaded;//Reklam isteği yüklendiğinde
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {       
        banner.Show();
        ban = true;
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

        AdRequest requestrew = new AdRequest.Builder().Build();//Reklam videosu isteği

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;//Reklam videosu başarılı bir şekilde yüklendiğinde
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;//Reklam Videosu tam izlendikten sonra ödül verme işlemi

        rewardedAd.OnAdClosed += HandleRewardedAdClosed;//Reklam videosu kapandıktan sonra yapılacak işlemler

        rewardedAd.LoadAd(requestrew);//Reklam videosu yükleme

    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        rew = true;
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
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            AdControl.SetActive(true);
            AdControlText.text = "The ad video was not uploaded because you are on mobile data.";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            managerGame.ExitPanelOpen();
    }




}
