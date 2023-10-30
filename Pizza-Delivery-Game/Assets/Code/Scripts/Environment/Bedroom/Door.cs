using Enums.Scenes;
using Interfaces;
using Misc;
using Misc.Loader;
using Sounds.Audio;
using UnityEngine;

namespace Environment.Bedroom
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private UI.Crossfade _crossfade;
        [SerializeField] private RoomAudio _roomAudio;
        
        public void Interact()
        {
            _crossfade.FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.TestingFeaturesScene));
            _roomAudio.PlayDoorOpenSound();
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Leave";
        }
    }
}
