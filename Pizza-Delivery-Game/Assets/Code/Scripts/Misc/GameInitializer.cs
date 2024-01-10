using ServiceLocator;
using UI.Crossfade;
using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameInitializer : MonoBehaviour, IServiceRegistrar
    {
        private Crossfade _crossfade;
        
        private void Awake()
        {
            _crossfade = Instantiate(GameResources.Retrieve.CrossfadePrefab);
            
            this.RegisterCrossfadeService(_crossfade);
            this.RegisterCursorLockStateService(new CursorLockState.CursorLockState(true));
        }
    }
}
