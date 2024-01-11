using System;

namespace UI.Crossfade
{
    public interface ICrossfadeService
    {
        public void FadeIn(float duration);
        public void FadeIn(Action onStart, float duration);
        
        public void FadeIn(Action onStart, Action onComplete, float duration);
        
        public void FadeOut(float duration);
        public void FadeOut(Action onStart, float duration);
        public void FadeOut(Action onStart, Action onComplete, float duration);
    }
}
