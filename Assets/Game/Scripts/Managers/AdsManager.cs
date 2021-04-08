using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : Singleton<AdsManager>
{
    public static string Android_Key = "bc33f32d";
    private string m_ApplicationKey;

    public bool openRwdAds;
    private RewardType m_RewardType;

    public bool m_WatchInter;

    private void Awake()
    {
#if UNITY_ANDROID
        m_ApplicationKey = Android_Key;
#elif UNITY_IPHONE
        // m_ApplicationKey = IOS_Key;
#endif

        IronSourceConfig.Instance.setClientSideCallbacks(true);
        string id = IronSource.Agent.getAdvertiserId();
        IronSource.Agent.validateIntegration();

        IronSource.Agent.init(m_ApplicationKey);

        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        // IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;


        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        IronSource.Agent.loadInterstitial();
    }

    private void OnEnable()
    {
        // EventManager.AddListener(GameEvent.CHAR_WIN, WatchInterstitial);
    }

    private void OnDisable()
    {
        // EventManager.RemoveListener(GameEvent.CHAR_WIN, WatchInterstitial);
    }

    void RewardedVideoAdOpenedEvent()
    {

    }

    #region VIDEO

    void RewardedVideoAdClosedEvent()
    {
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
    }

    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        bool rewardedVideoAvailability = available;
    }

    void RewardedVideoAdStartedEvent()
    {

    }

    void RewardedVideoAdEndedEvent()
    {
        if (openRwdAds)
        {
            openRwdAds = false;

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
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        if (openRwdAds)
        {
            openRwdAds = false;

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
    }

    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {

    }

    public void WatchRewardVideoAds(RewardType _rewardType)
    {
        m_RewardType = _rewardType;
        openRwdAds = true;
        bool available = IronSource.Agent.isRewardedVideoAvailable();

        if (available)
        {
            IronSource.Agent.showRewardedVideo();
        }
    }

    #endregion

    #region INTER

    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        IronSource.Agent.loadInterstitial();
    }
    //Invoked right before the Interstitial screen is about to open.
    void InterstitialAdShowSucceededEvent()
    {
        IronSource.Agent.loadInterstitial();
    }
    //Invoked when the ad fails to show.
    //@param description - string - contains information about the failure.
    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        IronSource.Agent.loadInterstitial();
    }
    // Invoked when end user clicked on the interstitial ad
    void InterstitialAdClickedEvent()
    {

    }
    //Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent()
    {
        IronSource.Agent.loadInterstitial();
    }
    //Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent()
    {

    }
    //Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent()
    {

    }

    public void WatchInterstitial()
    {
        if (m_WatchInter)
        {
            if (IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial();
            }
        }
    }

    #endregion
}

public enum RewardType
{
    CHARACTER,
    CHARACTER_2,
    GOLD_1,
    GOLD_2,
}