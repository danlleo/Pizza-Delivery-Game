using System;

namespace UI.Crossfade
{
    public interface ICrossfadeService
    {
        void FadeIn(Action onStart = null, Action onComplete = null);
        void FadeOut(Action onStart = null, Action onComplete = null);
    }
}