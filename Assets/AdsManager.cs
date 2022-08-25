using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager instance;
    #if UNITY_IOS
         string gameId = "4896034";
        string[] adsName = {"Interstitial_iOS", "Rewarded_iOS", "Banner_iOS"}; 
    #else
        string gameId = "4896035";
        string[] adsName = { "Interstitial_Android", "Rewarded_Android", "Banner_Android" };
    #endif

    private Action onAdsSuccess;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
    }
    public void PlayAd(Action onSuccess)
    {
        if (Advertisement.IsReady(adsName[0]))
        {
            onAdsSuccess = onSuccess;
            Advertisement.Show(adsName[0]);
        }
    }
    public void PlayRewardedAd(Action onSuccess)
    {
        if (Advertisement.IsReady(adsName[1]))
        {
            onAdsSuccess = onSuccess;
            Advertisement.Show(adsName[1]);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log(placementId + " ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log(placementId + " start");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if ((placementId == adsName[1] || placementId == adsName[0]) && showResult == ShowResult.Finished)
        {
            onAdsSuccess.Invoke();
        }
    }
}
