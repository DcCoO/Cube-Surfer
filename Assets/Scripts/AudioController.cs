using UnityEngine;

namespace CubeSurferClone
{
    public class AudioController : SingletonMonoBehaviour<AudioController>
    {
        [SerializeField] AudioSource source;

        [Header("Clips")]
        [SerializeField] AudioClip diamondSound;
        [SerializeField] AudioClip collectibleSound;
        [SerializeField] AudioClip hitSound;
        [SerializeField] AudioClip winSound;
        [SerializeField] AudioClip lavaSound;

        public void PlayDiamond() => PlayTrack(diamondSound);
        public void PlayCollectible() => PlayTrack(collectibleSound);
        public void PlayHit() => PlayTrack(hitSound);
        public void PlayWin() => PlayTrack(winSound);
        public void PlayLava() => PlayTrack(lavaSound);

        void PlayTrack(AudioClip clip) => source.PlayOneShot(clip);
    }
}
