using UnityEngine;

namespace Sounds
{
    [DisallowMultipleComponent]
    public class AudioPlayer : MonoBehaviour
    {
        private const float DEFAULT_PITCH_VALUE = 1f;
        
        public void PlaySound(AudioSource audioSource, AudioClip audioClip, float volume = 1f)
        {
            audioSource.pitch = DEFAULT_PITCH_VALUE;
            audioSource.PlayOneShot(audioClip, volume);
        }

        public void PlaySound(AudioSource audioSource, AudioClip[] audioClips, float volume = 1f)
        {
            audioSource.pitch = DEFAULT_PITCH_VALUE;
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)], volume);
        }

        public void PlaySoundWithRandomPitch(AudioSource audioSource, AudioClip audioClip, float minPitch,
            float maxPitch, float volume = 1f)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(audioClip, volume);
        }
        
        public void PlaySoundWithRandomPitch(AudioSource audioSource, AudioClip[] audioClips, float minPitch,
            float maxPitch, float volume = 1f)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)], volume);
        }
        
        public void PlaySoundAtPoint(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
        
        public void PlaySoundAtPoint(AudioClip[] audioClips, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length - 1)], position, volume);
        }
    }
}
