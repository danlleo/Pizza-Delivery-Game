using UnityEngine;

namespace Monster
{
    public static class AnimationParams
    {
        public static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));
        public static readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));
    }
}
