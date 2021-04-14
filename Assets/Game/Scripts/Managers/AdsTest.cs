// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AdsTest : Singleton<AdsTest>
// {
//     public static string appKey = "bc33f32d";

//     private void Awake()
//     {
//         IronSourceConfig.Instance.setClientSideCallbacks(true);
//         string id = IronSource.Agent.getAdvertiserId();
//         IronSource.Agent.validateIntegration();

//         IronSource.Agent.init(appKey);

//         IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
//         IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
//         // IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
//         IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
//         IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
//         IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
//         IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
//     }

//     void RewardedVideoAdOpenedEvent()
//     {

//     }

//     void RewardedVideoAdClosedEvent()
//     {

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

//     }

//     void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
//     {

//     }

//     void RewardedVideoAdShowFailedEvent(IronSourceError error)
//     {

//     }

//     public void WatchRewardVideoAds()
//     {
//         bool available = IronSource.Agent.isRewardedVideoAvailable();

//         if (available)
//         {
//             IronSource.Agent.showRewardedVideo();
//         }
//     }
// }
