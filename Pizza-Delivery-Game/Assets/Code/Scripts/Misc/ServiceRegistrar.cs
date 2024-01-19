using ServiceLocator;
using UnityEngine;

namespace Misc
{
    public class ServiceRegistrar : MonoBehaviour, IServiceRegistrar
    {
        public void Initialize()
        {
            this.RegisterCrossfadeService(Instantiate(GameResources.Retrieve.CrossfadePrefab));
            this.RegisterCursorLockStateService(new CursorLockState.CursorLockState(true));
        }
    }
}
