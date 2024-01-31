using System;
using System.Collections;
using Environment.LaboratorySecondLevel;
using UnityEngine;
using Utilities;
using Zenject;

namespace Dialogue.DialogueTriggers
{
    public class LaboratorySecondLevelDialogueTrigger : DialogueTrigger
    {
        [Header("Dialogue items")] 
        [SerializeField] private DialogueSO _goalDialogueSO;
        [SerializeField] private DialogueSO _noPizzaBoxDialogueSO;
        [SerializeField] private DialogueSO _notAllowedToPickPizzaDialogueSO;
        [SerializeField] private DialogueSO _securePizzaBoxDialogueSO;
        [SerializeField] private DialogueSO[] _startedChaseDialogueSOArray;
        [SerializeField] private DialogueSO[] _stoppedChaseDialogueSOArray;

        private Monster.Monster _monster;

        private readonly float _timeBeforePlayingGoalDialogueSOInSeconds = 1f;
        
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_timeBeforePlayingGoalDialogueSOInSeconds);
            InvokeDialogue(_goalDialogueSO);
        }

        [Inject]
        private void Construct(Monster.Monster monster)
        {
            _monster = monster;
        }
        
        private void OnEnable()
        {
            PizzaBox.OnAnyPlayerTriedTakePizzaWhileBeenChased += PizzaBox_OnAnyPlayerTriedTakePizzaWhileBeenChased;
            PizzaBox.OnAnyPlayerPickedUpPizzaBox += PizzaBox_OnAnyPlayerPickedUpPizzaBox;
            NoPizzaBoxStaticEvent.OnAnyNoPizzaBox += OnAnyNoPizzaBox;
            _monster.StartedChasingEvent.Event += Monster_OnStartedChase;
            _monster.StoppedChasingEvent.Event += Monster_OnStoppedChaseEvent;
        }

        private void OnDisable()
        {
            PizzaBox.OnAnyPlayerTriedTakePizzaWhileBeenChased -= PizzaBox_OnAnyPlayerTriedTakePizzaWhileBeenChased;
            PizzaBox.OnAnyPlayerPickedUpPizzaBox -= PizzaBox_OnAnyPlayerPickedUpPizzaBox;
            NoPizzaBoxStaticEvent.OnAnyNoPizzaBox -= OnAnyNoPizzaBox;
            _monster.StartedChasingEvent.Event -= Monster_OnStartedChase;
            _monster.StoppedChasingEvent.Event -= Monster_OnStoppedChaseEvent;
        }

        private void OnAnyNoPizzaBox(object sender, EventArgs e)
        {
            InvokeDialogue(_noPizzaBoxDialogueSO);
        }
        
        private void Monster_OnStartedChase(object sender, EventArgs e)
        {
            DialogueSO dialogue = _startedChaseDialogueSOArray.GetRandomItem();
            InvokeDialogue(dialogue);
        }
        
        private void Monster_OnStoppedChaseEvent(object sender, EventArgs e)
        {
            DialogueSO dialogue = _stoppedChaseDialogueSOArray.GetRandomItem();
            InvokeDialogue(dialogue);
        }
        
        private void PizzaBox_OnAnyPlayerTriedTakePizzaWhileBeenChased(object sender, EventArgs e)
        {
            InvokeDialogue(_notAllowedToPickPizzaDialogueSO);
        }
        
        private void PizzaBox_OnAnyPlayerPickedUpPizzaBox(object sender, EventArgs e)
        {
            InvokeDialogue(_securePizzaBoxDialogueSO);
        }
    }
}
