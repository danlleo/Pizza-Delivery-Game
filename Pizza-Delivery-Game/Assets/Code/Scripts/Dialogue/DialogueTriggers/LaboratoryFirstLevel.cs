using System;
using Environment.LaboratoryFirstLevel;
using EventBus;
using Keypad;
using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class LaboratoryFirstLevel : DialogueTrigger
    {
        [Header("Dialogue items")]
        [SerializeField] private DialogueSO _noKeycardDialogueSO;
        [SerializeField] private DialogueSO _noWrenchDialogueSO;
        [SerializeField] private DialogueSO _fixedPipesDialogueSO;
        [SerializeField] private DialogueSO _passwordUnknownDialogueSO;
        [SerializeField] private DialogueSO _fireExtinguisherDialogueSO;
        [SerializeField] private DialogueSO _noFlashlightDialogueSO;
        
        private EventBinding<FixPipesEvent> _noWrenchEventBinding;
        
        private void OnEnable()
        {
            KeycardStateStaticEvent.OnKeycardStateChanged += OnAnyKeycardStateChanged;
            PasswordUnknownStaticEvent.OnAnyPasswordUnknown += OnAnyPasswordUnknown;
            InteractedWithFireExtinguisherStaticEvent.OnAnyInteractedWithFireExtinguisher += OnAnyInteractedWithFireExtinguisher;
            NoFlashlightStaticEvent.OnAnyNoFlashlight += OnAnyNoFlashlight;

            _noWrenchEventBinding = new EventBinding<FixPipesEvent>(Player_OnPipeFix);
            EventBus<FixPipesEvent>.Register(_noWrenchEventBinding);
        }

        private void OnDisable()
        {
            KeycardStateStaticEvent.OnKeycardStateChanged -= OnAnyKeycardStateChanged;
            PasswordUnknownStaticEvent.OnAnyPasswordUnknown -= OnAnyPasswordUnknown;
            InteractedWithFireExtinguisherStaticEvent.OnAnyInteractedWithFireExtinguisher -= OnAnyInteractedWithFireExtinguisher;
            NoFlashlightStaticEvent.OnAnyNoFlashlight -= OnAnyNoFlashlight;
            
            EventBus<FixPipesEvent>.Deregister(_noWrenchEventBinding);
        }

        private void OnAnyNoFlashlight(object sender, EventArgs e)
        {
            InvokeDialogue(_noFlashlightDialogueSO);
        }
        
        private void OnAnyInteractedWithFireExtinguisher(object sender, EventArgs e)
        {
            InvokeDialogue(_fireExtinguisherDialogueSO);
        }
        
        private void Player_OnPipeFix(FixPipesEvent fixPipesEvent)
        {
            if (!fixPipesEvent.HasFixed)
            {
                InvokeDialogue(_noWrenchDialogueSO);
                return;
            }
            
            InvokeDialogue(_fixedPipesDialogueSO);
        }

        private void OnAnyKeycardStateChanged(object sender, KeycardStateStaticEventArgs e)
        {
            if (e.AccessGranted) return;
            
            InvokeDialogue(_noKeycardDialogueSO);
        }
        
        private void OnAnyPasswordUnknown(object sender, EventArgs e)
        {
            InvokeDialogue(_passwordUnknownDialogueSO);
        }
    }
}
