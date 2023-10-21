using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue_", menuName = "Scriptable Objects/Dialogues/Dialogue")]
    public class DialogueSO : ScriptableObject
    {
        [SerializeField] private TextAsset _dialogueText;
        [SerializeField] private Color _textColor; 
        
        public TextAsset DialogueText => _dialogueText;
        public Color TextColor => _textColor;
    }
}
