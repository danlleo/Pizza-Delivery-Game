using System.Collections;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerMovement : MonoBehaviour
    {
        [Header("External References")]
        [SerializeField] private Player _player;
        
        [Header("Settings")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _sprintSpeed;
        [SerializeField] private float _staminaRecoverDelayInSeconds;
        
        private CharacterController _characterController;
        
        private Coroutine _recoverStaminaRoutine;
        private Coroutine _delayStaminaRecoverRoutine;
        
        private bool _canSprint = true;
        
        private float _maxStaminaPercent = 100f;
        private float _staminaPercent = 100f;
        private float _initialMoveSpeed;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector2 input)
        {
            // We multiple direction in which player is looking by direction on vertical input,
            // that way we can move him forward or backwards, plus we add player's horizontal direction 
            // by horizontal input, to move him to the right or left
            Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
            
            _characterController.Move(moveDirection * (_moveSpeed * Time.deltaTime));
        }

        #region Sprint

        public void BeginSprint()
        {
            if (!_canSprint)
                return;
            
            if (_recoverStaminaRoutine != null)
                StopCoroutine(_recoverStaminaRoutine);
            
            if (_delayStaminaRecoverRoutine != null)
                StopCoroutine(_delayStaminaRecoverRoutine);
            
            _initialMoveSpeed = _moveSpeed;
            _moveSpeed *= _sprintSpeed;
        }

        public void Sprint()
        {
            if (!_canSprint)
                return;
            
            if (_recoverStaminaRoutine != null)
                StopCoroutine(_recoverStaminaRoutine);
            
            if (_delayStaminaRecoverRoutine != null)
                StopCoroutine(_delayStaminaRecoverRoutine);
            
            if (_staminaPercent <= 0)
            {
                StopSprint();
                return;
            }
            
            DecreaseStaminaOverTimeBy(.20f);
            _player._staminaEvent.Call(this, new StaminaEventArgs(_staminaPercent));
        }
        
        public void StopSprint()
        {
            _moveSpeed = _initialMoveSpeed;
            
            if (_recoverStaminaRoutine != null)
                StopCoroutine(_recoverStaminaRoutine);
            
            if (_delayStaminaRecoverRoutine != null)
                StopCoroutine(_delayStaminaRecoverRoutine);
            
            _delayStaminaRecoverRoutine = StartCoroutine(DelayStaminaRecoverRoutine());
        }

        private void DecreaseStaminaOverTimeBy(float decreaseValue)
        {
            _staminaPercent -= decreaseValue;
        }

        private void RecoverStaminaOverTimeBy(float increaseValue)
        {
            if (_staminaPercent >= _maxStaminaPercent)
            {
                _staminaPercent = _maxStaminaPercent;
                return;
            }

            _staminaPercent += increaseValue;
            _player._staminaEvent.Call(this, new StaminaEventArgs(_staminaPercent));
        }

        private IEnumerator RecoverStaminaRoutine()
        {
            while (_staminaPercent < _maxStaminaPercent)
            {
                RecoverStaminaOverTimeBy(.10f);
                yield return null;
            }
        }

        private IEnumerator DelayStaminaRecoverRoutine()
        {
            yield return new WaitForSeconds(_staminaRecoverDelayInSeconds);
            StartCoroutine(RecoverStaminaRoutine());
        }

        #endregion
    }
}