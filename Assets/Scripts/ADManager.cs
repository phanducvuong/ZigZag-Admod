using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADManager : MonoBehaviour
{
    private static ADManager instance;
    private InterstitialAd InterstitialAd;

    public static ADManager Instance {
        get {
            if (instance == null)
                instance = GameObject.FindObjectOfType<ADManager>();
            return instance;
        }
    }

    private void Awake() {
        _RequestInterstitialAd();
    }

    public void _RequestInterstitialAd() {
        string id = "ca-app-pub-3940256099942544/1033173712";
        InterstitialAd = new InterstitialAd(id);
        AdRequest req= new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        InterstitialAd.LoadAd(req);
    }

    public void _DisplayInterstitialAd() {
        if (InterstitialAd.IsLoaded()) {
            InterstitialAd.Show();
        }
        this.InterstitialAd.Destroy();
    }
}
