using System.Collections.Generic;
using Interfaces;
using Sounds.Audio;
using UnityEngine;

namespace Environment.Bedroom
{
    public class LightSwitch : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _lightSwitch;
        [SerializeField] private RoomAudio _roomAudio;
        [SerializeField] private Light[] _lightSources;

        [Header("Lightmaps")]
        [Space(10)]
        [SerializeField] private Texture2D[] _darkLightmapDir;
        [SerializeField] private Texture2D[] _darkLightmapColor;
        
        [Space(5)]
        [SerializeField] private Texture2D[] _brightLightmapDir;
        [SerializeField] private Texture2D[] _brightLightmapColor;

        private LightmapData[] _darkLightmapData;
        private LightmapData[] _brightLightmapData;
        
        private bool _isTurnedOn;
        
        private void Start()
        {
            InitializeDarkLightmap();
            InitializeBrightLightmap();
            ToggleLightSources(_isTurnedOn);
        }

        private void InitializeDarkLightmap()
        {
            var dlightmap = new List<LightmapData>();

            for (int i = 0; i < _darkLightmapDir.Length; i++)
            {
                var lmdata = new LightmapData();

                lmdata.lightmapDir = _darkLightmapDir[i];
                lmdata.lightmapColor = _darkLightmapColor[i];
                
                dlightmap.Add(lmdata);
            }

            _darkLightmapData = dlightmap.ToArray();
        }

        private void InitializeBrightLightmap()
        {
            var blightmap = new List<LightmapData>();

            for (int i = 0; i < _brightLightmapDir.Length; i++)
            {
                var lmdata = new LightmapData();

                lmdata.lightmapDir = _brightLightmapDir[i];
                lmdata.lightmapColor = _brightLightmapColor[i];
                
                blightmap.Add(lmdata);
            }

            _brightLightmapData = blightmap.ToArray();
        }

        private void SwitchLightmap(bool isTurnedOn)
        {
            if (isTurnedOn)
            {
                LightmapSettings.lightmaps = _darkLightmapData;
                return;
            }
            
            LightmapSettings.lightmaps = _brightLightmapData;
        }

        private void ToggleLightSources(bool isTurnedOn)
        {
            for (int i = 0; i < _lightSources.Length; i++)
                _lightSources[i].enabled = isTurnedOn;
        }
        
        public void Interact()
        {
            _lightSwitch.transform.localRotation = Quaternion.Euler(_isTurnedOn ? new Vector3(0f, 0f, -180f) : Vector3.zero);
            _isTurnedOn = !_isTurnedOn;
            
            SwitchLightmap(_isTurnedOn);
            ToggleLightSources(_isTurnedOn);
            _roomAudio.PlayRoomLightSwitchSound();
        }

        public string GetActionDescription()
        {
            return "Light switch";
        }
    }
}
