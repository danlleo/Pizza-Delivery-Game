﻿using System;
using Enums.Scenes;
using Interfaces;
using Misc;
using Misc.Loader;
using UI;
using UnityEngine;

namespace Environment.LaboratoryEntry
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("External References")]
        [SerializeField] private Scientist.Scientist _scientist;
        
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.enabled = false;
        }

        private void OnEnable()
        {
            _scientist.OpenedDoorEvent.Event += OpenedDoor_Event;
        }

        private void OnDisable()
        {
            _scientist.OpenedDoorEvent.Event -= OpenedDoor_Event;
        }

        public void Interact()
        {
            Crossfade.Instance.FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.FirstLaboratoryLevelScene));
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Door";
        }
        
        private void OpenedDoor_Event(object sender, EventArgs e)
        {
            _boxCollider.enabled = true;
        }
    }
}