using UnityEngine;
using UnityEngine.SceneManagement;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.activeSceneChanged += SceneManager_OnActiveSceneChanged;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= SceneManager_OnActiveSceneChanged;
        }

        private void SceneManager_OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            InputAllowance.EnableInput();
        }

        private void Start()
        {
            InputAllowance.EnableInput();
        }
    }
}
    