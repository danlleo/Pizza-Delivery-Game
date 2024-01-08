using UI.Crossfade;
using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private Crossfade _crossfade;
        
        private void Start()
        {
            ServiceLocator.ServiceLocator.RegisterCrossfadeService(_crossfade);
        }
    }
}
