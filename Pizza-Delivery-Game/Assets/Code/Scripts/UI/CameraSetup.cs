using UnityEngine;
using Zenject;

namespace UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Canvas))]
    public class CameraSetup : MonoBehaviour
    {
        private Canvas _canvas;
        private Player.Player _player;
        
        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = _player.UICamera;
        }
    }
}