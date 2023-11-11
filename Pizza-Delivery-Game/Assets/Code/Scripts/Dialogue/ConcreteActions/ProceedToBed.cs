using System.Collections;
using Misc;
using Sounds.Audio;
using UI;
using UnityEngine;

namespace Dialogue.ConcreteActions
{
    public class ProceedToBed : DialogueAction
    {
        [SerializeField] private BedroomAudio _bedroomAudio;
        
        public override void Perform()
        {
            Crossfade.Instance.FadeIn(() => InputAllowance.DisableInput(), () =>
            {
                StartCoroutine(WaitSpecificTimeBeforeDoorRingRoutine());
            });
        }

        private IEnumerator WaitSpecificTimeBeforeDoorRingRoutine()
        {
            yield return new WaitForSeconds(2f);
            _bedroomAudio.PlayDoorOpenSound();
        }
    }
}
