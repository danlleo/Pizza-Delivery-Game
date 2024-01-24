using System.Collections;
using Enums.Scenes;
using Misc.Loader;
using UnityEngine;

namespace StoryComics
{
    [RequireComponent(typeof(OnStoryFinished))]
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class StoryComicsPlayer : MonoBehaviour
    {
        [HideInInspector] public OnStoryFinished OnStoryFinished;
        
        [Header("External references")]
        [SerializeField] private Scene _targetScene;

        [Space(5)] 
        [SerializeField] private Transform _container;
        [SerializeField] private CanvasGroup _containerCanvasGroup;
        [SerializeField] private StoryComicsText _storyComicsTextPrefab;
        [SerializeField] private StoryComicsSO _story;

        [Header("Settings")] 
        [SerializeField] private float _containerCanvasGroupAlphaTransitionTimeInSeconds = 2f;
        [SerializeField] private float _timeToDisplayTextInSeconds = 1f;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            OnStoryFinished = GetComponent<OnStoryFinished>();
        }

        private void Start()
        {
            StartCoroutine(PlayComicsRoutine());
        }

        private IEnumerator PlayComicsRoutine()
        {
            foreach (SlideSO slide in _story.SlideList)
            {
                foreach (SlideItem slideItem in slide.SlideItemList)
                {
                    ResetContainerCanvasGroupAlpha();
                    
                    StoryComicsText storyComicsText = InstantiateComicsText(slideItem.Text);

                    float displayTextTimer = 0f;
                    CanvasGroup canvasGroup = storyComicsText.GetCanvasGroup();
                    
                    while (displayTextTimer <= _timeToDisplayTextInSeconds)
                    {
                        displayTextTimer += Time.deltaTime;
                        float t = displayTextTimer / _timeToDisplayTextInSeconds;

                        canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
                        
                        yield return null;
                    }
                    
                    if (slideItem.Clip != null)
                    {
                        _audioSource.clip = slideItem.Clip;
                        _audioSource.Play();
                    }
                    
                    yield return new WaitForSeconds(slideItem.TimeToDisplayInSeconds);
                }

                yield return new WaitForSeconds(slide.TimeToTransitionToAnotherSlide);

                float fadeOutTimer = 0f;

                while (fadeOutTimer <= _containerCanvasGroupAlphaTransitionTimeInSeconds)
                {
                    fadeOutTimer += Time.deltaTime;
                    float t = fadeOutTimer / _containerCanvasGroupAlphaTransitionTimeInSeconds;
                    
                    _containerCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
                    
                    yield return null;
                }
                
                ClearContainer();
            }
            
            OnStoryFinished.Call(this);
            Invoke(nameof(LoadTargetScene), 3f);
        }

        private void LoadTargetScene()
        {
            Loader.Load(_targetScene);
        }
        
        private void ResetContainerCanvasGroupAlpha()
            => _containerCanvasGroup.alpha = 1f;
        
        private void ClearContainer()
        {
            foreach (Transform child in _container)
                Destroy(child.gameObject);
        }

        private StoryComicsText InstantiateComicsText(string text)
        {
            StoryComicsText storyComicsText = Instantiate(_storyComicsTextPrefab, _container);
            storyComicsText.SetText(text);

            return storyComicsText;
        }
    }
}
