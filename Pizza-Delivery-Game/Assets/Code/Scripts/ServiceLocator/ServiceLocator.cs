using UI.Crossfade;

namespace ServiceLocator
{
    public class ServiceLocator
    {
        private static ICrossfadeService s_crossfadeService;

        public static void RegisterCrossfadeService(ICrossfadeService crossfadeService)
            => s_crossfadeService = crossfadeService;

        public static ICrossfadeService GetCrossfadeService()
            => s_crossfadeService;
    }
}
