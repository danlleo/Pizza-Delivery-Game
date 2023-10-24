using UnityEngine;

namespace Misc
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UI.Crossfade _crossfade;

        private void Start()
        {
            _crossfade.FadeOut(InputAllowance.EnableInput);
        }
    }
}
    