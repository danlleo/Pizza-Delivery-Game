using Interfaces;
using Sounds.Audio;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.Bedroom
{
    [DisallowMultipleComponent]
    public class Lamp : MonoBehaviour, IInteractable
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        
        [Header("External references")]
        [SerializeField] private Light _lightSource;
        [SerializeField] private GameObject LampGameObject;
        [FormerlySerializedAs("_roomAudio")] [SerializeField] private BedroomAudio _bedroomAudio;

        [Header("Settings")] 
        [SerializeField] private bool _isOn;
        [SerializeField] private Material _lampOnMaterial;
        [SerializeField] private Material _lampOffMaterial;

        private void Awake()
        {
            ChangeMaterial();
        }

        public void Interact()
        {
            ToggleLight();
            ChangeMaterial();
            
            _bedroomAudio.PlayLampLightSwitchSound();
        }

        public string GetActionDescription()
        {
            return "Lamp";
        }

        private void ToggleLight()
        {
            _isOn = !_isOn;
            _lightSource.enabled = _isOn;
        }

        private void ChangeMaterial()
        {
            LampGameObject.GetComponent<MeshRenderer>().material = _isOn ? _lampOnMaterial : _lampOffMaterial;
        }
    }
}
