using System.Collections.Generic;
using UnityEngine;

namespace StoryComics
{
    [CreateAssetMenu(fileName = "StoryComics_", menuName = "Scriptable Objects/StoryComics/Story")]
    public class StoryComicsSO : ScriptableObject
    {
        public List<SlideSO> SlideList;
    }
}
