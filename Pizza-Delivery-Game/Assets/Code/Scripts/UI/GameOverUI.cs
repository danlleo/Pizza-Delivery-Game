using System;
using Common;
using DG.Tweening;
using Misc.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Scene = Enums.Scenes.Scene;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class GameOverUI : MonoBehaviour
    {
        [Header("External references")]    
        [SerializeField, ChildrenOnly] private GameObject _gameOverUI;
        [SerializeField, ChildrenOnly] private CanvasGroup _targetFadeGroup;
        [SerializeField] private AudioClip _buttonPressClip;
        
        [Header("Settings")]
        [SerializeField] private float _timeToFadeInSeconds = 1f;

        private Crossfade.Crossfade _crossfade;
        private AudioSource _audioSource;

        private bool _canInteract;

        [Inject]
        private void Construct(Crossfade.Crossfade crossfade)
        {
            _crossfade = crossfade;
        }
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _gameOverUI.SetActive(false);
        }

        private void Update()
        {
            if (!_canInteract) return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                _audioSource.clip = _buttonPressClip;
                _audioSource.Play();
                
                _crossfade.FadeIn(
                    () =>
                    {
                        _canInteract = false;
                        Destroy(this);
                    },
                    () =>
                    {
                        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                        SceneManager.LoadScene(currentSceneIndex);
                    }, 1f
                );
                
                return;
            }

            if (!Input.GetKeyDown(KeyCode.Q)) return;
            
            _audioSource.clip = _buttonPressClip;
            _audioSource.Play();
                
            _crossfade.FadeIn(
                () =>
                {
                    _canInteract = false;
                    Destroy(this);
                },
                () =>
                {
                    Loader.Load(Scene.MainMenuScene);
                }, 1f
            );
        }

        private void OnEnable()
        {
            Common.OnAnyGameOver.Event += OnAnyGameOver;
        }

        private void OnDisable()
        {
            Common.OnAnyGameOver.Event -= OnAnyGameOver;
        }

        private void FadeIn()
        {
            _targetFadeGroup.DOFade(1f, _timeToFadeInSeconds);
        }
        
        private void OnAnyGameOver(object sender, EventArgs e)
        {
            _canInteract = true;
            _targetFadeGroup.alpha = 0f;
            _gameOverUI.SetActive(true);
            FadeIn();
        }
    }
}
