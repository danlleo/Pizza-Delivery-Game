using InspectableObject;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    public class Wrench : UI.InspectableObject.InspectableObject
    {
        [SerializeField] private InspectableObjectSO _wrenchInspectableObject;
        [SerializeField] private InspectableObjectSO _wrenchAfterGasLeakInspectableObject;

        private InspectableObjectSO _target;

        protected override InspectableObjectSO InspectableObjectSO => _target;
        protected override string ActionDescription => "Wrench";

        private void Awake()
        {
            _target = _wrenchInspectableObject;
        }

        private void OnEnable()
        {
            GasLeakedStaticEvent.OnAnyGasLeaked += OnAnyGasLeaked;
        }

        private void OnDisable()
        {
            GasLeakedStaticEvent.OnAnyGasLeaked -= OnAnyGasLeaked;
        }

        private void OnAnyGasLeaked(object sender, GasLeakedStaticEventArgs e)
        {
            _target = _wrenchAfterGasLeakInspectableObject;
        }
    }
}
