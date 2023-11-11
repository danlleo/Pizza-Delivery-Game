using UnityEngine;

namespace Dialogue
{
    public abstract class DialogueAction : MonoBehaviour
    {
        public virtual void Invoke()
        {
            DialogueActionStaticEvent.Call(Player.Player.Instance, new DialogueActionStaticEventArgs(this));
        }

        public abstract void Perform();
    }
}