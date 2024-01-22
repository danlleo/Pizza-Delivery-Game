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
            OnAnyPCLoading.Call(this, new OnAnyPCLoadingEventArgs(true));
            StartCoroutine(ActionPerformerRoutine());
        }
        
        protected abstract float DelayTimeInSeconds { get; }
        
        protected abstract void PerformAction();
        
        protected virtual void OnDestroy()
        {
            OnAnyClickableRemoveRequest.Call(this);
        }

        private IEnumerator ActionPerformerRoutine()
        {
            yield return new WaitForSeconds(DelayTimeInSeconds);
            PerformAction();
            OnAnyPCLoading.Call(this, new OnAnyPCLoadingEventArgs(false));
        }
    }
}
