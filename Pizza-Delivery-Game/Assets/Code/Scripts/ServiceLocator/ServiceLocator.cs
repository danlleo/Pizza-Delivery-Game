using Misc.CursorLockState;
using UI.Crossfade;

namespace ServiceLocator
{
    public static class ServiceLocator
    {
        private static ICrossfadeService s_crossfadeService;
        private static ICursorLockService s_cursorLockService;
        
        public static void RegisterCrossfadeService(this IServiceRegistrar _, ICrossfadeService crossfadeService)
            => s_crossfadeService = crossfadeService;

        public static ICrossfadeService GetCrossfadeService()
            => s_crossfadeService;

        public static void RegisterCursorLockStateService(this IServiceRegistrar _,
            ICursorLockService cursorLockService)
            => s_cursorLockService = cursorLockService;

        public static ICursorLockService GetCursorLockService()
            => s_cursorLockService;
    }
}
