using System;
using UnityEngine;

namespace StoryComics
{
    [DisallowMultipleComponent]
    public class OnStoryFinished : MonoBehaviour
    {
        public event EventHandler Event;

        public void Call(StoryComicsPlayer storyComicsPlayer)
        {
            Event?.Invoke(storyComicsPlayer, EventArgs.Empty);
        }
    }
}