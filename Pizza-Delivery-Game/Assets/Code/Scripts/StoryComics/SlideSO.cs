using System.Collections.Generic;
using UnityEngine;

namespace StoryComics 
{
	[CreateAssetMenu(fileName = "Slide_", menuName = "Scriptable Objects/StoryComics/Slide")]
	public class SlideSO : ScriptableObject
	{
		[SerializeField] private List<SlideItem> _slideItemList;
		[SerializeField, Range(0f, 2f)] private float _timeToTransitionToAnotherSlide;
		
		public IReadOnlyList<SlideItem> SlideItemList => _slideItemList;
		public float TimeToTransitionToAnotherSlide => _timeToTransitionToAnotherSlide;
	}
}

