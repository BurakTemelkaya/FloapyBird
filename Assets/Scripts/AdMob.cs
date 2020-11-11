using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMob : MonoBehaviour
{
    public BannerView banner;
    private RewardedAd rewardedAd;

    public GameManager managerGame = null;

    bool rew,inte;

    public static AdMob obje = null;

    private InterstitialAd interstitial;

    public int dead;

    private void Awake()
    {
        if (obje == null)
        {
            obje = this;
            DontDestroyOnLoad(this);
        }
        else if (this != obje)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        MobileAds.Initialize(reklam => { });
        if (!rew)
        {
            CreateAndLoadRewardedAd();
        }
        if (!inte)
        {
            RequestInterstitial();
        }
       
        
    }
    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";//ca-app-pub-6643171955921787/6886730600
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
        managerGame.AdControl.SetActive(true);
        managerGame.AdControlText.text = "Congratulations, you earned heal. :)";
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
            managerGame.AdControl.SetActive(true);
            managerGame.AdControlText.text = "Sorry, Ad video could not be uploaded. Try again later :(";
        }
    }
    public void RequestInterstitial()
    {

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";//ca-app-pub-6643171955921787/5375239745
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif
        if (interstitial != null)
            interstitial.Destroy();
        
        interstitial = new InterstitialAd(adUnitId);

        interstitial.OnAdLoaded += HandleOnAdLoaded;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        inte = true;
    }
    public void InstentiateControl()
    {
        dead++;
        if (dead == 5)
        {
            dead = 0;
            ShowInstersitial();
        }
    }
    public void ShowInstersitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            RequestInterstitial();
        }                            
    }

    public void GameManagerGetCompenent()
    {
        managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
    }




}
