using Enums.Scenes;
using Interfaces;
using Misc;
using Misc.Loader;
using UnityEngine;

namespace Environment.Bedroom
{
    [SelectionBase]   
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private UI.Crossfade _crossfade;
        
        public void Interact()
        {
            _crossfade.FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.TestingFeaturesScene));
        }

        public string GetActionDescription()
        {
            return "Leave";
        }
    }
}
