using System.Collections;
using UnityEngine;
using Utilities;

namespace Common
{
    [DisallowMultipleComponent]
    public class ModelViewCamera : MonoBehaviour
    {
        private const string UI_LAYER = "UI";
        
        [Header("Settings")]
        [SerializeField] private float _objectRotationSpeed = 35f;
        [SerializeField, ChildrenOnly] private Transform _objectContainer;

        public void Display(GameObject displayGameObject)
        {
            if (_objectContainer.childCount > 0)
                foreach (Transform child in _objectContainer)
                    Destroy(child.gameObject);
            
            displayGameObject.SetParent(_objectContainer);
            displayGameObject.transform.localPosition = Vector3.zero;
            
            SetLayersFor(displayGameObject);
            StartCoroutine(RotateObjectRoutine(displayGameObject));
        }
        
        private IEnumerator RotateObjectRoutine(GameObject targetGameObject)
        {
            while (true)
            {
                targetGameObject.transform.Rotate(Vector3.up * (_objectRotationSpeed * Time.deltaTime));
                targetGameObject.transform.Rotate(Vector3.forward * (_objectRotationSpeed * Time.deltaTime));

                yield return null;
            }
        }
        
        private void SetLayersFor(GameObject target)
        {
            target.layer = LayerMask.NameToLayer(UI_LAYER);

            foreach (Transform child in target.transform) child.gameObject.layer = LayerMask.NameToLayer(UI_LAYER);
        }
    }
}
