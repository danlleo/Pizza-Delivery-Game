﻿using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace UI
{
    [DisallowMultipleComponent]
    public class WorldScreenSpaceIconDisplay : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private UI _ui;
        [SerializeField] private Transform _container;
        [SerializeField] private ScreenSpaceIconFollowWorld _screenSpaceIconFollowWorldPrefab;

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
            WorldScreenSpaceIcon worldScreenSpaceIcon = icon.GetWorldScreenSpaceIcon();
            ScreenSpaceIconFollowWorld screenSpaceIconFollowWorld = Instantiate(_screenSpaceIconFollowWorldPrefab, _container);
    
            _worldScreenSpaceIconDictionary.Add(icon, screenSpaceIconFollowWorld);
            
            screenSpaceIconFollowWorld.InitializeAndDisplay(worldScreenSpaceIcon.LookAtTarget, worldScreenSpaceIcon.Offset);
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