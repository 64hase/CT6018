using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class UnityAdsManager : MonoBehaviour
{
    #region Instance
    public static UnityAdsManager instance;
    public static UnityAdsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UnityAdsManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned UnityAdsManager", typeof(UnityAdsManager)).GetComponent<UnityAdsManager>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion Instance
    [Header("Config")]
    [SerializeField] private string gameID = "";
    [SerializeField] private bool testmode = true;
    [SerializeField] private string rewardedVideoPlacementID;
    [SerializeField] private string regularPlacementID;


    private void Awake()
    {
        Advertisement.Initialize(gameID, testmode);
    }
    public void ShowRegularAd(Action<ShowResult> callback)
    {
#if UNITY_ADS
        if (Advertisement.IsReady(regularPlacementID))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(regularPlacementID, so);
        }
        else
            Debug.Log("Regular Ad not ready");
#else
        Debug.Log("Ads not supported");
#endif
    }
    public void ShowRewardedAd(Action<ShowResult> callback)
    {
#if UNITY_ADS
        if (Advertisement.IsReady(rewardedVideoPlacementID))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(rewardedVideoPlacementID, so);
        }
        else
            Debug.Log("Rewarded Video not ready");
#else
        Debug.Log("Ads not supported");
#endif
    }
}
