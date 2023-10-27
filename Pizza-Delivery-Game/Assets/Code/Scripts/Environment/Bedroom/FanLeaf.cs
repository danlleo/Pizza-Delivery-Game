using UnityEngine;

namespace Environment.Bedroom
{
    public class FanLeaf : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 1.2f;

        private void Update()
        {
            transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}
