using UI.Crossfade;

namespace ServiceLocator
{
    public static class ServiceLocator
    {
        private static ICrossfadeService s_crossfadeService;

        public static void RegisterCrossfadeService(this IServiceRegistrar _, ICrossfadeService crossfadeService)
            => s_crossfadeService = crossfadeService;

        public static ICrossfadeService GetCrossfadeService()
            => s_crossfadeService;
    }
}
