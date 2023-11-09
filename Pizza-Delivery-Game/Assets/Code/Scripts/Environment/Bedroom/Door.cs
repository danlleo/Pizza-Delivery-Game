using Enums.Scenes;
using Interfaces;
using Misc;
using Misc.Loader;
using Sounds.Audio;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.Bedroom
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private UI.Crossfade _crossfade;
        [FormerlySerializedAs("_roomAudio")] [SerializeField] private BedroomAudio _bedroomAudio;
        
        public void Interact()
        {
            _crossfade.FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.TestingFeaturesScene));
            _bedroomAudio.PlayDoorOpenSound();
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Leave";
        }
    }
}
