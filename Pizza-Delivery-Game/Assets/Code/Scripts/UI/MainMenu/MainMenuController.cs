using System;
using UnityEngine;

namespace UI.MainMenu
{
    [DisallowMultipleComponent]
    public class MainMenuController : MonoBehaviour
    {
        public static Action OnAnyTriedNewGame;
        public static Action OnAnyOptionsOpen;
        public static Action OnAnyCreditsOpen;
        public static Action OnAnyTryQuit;

        
        private void OnEnable()
        {
            MainMenuScreen.OnAnyNewGameButtonClicked += MainMenuScreen_OnAnyNewGameButtonClicked;
            MainMenuScreen.OnAnyOptionsButtonClicked += MainMenuScreen_OnAnyOptionsButtonClicked;
            MainMenuScreen.OnAnyCreditsButtonClicked += MainMenuScreen_OnAnyCreditsButtonClicked;
            MainMenuScreen.OnAnyQuitButtonClicked += MainMenuScreen_OnAnyQuitButtonClicked;
        }

        private void OnDisable()
        {
            MainMenuScreen.OnAnyNewGameButtonClicked -= MainMenuScreen_OnAnyNewGameButtonClicked;
            MainMenuScreen.OnAnyOptionsButtonClicked -= MainMenuScreen_OnAnyOptionsButtonClicked;
            MainMenuScreen.OnAnyCreditsButtonClicked -= MainMenuScreen_OnAnyCreditsButtonClicked;
            MainMenuScreen.OnAnyQuitButtonClicked -= MainMenuScreen_OnAnyQuitButtonClicked;
        }

        private void MainMenuScreen_OnAnyNewGameButtonClicked()
        {
            OnAnyTriedNewGame?.Invoke();
        }
        
        private void MainMenuScreen_OnAnyOptionsButtonClicked()
        {
            OnAnyOptionsOpen?.Invoke();
        }
        
        private void MainMenuScreen_OnAnyCreditsButtonClicked()
        {
            OnAnyCreditsOpen?.Invoke();
        }
        
        private void MainMenuScreen_OnAnyQuitButtonClicked()
        {
            OnAnyTryQuit?.Invoke();
        }
    }
}
