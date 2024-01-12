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
        }

        private void PlayStory()
        {
            
        }
    }
}
