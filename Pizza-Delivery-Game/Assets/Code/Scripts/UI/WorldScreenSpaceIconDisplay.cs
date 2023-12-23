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

        private List<IWorldScreenSpaceIcon> _worldScreenSpaceIconList = new();
        
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
            => _worldScreenSpaceIconList.Contains(worldScreenSpaceIcon);

        private void Display(IWorldScreenSpaceIcon icon)
        {
            _worldScreenSpaceIconList.Add(icon);
            
            Image worldScreenSpaceIconImage = Instantiate(_worldScreenSpaceIconPrefab, _container);
            WorldScreenSpaceIcon worldScreenSpaceIcon = icon.GetWorldScreenSpaceIcon();
    
            var screenSpaceIconFollowWorld = worldScreenSpaceIconImage.gameObject.AddComponent<ScreenSpaceIconFollowWorld>();
            
            screenSpaceIconFollowWorld.Initialize(worldScreenSpaceIcon.LookAtTarget, worldScreenSpaceIcon.Offset);
        }

        private void Conceal(IWorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            _worldScreenSpaceIconList.Remove(worldScreenSpaceIcon);
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