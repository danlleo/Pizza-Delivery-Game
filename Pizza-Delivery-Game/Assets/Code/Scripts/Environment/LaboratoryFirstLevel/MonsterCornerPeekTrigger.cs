using Enums.Keycards;
using Misc;
using Monster;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [RequireComponent(typeof(BoxCollider))]
    public class MonsterCornerPeekTrigger : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private MonsterCornerPeek _monsterCornerPeek;

        private bool _canPeek;
        
        private void OnEnable()
        {
            Keycard.OnAnyPickedUpKeycard += OnAnyPickedUpKeycard;
        }

        private void OnDisable()
        {
            Keycard.OnAnyPickedUpKeycard -= OnAnyPickedUpKeycard;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_canPeek) return;
            if (!other.gameObject.TryGetComponent(out Player.Player _)) return;
            
            ObjectSpawner.Instance.SpawnObject<MonsterCornerPeek>(_monsterCornerPeek.gameObject,
                _monsterCornerPeek.transform.position, _monsterCornerPeek.transform.rotation);
            
            Destroy(gameObject);
        }

        private void OnAnyPickedUpKeycard(object sender, Keycard.OnAnyPickedUpKeycardEventArgs e)
        {
            if (e.KeycardType == KeycardType.KeycardC)
                _canPeek = true;
        }
    }
}