using Interfaces;
using Sounds.Audio;
using UnityEngine;

namespace Environment.Bedroom
{
    public class LightSwitch : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _lightSwitch;
        [SerializeField] private Material _lightSwitchOnMaterial;
        [SerializeField] private RoomAudio _roomAudio;
        
        public void Interact()
        {
            _lightSwitch.GetComponent<MeshRenderer>().material = _lightSwitchOnMaterial;
            _lightSwitch.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _roomAudio.PlaySwitchOnSound();
            
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Turn on the light";
        }
    }
}
