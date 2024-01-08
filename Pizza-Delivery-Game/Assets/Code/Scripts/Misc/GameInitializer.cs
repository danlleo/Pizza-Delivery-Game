using ServiceLocator;
using UI.Crossfade;
using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameInitializer : MonoBehaviour, IServiceRegistrar
    {
        [Header("External references")]
        [SerializeField] private Crossfade _crossfade;
        
        private void Awake()
        {
            this.RegisterCrossfadeService(_crossfade);
        }
    }
}
