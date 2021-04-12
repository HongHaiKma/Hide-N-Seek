using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

// public class AdsManager : Singleton<AdsManager>
// {
//     public static string Android_Key = "bc33f32d";
//     private string m_ApplicationKey;

//     public bool openRwdAds;
//     private RewardType m_RewardType;

//     public bool m_WatchInter;

//     private void Awake()
//     {
// #if UNITY_ANDROID
//         m_ApplicationKey = Android_Key;
// #elif UNITY_IPHONE
//         // m_ApplicationKey = IOS_Key;
// #endif

//         IronSourceConfig.Instance.setClientSideCallbacks(true);
//         string id = IronSource.Agent.getAdvertiserId();
//         IronSource.Agent.validateIntegration();

//         IronSource.Agent.init(m_ApplicationKey);

//         IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
//         IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
//         // IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
//         IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
//         IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
//         IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
//         IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;


//         IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
//         IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
//         IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
//         IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
//         IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
//         IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

//         IronSource.Agent.loadInterstitial();
//     }

//     private void OnEnable()
//     {
//         // EventManager.AddListener(GameEvent.CHAR_WIN, WatchInterstitial);
//     }

//     private void OnDisable()
//     {
//         // EventManager.RemoveListener(GameEvent.CHAR_WIN, WatchInterstitial);
//     }

//     void RewardedVideoAdOpenedEvent()
//     {

//     }

//     #region VIDEO

//     void RewardedVideoAdClosedEvent()
//     {
//         if (!openRwdAds)
//         {
//             switch (m_RewardType)
//             {
//                 case RewardType.GOLD_1:
//                     EventManager.CallEvent(GameEvent.ADS_GOLD_1_ANIM);
//                     break;
//                 case RewardType.GOLD_2:
//                     EventManager.CallEvent(GameEvent.ADS_GOLD_2_ANIM);
//                     break;
//                 case RewardType.CHARACTER_2:
//                     EventManager.CallEvent(GameEvent.ADS_CHARACTER_2_ANIM);
//                     break;
//             }
//         }
//     }

//     void RewardedVideoAvailabilityChangedEvent(bool available)
//     {
//         bool rewardedVideoAvailability = available;
//     }

//     void RewardedVideoAdStartedEvent()
//     {

//     }

//     void RewardedVideoAdEndedEvent()
//     {
//         if (openRwdAds)
//         {
//             openRwdAds = false;

//             switch (m_RewardType)
//             {
//                 case RewardType.CHARACTER:
//                     EventManager.CallEvent(GameEvent.ADS_CHARACTER_LOGIC);
//                     break;
//                 case RewardType.CHARACTER_2:
//                     m_WatchInter = false;
//                     EventManager.CallEvent(GameEvent.ADS_CHARACTER_2_LOGIC);
//                     break;
//                 case RewardType.GOLD_1:
//                     EventManager.CallEvent(GameEvent.ADS_GOLD_1_LOGIC);
//                     break;
//                 case RewardType.GOLD_2:
//                     m_WatchInter = false;
//                     EventManager.CallEvent(GameEvent.ADS_GOLD_2_LOGIC);
//                     break;
//             }
//         }
//     }

//     void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
//     {
//         if (openRwdAds)
//         {
//             openRwdAds = false;

//             switch (m_RewardType)
//             {
//                 case RewardType.CHARACTER:
//                     EventManager.CallEvent(GameEvent.ADS_CHARACTER_LOGIC);
//                     break;
//                 case RewardType.CHARACTER_2:
//                     m_WatchInter = false;
//                     EventManager.CallEvent(GameEvent.ADS_CHARACTER_2_LOGIC);
//                     break;
//                 case RewardType.GOLD_1:
//                     EventManager.CallEvent(GameEvent.ADS_GOLD_1_LOGIC);
//                     break;
//                 case RewardType.GOLD_2:
//                     m_WatchInter = false;
//                     EventManager.CallEvent(GameEvent.ADS_GOLD_2_LOGIC);
//                     break;
//             }
//         }
//     }

//     void RewardedVideoAdShowFailedEvent(IronSourceError error)
//     {

//     }

//     public void WatchRewardVideoAds(RewardType _rewardType)
//     {
//         m_RewardType = _rewardType;
//         openRwdAds = true;
//         bool available = IronSource.Agent.isRewardedVideoAvailable();

//         if (available)
//         {
//             IronSource.Agent.showRewardedVideo();
//         }
//     }

//     #endregion

//     #region INTER

//     void InterstitialAdLoadFailedEvent(IronSourceError error)
//     {
//         IronSource.Agent.loadInterstitial();
//     }
//     //Invoked right before the Interstitial screen is about to open.
//     void InterstitialAdShowSucceededEvent()
//     {
//         IronSource.Agent.loadInterstitial();
//     }
//     //Invoked when the ad fails to show.
//     //@param description - string - contains information about the failure.
//     void InterstitialAdShowFailedEvent(IronSourceError error)
//     {
//         IronSource.Agent.loadInterstitial();
//     }
//     // Invoked when end user clicked on the interstitial ad
//     void InterstitialAdClickedEvent()
//     {

//     }
//     //Invoked when the interstitial ad closed and the user goes back to the application screen.
//     void InterstitialAdClosedEvent()
//     {
//         IronSource.Agent.loadInterstitial();
//     }
//     //Invoked when the Interstitial is Ready to shown after load function is called
//     void InterstitialAdReadyEvent()
//     {

//     }
//     //Invoked when the Interstitial Ad Unit has opened
//     void InterstitialAdOpenedEvent()
//     {

//     }

//     public void WatchInterstitial()
//     {
//         if (m_WatchInter)
//         {
//             if (IronSource.Agent.isInterstitialReady())
//             {
//                 IronSource.Agent.showInterstitial();
//             }
//         }
//     }

//     #endregion
// }

// public enum RewardType
// {
//     CHARACTER,
//     CHARACTER_2,
//     GOLD_1,
//     GOLD_2,
// }

public class AdsManager : Singleton<AdsManager>
{
    private BannerView m_BannerView;
    private string m_BannerId = "ca-app-pub-8721698442392956/1702569451";
    public bool m_BannerLoaded;

    private InterstitialAd interstitial;
    private string m_InterId = "ca-app-pub-8721698442392956/3416952287";
    public bool m_WatchInter;

    private RewardedAd rewardedAd;
    private string m_RewardId = "ca-app-pub-8721698442392956/4408623845";


    public bool openRwdAds;
    private RewardType m_RewardType;


    private void Awake()
    {
        m_BannerLoaded = false;
        m_WatchInter = true;

        MobileAds.Initialize(initStatus => { });

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

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
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
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
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

        LoadRewardVideo();
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
        LoadRewardVideo();

        m_RewardType = _rewardType;
        openRwdAds = true;
        // bool available = IronSource.Agent.isRewardedVideoAvailable();

        // if (available)
        // {
        //     IronSource.Agent.showRewardedVideo();
        // }

        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            LoadRewardVideo();
        }
    }
}

public enum RewardType
{
    CHARACTER,
    CHARACTER_2,
    GOLD_1,
    GOLD_2,
}