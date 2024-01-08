using Enums.Scenes;
using Misc;
using Misc.Loader;
using UnityEngine;

namespace UI.MainMenu
{
    [DisallowMultipleComponent]
    public class MainMenuController : MonoBehaviour
    {
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
            ServiceLocator.ServiceLocator.GetCrossfadeService()
                .FadeIn(InputAllowance.DisableInput, () => Loader.Load(Scene.BedroomScene));
        }
        
        private void MainMenuScreen_OnAnyOptionsButtonClicked()
        {
            
        }
        
        private void MainMenuScreen_OnAnyCreditsButtonClicked()
        {
            
        }
        
        private void MainMenuScreen_OnAnyQuitButtonClicked()
        {
            
        }
    }
}
