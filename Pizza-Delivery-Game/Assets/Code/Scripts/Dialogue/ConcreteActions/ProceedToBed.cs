using System.Collections;
using Environment.Bedroom;
using Misc;
using UI;
using UI.Crossfade;
using UnityEngine;
using Zenject;

namespace Dialogue.ConcreteActions
{
    public class ProceedToBed : DialogueAction
    {
        [Header("External references")]
        [SerializeField] private OneHourLaterCanvas _oneHourLaterCanvas;

        private Player.Player _player;
        private Crossfade _crossfade;
        
        [Inject]
        private void Construct(Player.Player player, Crossfade crossfade)
        {
            _player = player;
            _crossfade = crossfade;
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
                WokeUpStaticEvent.Call(_player);
                CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(true));
            }, 1.2f);
        }
    }
}
