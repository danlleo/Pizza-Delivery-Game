using System.Collections.Generic;
using UnityEngine;

namespace StoryComics
{
    [CreateAssetMenu(fileName = "StoryComics_", menuName = "Scriptable Objects/StoryComics/Story")]
    public class StoryComicsSO : ScriptableObject
    {
        [SerializeField] private List<SlideSO> _slideList;
        
        public IReadOnlyList<SlideSO> SlideList => _slideList;
    }
}
