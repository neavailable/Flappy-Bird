using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine;

namespace MainScene.AdSettings
{
    public class InterstitialAd : MonoBehaviour, IInterstitialAdListener
    {
        private const string _appKey = "010f76dd020b2e78b268dcf87da38c43a4c2b5c9a605cec6";
        [SerializeField] private TMPro.TMP_Text _text;
        [SerializeField] private TMPro.TMP_Text _text1;
        [SerializeField] private TMPro.TMP_Text _text2;
        [SerializeField] private TMPro.TMP_Text _text3;
        [SerializeField] private TMPro.TMP_Text _text4;
        [SerializeField] private TMPro.TMP_Text _text5;
        [SerializeField] private TMPro.TMP_Text _text6;
        
        private void Awake()
        {
            Appodeal.initialize(_appKey, Appodeal.INTERSTITIAL);
            
            Appodeal.setInterstitialCallbacks(this);
        }

        public void ShowInterstitialAd()
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
        
        public void onInterstitialLoaded(bool isPrecache)
        {
            _text.text = "onInterstitialLoaded\n";
        }
        public void onInterstitialFailedToLoad()
        {
            _text1.text = "onInterstitialFailedToLoad\n";
        }
        public void onInterstitialShowFailed()
        {
            _text2.text = "onInterstitialShowFailed\n";
        }
        
        public void onInterstitialShown()
        {
            _text3.text= "onInterstitialShown\n";
        }
        public void onInterstitialClosed()
        {
            _text4.text = "onInterstitialClosed\n";
        }
        public void onInterstitialClicked()
        {
            _text5.text= "onInterstitialClicked\n";
        }
        public void onInterstitialExpired()
        {
            _text6.text = "onInterstitialExpired\n";
        }
    }
}