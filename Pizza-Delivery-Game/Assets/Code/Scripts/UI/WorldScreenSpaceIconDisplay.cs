using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WorldScreenSpaceIconDisplay : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private UI _ui;
        [SerializeField] private Transform _container;
        [SerializeField] private Image _worldScreenSpaceIconPrefab;

        private readonly Dictionary<IWorldScreenSpaceIcon, ScreenSpaceIconFollowWorld> _worldScreenSpaceIconDictionary =
            new();
        
        private void OnEnable()
        {
            _ui.WorldScreenSpaceIconDetectedEvent.OnWorldScreenSpaceIconDetected += OnWorldScreenSpaceIconDetected;
            _ui.WorldScreenSpaceIconLostEvent.OnWorldScreenSpaceIconLost += OnWorldScreenSpaceIconLost;
        }

        private void OnDisable()
        {
            _ui.WorldScreenSpaceIconDetectedEvent.OnWorldScreenSpaceIconDetected -= OnWorldScreenSpaceIconDetected;
            _ui.WorldScreenSpaceIconLostEvent.OnWorldScreenSpaceIconLost -= OnWorldScreenSpaceIconLost;
        }

        private bool IsInList(IWorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            return _worldScreenSpaceIconDictionary.TryGetValue(worldScreenSpaceIcon,
                out ScreenSpaceIconFollowWorld _);
        }

        private void Display(IWorldScreenSpaceIcon icon)
        {
            Image worldScreenSpaceIconImage = Instantiate(_worldScreenSpaceIconPrefab, _container);
            WorldScreenSpaceIcon worldScreenSpaceIcon = icon.GetWorldScreenSpaceIcon();
         
            var screenSpaceIconFollowWorld = worldScreenSpaceIconImage.gameObject.AddComponent<ScreenSpaceIconFollowWorld>();
    
            _worldScreenSpaceIconDictionary.Add(icon, screenSpaceIconFollowWorld);
            
            screenSpaceIconFollowWorld.Initialize(worldScreenSpaceIcon.LookAtTarget, worldScreenSpaceIcon.Offset);
        }

        private void Conceal(IWorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            if (!_worldScreenSpaceIconDictionary.TryGetValue(worldScreenSpaceIcon,
                    out ScreenSpaceIconFollowWorld screenSpaceIconFollowWorld)) return;
            
            Destroy(screenSpaceIconFollowWorld.gameObject);
            _worldScreenSpaceIconDictionary.Remove(worldScreenSpaceIcon);
        }
        
        private void OnWorldScreenSpaceIconDetected(object sender, WorldScreenSpaceIconDetectedEventArgs e)
        {
            if (IsInList(e.WorldScreenSpaceIcon)) return;
            
            Display(e.WorldScreenSpaceIcon);
        }
        
        private void OnWorldScreenSpaceIconLost(object sender, WorldScreenSpaceIconLostEventArgs e)
        {
            if (!IsInList(e.WorldScreenSpaceIcon)) return;

            Conceal(e.WorldScreenSpaceIcon);
        }
    }
}