using System;
using Common;
using Environment.Bedroom.PC;
using TMPro;
using UnityEngine;

namespace UI
{
    [DisallowMultipleComponent]
    public class JobApplyCanvas : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField, ChildrenOnly] private GameObject _jobApplyContainer;
        [SerializeField, ChildrenOnly] private TextMeshProUGUI _jobApplyText;
        
        [Header("Settings")]
        [SerializeField] private float _textBlinkTimeInSeconds = 1f;
        
        private bool _isDisplaying;
        
        private void Awake()
        {
            _jobApplyContainer.SetActive(false);
        }

        private void OnEnable()
        {
            Environment.Bedroom.PC.OnAnyJobReviewing.Event += OnAnyJobReviewing;
        }

        private void OnDisable()
        {
            Environment.Bedroom.PC.OnAnyJobReviewing.Event -= OnAnyJobReviewing;
        }
        
        private void Update()
        {
            if (!_isDisplaying) return;
            
            _jobApplyText.alpha = Mathf.PingPong(Time.time * _textBlinkTimeInSeconds, 1f);

            if (!Input.GetKeyDown(KeyCode.E)) return;
            
            OnAnyStoppedUsingPC.Call(this);
            Destroy(gameObject);
        }
        
        private void OnAnyJobReviewing(object sender, EventArgs e)
        {
            _isDisplaying = true;
            _jobApplyContainer.SetActive(true);
        }
    }
}
