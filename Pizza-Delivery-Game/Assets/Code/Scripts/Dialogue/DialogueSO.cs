using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue_", menuName = "Scriptable Objects/Dialogues/Dialogue")]
    public class DialogueSO : ScriptableObject
    {
        [SerializeField] private ConfigurationSO _configurationSO;
        [SerializeField] private TextAsset _dialogueText;

        public ConfigurationSO Configuration => _configurationSO;
        public ActionHolder OnDialogueEnd;
        public TextAsset DialogueText => _dialogueText;
    }
}
