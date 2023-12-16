using System;
using Misc;
using Monster;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [RequireComponent(typeof(BoxCollider))]
    public class MonsterCornerPeekTrigger : MonoBehaviour
    {
        [SerializeField] private MonsterCornerPeek _monsterCornerPeek;

        private bool _canPeek;
        
        private void OnEnable()
        {
            PickedUpKeycardCStaticEvent.OnAnyPickedUpKeycardC += OnAnyPickedUpKeycardC;
        }

        private void OnDisable()
        {
            PickedUpKeycardCStaticEvent.OnAnyPickedUpKeycardC -= OnAnyPickedUpKeycardC;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_canPeek) return;
            if (!other.gameObject.TryGetComponent(out Player.Player _)) return;
            
            ObjectSpawner.Instance.SpawnObject<MonsterCornerPeek>(_monsterCornerPeek.gameObject,
                _monsterCornerPeek.transform.position, _monsterCornerPeek.transform.rotation);
            
            Destroy(gameObject);
        }

        private void OnAnyPickedUpKeycardC(object sender, EventArgs e)
        {
            _canPeek = true;
        }
    }
}