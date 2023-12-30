using System;
using TMPro;
using UnityEngine;

namespace Tablet
{
    [DisallowMultipleComponent]
    public class TabletCanvas : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private TextMeshProUGUI _putdownTabletText;

        [Header("Settings")]
        [SerializeField] private float _textBlinkTimeInSeconds = 1f;

        private bool _isDisplaying;
        
        private void Awake()
        {
            _putdownTabletText.enabled = false;
        }

        private void OnEnable()
        {
            PickedUpStaticEvent.OnTabletPickedUp += OnAnyTabletPickedUp;
            PutDownStaticEvent.OnTabletPutDown += OnAnyTabletPutDown;
        }

        private void OnDisable()
        {
            PickedUpStaticEvent.OnTabletPickedUp -= OnAnyTabletPickedUp;
            PutDownStaticEvent.OnTabletPutDown -= OnAnyTabletPutDown;
        }

        private void Update()
        {
            if (!_isDisplaying) return;
            
            _putdownTabletText.alpha = Mathf.PingPong(Time.time * _textBlinkTimeInSeconds, 1f);
        }

        private void OnAnyTabletPickedUp(object sender, EventArgs e)
        {
            _isDisplaying = true;
            _putdownTabletText.enabled = true;
        }
        
        private void OnAnyTabletPutDown(object sender, EventArgs e)
        {
            _isDisplaying = false;
            _putdownTabletText.enabled = false;
        }
    }
}
