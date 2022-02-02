using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip[] audioClips;

        /// <summary>
        /// Plays the move item success audio clip
        /// </summary>
        public void PlayMoveSuccess()
        {
            if (!audioSource) return;

            if (audioClips.Length >= 3 && audioClips[2])
                audioSource.PlayOneShot(audioClips[2]);
        }

        /// <summary>
        /// Plays the start move item audio clip
        /// </summary>
        public void PlayMoveStart()
        {
            if (!audioSource) return;

            if (audioClips.Length >= 2 && audioClips[1])
                audioSource.PlayOneShot(audioClips[1]);
        }

        /// <summary>
        /// Plays the move failure audio clip
        /// </summary>
        public void PlayMoveFailure()
        {
            if (!audioSource) return;

            if (audioClips.Length >= 1 && audioClips[0])
                audioSource.PlayOneShot(audioClips[0]);
        }
    }
}