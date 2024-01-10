using System.Collections;
using Environment.Bedroom;
using Misc;
using UI;
using UnityEngine;

namespace Dialogue.ConcreteActions
{
    public class ProceedToBed : DialogueAction
    {
        [Header("External references")]
        [SerializeField] private OneHourLaterCanvas _oneHourLaterCanvas;
        
        protected override void Perform()
        {
            ServiceLocator.ServiceLocator.GetCrossfadeService().FadeIn(InputAllowance.DisableInput, () =>
            {
                StartCoroutine(WaitSpecificTimeBeforeDoorRingRoutine());
            });
        }

        private IEnumerator WaitSpecificTimeBeforeDoorRingRoutine()
        {
            yield return new WaitForSeconds(2f);
            
            Instantiate(_oneHourLaterCanvas);
            
            yield return new WaitForSeconds(4f);
            
            ServiceLocator.ServiceLocator.GetCrossfadeService().FadeOut(() => {}, () =>
            {
                InputAllowance.EnableInput();
                WokeUpStaticEvent.Call(Player.Player.Instance);
                CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(true));
            });
        }
    }
}
