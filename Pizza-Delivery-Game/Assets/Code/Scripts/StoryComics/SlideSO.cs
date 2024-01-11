using System.Collections.Generic;
using UnityEngine;

namespace StoryComics 
{
	[CreateAssetMenu(fileName = "Slide_", menuName = "Scriptable Objects/StoryComics/Slide")]
	public class SlideSO : ScriptableObject
	{
		public List<SlideItem> SlideItemList;
		[Range(0f, 2f)] public float TimeToTransitionToAnotherTime;
	}
}

