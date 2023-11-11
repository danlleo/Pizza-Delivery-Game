using System.Collections;
using Misc;
using UI;
using UnityEngine;

namespace Dialogue.ConcreteActions
{
    public class ProceedToBed : DialogueAction
    {
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
        }
    }
}
