using System;
using Player.Inventory;
using UnityEngine;
using UnityEngine.UIElements;
using static Utilities.VisualElementCreationTool;

namespace UI.ItemUseConfirmation
{
    [DisallowMultipleComponent]
    public sealed class ItemUseConfirmationScreen : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        private void OnEnable()
        {
            Player.Inventory.OnAnyItemUse.Event += OnAnyItemUse;
        }

        private void OnDisable()
        {
            Player.Inventory.OnAnyItemUse.Event -= OnAnyItemUse;
        }

        private void OnAnyItemUse(object sender, OnAnyItemUseEventArgs e)
        {
            CreatePopupWindow($"ARE YOU SURE YOU WANT TO USE: {e.Item.ItemName}?",
                "Using it will remove it from your inventory.",
                () =>
                {
                    e.ItemReceiver.OnReceive();
                    e.ItemReceiver.CallOnAnyItemFinishedInteraction();
                    DeleteUI();
                }, () =>
                {
                    e.ItemReceiver.OnDecline();
                    e.ItemReceiver.CallOnAnyItemFinishedInteraction();
                    DeleteUI();
                });
        }
        
        private void CreatePopupWindow(string titleMessage, string bodyMessage, Action onConfirm, Action onDecline)
        {
            var popupWindow = Create<PopupWindow>();
            popupWindow.Title = titleMessage;
            popupWindow.Message = bodyMessage;
            popupWindow.OnConfirm = onConfirm;
            popupWindow.OnDecline = onDecline;
            
            _uiDocument.rootVisualElement.Add(popupWindow);
            _uiDocument.rootVisualElement.Q<Button>("cancel-button").Focus();
        }
        
        private void DeleteUI()
            => _uiDocument.rootVisualElement.Clear();
    }
}
