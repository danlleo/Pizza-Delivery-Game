using System.Collections;
using Environment.Bedroom;
using Misc;
using UI;
using UnityEngine;
using Zenject;

namespace Dialogue.ConcreteActions
{
    public class ProceedToBed : DialogueAction
    {
        [Header("External references")]
        [SerializeField] private OneHourLaterCanvas _oneHourLaterCanvas;

        private Player.Player _player;
        
        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
        protected override void Perform()
        {
            ServiceLocator.ServiceLocator.GetCrossfadeService().FadeIn(InputAllowance.DisableInput, () =>
            {
                StartCoroutine(WaitSpecificTimeBeforeDoorRingRoutine());
            }, 1.2f);
        }

        private IEnumerator WaitSpecificTimeBeforeDoorRingRoutine()
        {
            yield return new WaitForSeconds(2f);
            
            Instantiate(_oneHourLaterCanvas);
            
            yield return new WaitForSeconds(4f);
            
            ServiceLocator.ServiceLocator.GetCrossfadeService().FadeOut(() => {}, () =>
            {
                InputAllowance.EnableInput();
                WokeUpStaticEvent.Call(_player);
                CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(true));
            }, 1.2f);
        }
    }
}
