using Enums.Scenes;
using Interfaces;
using Misc.Loader;
using Player.Inventory;
using UI.Crossfade;
using UnityEngine;
using Zenject;

namespace Environment.LaboratorySecondLevel
{
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("External references")]
        [SerializeField] private ItemSO _pizzaBox;

        private Player.Player _player;
        private Monster.Monster _monster;
        private Crossfade _crossfade;
        
        [Inject]
        private void Construct(Player.Player player, Monster.Monster monster, Crossfade crossfade)
        {
            _player = player;
            _monster = monster;
            _crossfade = crossfade;
        }
        
        public void Interact()
        {
            if (!_player.Inventory.HasItem(_pizzaBox))
            {
                NoPizzaBoxStaticEvent.Call(this);
                return;
            }

            _crossfade
                .FadeIn(
                    () => Destroy(_monster.gameObject),
                    () => Loader.Load(Scene.EndingScene), 1.5f);
            
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Door";
        }
    }
}
