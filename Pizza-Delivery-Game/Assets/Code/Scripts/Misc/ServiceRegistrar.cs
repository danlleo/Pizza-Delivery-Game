using ServiceLocator;
using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class ServiceRegistrar : MonoBehaviour, IServiceRegistrar
    {
        public void Initialize()
        {
            this.RegisterCrossfadeService(GameResources.Retrieve.CrossfadePrefab);
            this.RegisterCursorLockStateService(new CursorLockState.CursorLockState(true));
        }
    }
}
