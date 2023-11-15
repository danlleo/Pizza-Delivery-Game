using UnityEngine;

namespace Scientist.Outdoor
{
    public static class AnimationParams
    {
        public static readonly int OnStartedTalking = Animator.StringToHash(nameof(OnStartedTalking));
        public static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));
    }
}