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

    bool rew;

    public static AdMob obje = null;

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
        if (!rew)
        {
            CreateAndLoadRewardedAd();
        }
    }
    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";//ca-app-pub-6643171955921787/6886730600//gerçek reklam birimi
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
    public void GameManagerGetCompenent()
    {
        managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            managerGame.ExitPanelOpen();
    }




}
