using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAdsMediationTestSuite.Api;
using Facebook.Unity;

public class AdsManager : Singleton<AdsManager>
{
    private string m_APP_ID = "ca-app-pub-8721698442392956~1232205283";

    private BannerView m_BannerView;
    private string m_BannerId = "ca-app-pub-8721698442392956/8871323474";
    public bool m_BannerLoaded;

    private InterstitialAd interstitial;
    private string m_InterId = "ca-app-pub-8721698442392956/5034707168";
    public bool m_WatchInter;

    private RewardedAd rewardedAd;
    private string m_RewardId = "ca-app-pub-8721698442392956/2114343439";


    public bool openRwdAds;
    private RewardType m_RewardType;


    private void Awake()
    {
        FB.Init();

        m_BannerLoaded = false;
        m_WatchInter = true;

        MobileAds.Initialize(initStatus => { });
        // MobileAds.Initialize(m_APP_ID);

        this.RequestBanner();
        this.RequestInter();
        this.RequestRewardVideo();
    }

    // private void Start()
    // {

    // }

    // public void _InitilizationOfSDF()
    // {
    //     MobileAds.Initialize(initStatus => { });

    //     this.RequestBanner();
    // }

    private void OnEnable()
    {
        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.LEVEL_END, LoadBanner);
        EventManager.AddListener(GameEvent.LEVEL_END, LoadInter);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.LEVEL_END, LoadBanner);
        EventManager.RemoveListener(GameEvent.LEVEL_END, LoadInter);
    }

    public void RequestBanner()
    {
#if UNITY_ANDROID
#elif UNITY_IPHONE
#endif
        AdSize adSize = new AdSize(320, 35);
        this.m_BannerView = new BannerView(m_BannerId, adSize, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.m_BannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.m_BannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.m_BannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.m_BannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.m_BannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();

        this.m_BannerView.LoadAd(request);
    }

    public void RequestInter()
    {
#if UNITY_ANDROID
#elif UNITY_IPHONE
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(m_InterId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void RequestRewardVideo()
    {
        this.rewardedAd = new RewardedAd(m_RewardId);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void LoadBanner()
    {
        AdSize adSize = new AdSize(320, 35);
        this.m_BannerView = new BannerView(m_BannerId, adSize, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.m_BannerView.LoadAd(request);
    }

    public void LoadInter()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void LoadRewardVideo()
    {
        this.rewardedAd = new RewardedAd(m_RewardId);
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

    public void WatchInterstitial()
    {
        if (m_WatchInter)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
            else
            {
                // RequestInter();
                LoadInter();
            }
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");

        m_BannerLoaded = true;
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);

        m_BannerLoaded = false;
        // LoadBanner();

        // LoadInter();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);

        // LoadRewardVideo();
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);

        LoadRewardVideo();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        // MonoBehaviour.print("HandleRewardedAdClosed event received");
        if (!openRwdAds)
        {
            switch (m_RewardType)
            {
                case RewardType.GOLD_1:
                    EventManager.CallEvent(GameEvent.ADS_GOLD_1_ANIM);
                    break;
                case RewardType.GOLD_2:
                    EventManager.CallEvent(GameEvent.ADS_GOLD_2_ANIM);
                    break;
                case RewardType.CHARACTER_2:
                    EventManager.CallEvent(GameEvent.ADS_CHARACTER_2_ANIM);
                    break;
            }
        }

        LoadRewardVideo();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        // string type = args.Type;
        // double amount = args.Amount;
        // MonoBehaviour.print(
        //     "HandleRewardedAdRewarded event received for "
        //                 + amount.ToString() + " " + type);

        if (openRwdAds)
        {
            openRwdAds = false;

            ProcessRewardVideo();
        }
    }

    public void ProcessRewardVideo()
    {
        StartCoroutine(IEProcessRewardVideo());
    }

    IEnumerator IEProcessRewardVideo()
    {
        yield return Yielders.EndOfFrame;

        switch (m_RewardType)
        {
            case RewardType.CHARACTER:
                EventManager.CallEvent(GameEvent.ADS_CHARACTER_LOGIC);
                break;
            case RewardType.CHARACTER_2:
                m_WatchInter = false;
                EventManager.CallEvent(GameEvent.ADS_CHARACTER_2_LOGIC);
                break;
            case RewardType.GOLD_1:
                EventManager.CallEvent(GameEvent.ADS_GOLD_1_LOGIC);
                break;
            case RewardType.GOLD_2:
                m_WatchInter = false;
                EventManager.CallEvent(GameEvent.ADS_GOLD_2_LOGIC);
                break;
        }
    }

    public void WatchRewardVideo(RewardType _rewardType)
    {
        StartCoroutine(IEWatchRewardVideo(_rewardType));
    }

    IEnumerator IEWatchRewardVideo(RewardType _rewardType)
    {
        // LoadRewardVideo();

        m_RewardType = _rewardType;
        openRwdAds = true;

        GUIManager.Instance.GetPanelLoadingAds().g_Content.SetActive(true);

        yield return Yielders.Get(1f);
        yield return Yielders.EndOfFrame;

        GUIManager.Instance.GetPanelLoadingAds().g_Content.SetActive(false);

        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            LoadRewardVideo();
            GUIManager.Instance.GetPanelLoadingAds().g_NetworkError.SetActive(true);
            yield return Yielders.Get(0.5f);
            GUIManager.Instance.GetPanelLoadingAds().g_NetworkError.SetActive(false);
        }
    }

    public void ShowMediationTestSuite()
    {
        MediationTestSuite.Show();
    }
}

public enum RewardType
{
    CHARACTER,
    CHARACTER_2,
    GOLD_1,
    GOLD_2,
}