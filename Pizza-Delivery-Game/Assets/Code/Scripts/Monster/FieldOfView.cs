using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Monster
{
    public class FieldOfView : MonoBehaviour
    {
        public GameObject PlayerGameObject { get; private set; }
        
        public float Radius => _radius;
        public float Angle => _angle;
        
        public bool CanSeePlayer { get; private set; }
        
        [Header("Settings")]
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstructionLayerMask;
        
        [SerializeField] private float _radius;
        [SerializeField, Range(0f, 360f)] private float _angle;

        
        private void Start()
        {
            PlayerGameObject = Player.Player.Instance.gameObject;
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            var wait = new WaitForSeconds(0.2f);
            
            while (true)
            {
                yield return wait;
                CheckFieldOfView();
            }
        }

        private void CheckFieldOfView()
        {
            Collider[] rangeCheck = Physics.OverlapSphere(transform.position, _radius, _targetMask);

            if (rangeCheck.Length <= 0)
            {
                CanSeePlayer = false;
                return;
            }
            
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // We divide angle by two because we have to check both sides from transform forward: \./
            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                // If within eyesight 
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionLayerMask))
                {
                    // If we detect target with our raycast
                    CanSeePlayer = true;
                }
                else
                {
                    CanSeePlayer = false;
                }

                return;
            }

            CanSeePlayer = false;
        }
    }
}