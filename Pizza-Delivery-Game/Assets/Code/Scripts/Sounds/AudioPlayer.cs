using UnityEngine;
using Misc;

namespace Sounds
{
    public abstract class AudioPlayer : Singleton<AudioPlayer>
    {
        public void PlaySound(AudioSource audioSource, AudioClip audioClip, float volume = 1f)
        {
            audioSource.PlayOneShot(audioClip, volume);
        }

        public void PlaySound(AudioSource audioSource, AudioClip[] audioClips, float volume = 1f)
        {
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
