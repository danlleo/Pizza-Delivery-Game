using DG.Tweening;
using Environment.LaboratoryFirstLevel;
using Interfaces;
using UnityEngine;

namespace Monster
{
    public class MonsterCornerPeek : MonoBehaviour, ISpawnable
    {
        [Header("Settings")] 
        [SerializeField] private float _peekInTimeInSeconds;
        [SerializeField] private float _stayInPeekTimeInSeconds;
        [SerializeField] private float _peekOutTimeInSeconds;
        [SerializeField] private Vector3 _targetRotation;
        
        private Quaternion _initialRotation;

        private void Awake()
        {
            _initialRotation = transform.rotation;
        }
        
        public void OnSpawned()
        {
            this.CallMonsterPeakedStaticEvent();
            
            Sequence animationSequence = DOTween.Sequence();
            animationSequence.Append(transform.DORotate(_targetRotation, _peekInTimeInSeconds));
            animationSequence.AppendInterval(_stayInPeekTimeInSeconds);
            animationSequence.Append(transform.DORotate(_initialRotation.eulerAngles, _peekOutTimeInSeconds))
                .OnComplete(OnReturned);
        }

        public void OnReturned()
        {
            Destroy(gameObject);
        }
    }
}
