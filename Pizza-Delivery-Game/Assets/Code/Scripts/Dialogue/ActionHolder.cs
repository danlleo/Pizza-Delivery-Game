using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "DialogueActionHolder_", menuName = "Scriptable Objects/Dialogues/ActionsHolder")]
    public sealed class ActionHolder : ScriptableObject
    {
        public DialogueAction TargetAction;

        public void Invoke()
        {
            DialogueAction dialogueAction = Instantiate(TargetAction);
            SceneManager.MoveGameObjectToScene(dialogueAction.gameObject, SceneManager.GetActiveScene());
        }
    }
}
