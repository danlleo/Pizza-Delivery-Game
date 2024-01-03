using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "DialogueActionHolder_", menuName = "Scriptable Objects/Dialogues/ActionsHolder")]
    public sealed class ActionHolder : ScriptableObject
    {
        public DialogueAction TargetAction;

        public void Invoke()
        {
            Instantiate(TargetAction);
        }
    }
}
