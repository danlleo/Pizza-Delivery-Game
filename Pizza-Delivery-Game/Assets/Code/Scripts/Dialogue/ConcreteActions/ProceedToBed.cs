using System.Collections;
using Environment.Bedroom;
using Misc;
using UI;
using UI.Crossfade;
using UnityEngine;

namespace Dialogue.ConcreteActions
{
    public class ProceedToBed : DialogueAction
    {
        [Header("External references")]
        [SerializeField] private OneHourLaterCanvas _oneHourLaterCanvas;

        private Crossfade _crossfade;

        protected override void Start()
        {
            _crossfade = FindObjectOfType<Crossfade>();
            base.Start();
        }

        protected override void Perform()
        {
            _crossfade.FadeIn(InputAllowance.DisableInput, () =>
            {
                StartCoroutine(WaitSpecificTimeBeforeDoorRingRoutine());
            }, 1.2f);
        }

        private IEnumerator WaitSpecificTimeBeforeDoorRingRoutine()
        {
            yield return new WaitForSeconds(2f);
            
            Instantiate(_oneHourLaterCanvas);
            
            yield return new WaitForSeconds(4f);
            
            _crossfade.FadeOut(() => {}, () =>
            {
                InputAllowance.EnableInput();
                WokeUpStaticEvent.Call(this);
                CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(true));
            }, 1.2f);
        }
    }
}
