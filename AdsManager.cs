using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public void ShowRewardedAd()
    {
        Debug.Log("Showing Rewarded Ad");

        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions
            {
                resultCallback = HandleShowResult
            };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GameManager.Instance.Player.AddGems(100);
                UIManager.Instance.UpdateGemCount(GameManager.Instance.Player.amountOfDiamond);
                UIManager.Instance.OpenShop(GameManager.Instance.Player.amountOfDiamond);
                break;
            case ShowResult.Skipped:
                Debug.Log("You skipped the Ad! No Gems for you!");
                break;
            case ShowResult.Failed:
                Debug.Log("The Video failed, you must not have been ready!");
                break;
        }
    }
}
