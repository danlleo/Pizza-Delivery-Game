using System.Collections;
using UnityEngine;

namespace Environment.Bedroom.PC
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public abstract class Clickable : MonoBehaviour
    {
        public void HandleClick()
        {
            PCScreen.Instance.SetLoading(true);
            StartCoroutine(ActionPerformerRoutine());
        }
        
        protected abstract float DelayTimeInSeconds { get; set; }
        
        protected abstract void PerformAction();
        
        protected virtual void OnDestroy()
        {
            PCScreen.Instance.RemoveClickableObject(this);
        }

        private IEnumerator ActionPerformerRoutine()
        {
            yield return new WaitForSeconds(DelayTimeInSeconds);
            PerformAction();
            PCScreen.Instance.SetLoading(false);
        }
    }
}
