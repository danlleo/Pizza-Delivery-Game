using UnityEngine;

namespace Environment.Bedroom
{
    [DisallowMultipleComponent]
    public class Clock : MonoBehaviour
    {
        [SerializeField] private Transform _handSecondTransform;

        private void Update()
        {
            RotateHandSecond();
        }

        private void RotateHandSecond()
        {
            float degreesPerSecond = 360f / 60f;
            float rotationThisFrame = degreesPerSecond * Time.deltaTime * -1;

            _handSecondTransform.Rotate(0f, rotationThisFrame, 0f, Space.Self);
        }
    }
}
