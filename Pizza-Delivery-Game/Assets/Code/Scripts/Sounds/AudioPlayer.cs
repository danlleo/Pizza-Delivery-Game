using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public abstract class AudioPlayer : MonoBehaviour
    {
        private const float DEFAULT_PITCH_VALUE = 1f;

        protected void PlaySound(AudioSource audioSource, AudioClip audioClip, float volume = 1f)
        {
            audioSource.pitch = DEFAULT_PITCH_VALUE;
            audioSource.PlayOneShot(audioClip, volume);
        }

        protected void PlaySound(AudioSource audioSource, AudioClip[] audioClips, float volume = 1f)
        {
            audioSource.pitch = DEFAULT_PITCH_VALUE;
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)], volume);
        }

        protected void PlaySoundLoop(AudioSource audioSource, AudioClip audioClip, float volume = 1f)
        {
            audioSource.loop = true;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        
        protected void PlaySoundLoop(AudioSource audioSource, AudioClip[] audioClips, float volume = 1f)
        {
            audioSource.loop = true;
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length - 1)];
            audioSource.Play();
        }

        protected void StopLoopSound(AudioSource audioSource)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
        
        public static void PlaySoundWithRandomPitch(AudioSource audioSource, AudioClip audioClip, float minPitch,
            float maxPitch, float volume = 1f)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(audioClip, volume);
        }
        
        public static void PlaySoundWithRandomPitch(AudioSource audioSource, AudioClip[] audioClips, float minPitch,
            float maxPitch, float volume = 1f)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)], volume);
        }
        
        protected void PlaySoundAtPoint(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
        
        protected void PlaySoundAtPoint(AudioClip[] audioClips, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length - 1)], position, volume);
        }
    }
}
