using System;
using Environment.Bedroom.PC.Programs;
using UnityEngine;

namespace Environment.Bedroom.PC
{
    [DisallowMultipleComponent]
    public class PCFlow : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Transform _clickableObjectsContainer;

        [Header("Config")] 
        [SerializeField] private BrowserPopUp[] _browserPopUps;
        [SerializeField] private BrowserJobWebSite _browserJobWebSite;

        private int _closedBrowserPopUpsCounter;
        
        private void OnEnable() 
        {
            Browser.OnAnyOpenedBrowser += Browser_OnAnyOpenedBrowser;
            BrowserPopUp.OnAnyClosedPopUp += BrowserPopUp_OnAnyClosedPopUp;
        }

        private void OnDisable()
        {
            Browser.OnAnyOpenedBrowser -= Browser_OnAnyOpenedBrowser;
            BrowserPopUp.OnAnyClosedPopUp -= BrowserPopUp_OnAnyClosedPopUp;
        }
        
        private void Browser_OnAnyOpenedBrowser(object sender, EventArgs e)
        {
            OnAnyPopUpsAppeared.Call(this);
            
            foreach (BrowserPopUp browserPopUp in _browserPopUps)
            {
                BrowserPopUp popUp = Instantiate(browserPopUp, _clickableObjectsContainer);
                popUp.Initialize();
            }
        }
        
        private void BrowserPopUp_OnAnyClosedPopUp(object sender, EventArgs e)
        {
            _closedBrowserPopUpsCounter++;

            if (_closedBrowserPopUpsCounter != _browserPopUps.Length) return;
            
            BrowserPopUp.OnAnyClosedPopUp -= BrowserPopUp_OnAnyClosedPopUp;
            BrowserJobWebSite browserJobWebSite = Instantiate(_browserJobWebSite, _clickableObjectsContainer);
            browserJobWebSite.Initialize();
        }
    }
}