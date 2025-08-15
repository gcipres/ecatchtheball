using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAds : MonoBehaviour
{
    public static BannerView bannerView;
    
    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-4884669537343944/3111804875";
        #elif UNITY_IPHONE
            string adUnitId = "";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        var request = new AdRequest();
        bannerView.LoadAd(request);
        HideBanner();
    }

    public static void ShowBanner() {
        if (bannerView != null) {
            bannerView.Show();
        }
    }

    public static void HideBanner() {
        if (bannerView != null) {
            bannerView.Hide();
        }
    }
}