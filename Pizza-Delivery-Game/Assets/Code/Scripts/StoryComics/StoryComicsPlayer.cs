using System.Collections;
using Misc;
using UnityEngine;

namespace StoryComics
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class StoryComicsPlayer : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Transform _container;
        [SerializeField] private StoryComicsSO _story;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(PlaySlidesRoutine());
        }

        private IEnumerator PlaySlidesRoutine()
        {
            while (_story.SlideList.Count > 0)
            {
                SlideSO currentSlide = _story.SlideList[0];
                _story.SlideList.RemoveAt(0);

                while (currentSlide.SlideItemList.Count > 0)
                {
                    SlideItem currentSlideItem = currentSlide.SlideItemList[0];
                    currentSlide.SlideItemList.RemoveAt(0);
                    
                    // Don't use Game Resources in the future
                    StoryComicsText storyComicsText = Instantiate(GameResources.Retrieve.StoryComicsTextPrefab, _container);
                    storyComicsText.SetText(currentSlideItem.Text);

                    CanvasGroup storyComicsCanvasGroup = storyComicsText.GetCanvasGroup();

                    float timer = 0f;

                    while (timer < currentSlide.TimeToTransitionToAnotherTime)
                    {
                        timer += Time.deltaTime;
                        float t = timer / Time.deltaTime;

                        storyComicsCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

                        yield return null;
                    }
                }
            }
        }
    }
}
