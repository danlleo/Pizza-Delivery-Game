using DG.Tweening;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [SelectionBase]
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class LaboratoryWorker : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = true;
        }

        private void OnEnable()
        {
            LaboratoryWorkerCryStaticEvent.OnAnyLaboratoryWorkerCry += OnAnyLaboratoryWorkerCry;
        }

        private void OnDisable()
        {
            LaboratoryWorkerCryStaticEvent.OnAnyLaboratoryWorkerCry -= OnAnyLaboratoryWorkerCry;
        }

        private void OnAnyLaboratoryWorkerCry(object sender, LaboratoryWorkerCryEventArgs e)
        {
            if (e.IsCrying)
            {
                _audioSource.Play();
                return;
            }

            _audioSource.loop = false;
            _audioSource.DOFade(0f, .9f);
        }
    }
}