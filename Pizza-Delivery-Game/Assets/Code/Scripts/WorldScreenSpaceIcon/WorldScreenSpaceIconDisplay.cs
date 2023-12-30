using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace WorldScreenSpaceIcon
{
    [DisallowMultipleComponent]
    public class WorldScreenSpaceIconDisplay : MonoBehaviour
    {
        [Header("External references")] [SerializeField]
        private Transform _container;

        [SerializeField] private ScreenSpaceIconFollowWorld _screenSpaceIconFollowWorldPrefab;

        private readonly Dictionary<WorldScreenSpaceIcon, ScreenSpaceIconFollowWorld> _worldScreenSpaceIconDictionary =
            new();

        private void OnEnable()
        {
            WorldScreenSpaceIconDetectedStaticEvent.OnAnyWorldScreenSpaceIconDetected +=
                OnAnyWorldScreenSpaceIconDetected;
            WorldScreenSpaceIconLostStaticEvent.OnAnyWorldScreenSpaceIconLost += OnAnyWorldScreenSpaceIconLost;
            WorldScreenSpaceIconLostAllStaticEvent.OnAnyWorldScreenSpaceIconLostAll += OnAnyWorldScreenSpaceIconLostAll;
        }

        private void OnDisable()
        {
            WorldScreenSpaceIconDetectedStaticEvent.OnAnyWorldScreenSpaceIconDetected -=
                OnAnyWorldScreenSpaceIconDetected;
            WorldScreenSpaceIconLostStaticEvent.OnAnyWorldScreenSpaceIconLost -= OnAnyWorldScreenSpaceIconLost;
            WorldScreenSpaceIconLostAllStaticEvent.OnAnyWorldScreenSpaceIconLostAll -= OnAnyWorldScreenSpaceIconLostAll;
        }

        private bool IsInList(WorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            if (worldScreenSpaceIcon == null)
                return false;

            return _worldScreenSpaceIconDictionary.TryGetValue(worldScreenSpaceIcon,
                out ScreenSpaceIconFollowWorld _);
        }

        private void Display(WorldScreenSpaceIcon icon)
        {
            WorldScreenSpaceIconData worldScreenSpaceIconData = icon.GetWorldScreenSpaceIconData();
            ScreenSpaceIconFollowWorld screenSpaceIconFollowWorld =
                Instantiate(_screenSpaceIconFollowWorldPrefab, _container);

            _worldScreenSpaceIconDictionary.Add(icon, screenSpaceIconFollowWorld);

            screenSpaceIconFollowWorld.InitializeAndDisplay(worldScreenSpaceIconData.LookAtTarget,
                worldScreenSpaceIconData.Offset);
        }

        private void ConcealSingle(WorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            if (!_worldScreenSpaceIconDictionary.TryGetValue(worldScreenSpaceIcon,
                    out ScreenSpaceIconFollowWorld screenSpaceIconFollowWorld)) return;

            Destroy(screenSpaceIconFollowWorld.gameObject);

            _worldScreenSpaceIconDictionary.Remove(worldScreenSpaceIcon);
        }

        private void ConcealAll()
        {
            foreach (KeyValuePair<WorldScreenSpaceIcon, ScreenSpaceIconFollowWorld> pair in
                     _worldScreenSpaceIconDictionary) Destroy(pair.Value.gameObject);

            _worldScreenSpaceIconDictionary.Clear();
        }

        private void OnAnyWorldScreenSpaceIconDetected(object sender, WorldScreenSpaceIconDetectedEventArgs e)
        {
            if (IsInList(e.WorldScreenSpaceIcon)) return;

            Display(e.WorldScreenSpaceIcon);
        }

        private void OnAnyWorldScreenSpaceIconLost(object sender, WorldScreenSpaceIconLostEventArgs e)
        {
            if (!IsInList(e.WorldScreenSpaceIcon)) return;

            ConcealSingle(e.WorldScreenSpaceIcon);
        }

        private void OnAnyWorldScreenSpaceIconLostAll(object sender, EventArgs e)
        {
            ConcealAll();
        }
    }
}