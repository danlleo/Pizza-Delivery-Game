using System;
using Interfaces;
using Player.Inventory;
using UnityEngine;
using Zenject;

namespace Environment.LaboratorySecondLevel
{
    [DisallowMultipleComponent]
    public class PizzaBox : MonoBehaviour, IInteractable
    {
        public static event EventHandler OnAnyPlayerTriedTakePizzaWhileBeenChased;
        public static event EventHandler OnAnyPlayerPickedUpPizzaBox;
        
        [Header("External references")]
        [SerializeField] private ItemSO _pizzaBox;

        private Player.Player _player;
        private Monster.Monster _monster;

        private bool _isPlayerChasedByMonster;
        
        [Inject]
        private void Construct(Player.Player player, Monster.Monster monster)
        {
            _player = player;
            _monster = monster;
        }

        private void Awake()
        {
            _isPlayerChasedByMonster = false;
        }

        private void OnEnable()
        {
            _monster.StartedChasingEvent.Event += Monster_OnStartedChase;
            _monster.StoppedChasingEvent.Event += Monster_OnStoppedChase;
        }

        private void OnDisable()
        {
            _monster.StartedChasingEvent.Event -= Monster_OnStartedChase;
            _monster.StoppedChasingEvent.Event -= Monster_OnStoppedChase;
        }
        
        public void Interact()
        {
            if (_isPlayerChasedByMonster)
            {
                OnAnyPlayerTriedTakePizzaWhileBeenChased?.Invoke(this, EventArgs.Empty);
                return;
            }
            
            OnAnyPlayerPickedUpPizzaBox?.Invoke(this, EventArgs.Empty);
            _player.Inventory.TryAddItem(_pizzaBox, out bool _);
            
            Destroy(gameObject);
        }

        public string GetActionDescription()
        {
            return "Pizza box";
        }
        
        private void Monster_OnStartedChase(object sender, EventArgs e)
        {
            _isPlayerChasedByMonster = true;
        }

        private void Monster_OnStoppedChase(object sender, EventArgs e)
        {
            _isPlayerChasedByMonster = false;
        }
    }
}
