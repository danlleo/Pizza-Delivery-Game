using UnityEngine;

namespace UI.InspectableObject
{
    public class InspectableObjectUI : MonoBehaviour
    {
        [SerializeField] private GameObject _inspectableObjectUI;

        private void Show()
            => _inspectableObjectUI.SetActive(true);

        private void Hide()
            => _inspectableObjectUI.SetActive(false);
    }
}
