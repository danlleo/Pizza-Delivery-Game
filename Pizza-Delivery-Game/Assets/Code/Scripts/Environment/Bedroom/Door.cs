using Enums.Scenes;
using Interfaces;
using Misc;
using Misc.Loader;
using Sounds.Audio;
using UI;
using UnityEngine;

namespace Environment.Bedroom
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private BedroomAudio _bedroomAudio;
        
        public void Interact()
        {
            Crossfade.Instance.FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.TestingFeaturesScene));
            _bedroomAudio.PlayDoorOpenSound();
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Leave";
        }
    }
}
