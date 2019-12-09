using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityAdCaller : MonoBehaviour
{
    private int OpenWindowCount;

    private UnityAction callbackOnComplete;

public void PlayAd()
    {
        UnityAdsManager.Instance.ShowRegularAd(OnAdClosed);
    }
    public void PlayRewardedAd(UnityAction actionOnComplete)
    {
        callbackOnComplete = actionOnComplete;
        UnityAdsManager.Instance.ShowRewardedAd(OnRewardedAdClosed);
    }
    private void OnAdClosed(ShowResult result)
    {

    }
    private void OnRewardedAdClosed(ShowResult result)
    {
        Debug.Log("Rewarded ad closed");
        switch (result)
        {
            case ShowResult.Finished:
                callbackOnComplete?.Invoke();
                Debug.Log("Ad Finished, reward player");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped, no reward");
                break;
            case ShowResult.Failed:
                Debug.Log("Ad failed");
                break;
        }
    }
    public void OnShowInterstital()
    {
        OpenWindowCount = EventSystem.current.GetComponent<OpenCloseWindows>().OpenCount;
        if (OpenWindowCount % 8 == 0)
        {
            PlayAd();
        }
    }

    public void ShowRewardAdForChristmasHat()
    {
        PlayRewardedAd(() => { Debug.Log("Given player christmas hat"); });
    }
}
