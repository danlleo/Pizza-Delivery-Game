using UnityEngine;
using UnityEngine.UI;

namespace Misc
{
    public class CrosshairDisplayState : MonoBehaviour
    {
        [SerializeField] private Image _crosshair;

        private void Awake()
        {
            SetDisplayingState(true);
        }

        private void OnEnable()
        {
            CrosshairDisplayStateChangedStaticEvent.OnCursorStateChanged += OnCursorStateChanged_Event;
        }

        private void OnDisable()
        {
            CrosshairDisplayStateChangedStaticEvent.OnCursorStateChanged -= OnCursorStateChanged_Event;
        }

        private void SetDisplayingState(bool isDisplaying)
        {
            _crosshair.enabled = isDisplaying;
        }
        
        private void OnCursorStateChanged_Event(object sender, CrosshairDisplayStateChangedEventArgs e)
        {
            SetDisplayingState(e.IsDisplaying);
        }
    }
}
