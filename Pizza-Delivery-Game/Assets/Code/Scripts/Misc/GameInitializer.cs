using ServiceLocator;
using UI.Crossfade;
using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameInitializer : MonoBehaviour, IServiceRegistrar
    {
        private Crossfade _crossfade;
        
        public void Initialize()
        {
            _crossfade = Instantiate(GameResources.Retrieve.CrossfadePrefab);
            
            this.RegisterCrossfadeService(_crossfade);
            this.RegisterCursorLockStateService(new CursorLockState.CursorLockState(true));
        }
    }
}
