using UnityEngine;
using Utilities;

namespace Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;
        
        [Header("Music")]
        [SerializeField] private AudioClip _backgroundMusic;
        
        [Header("SFX")]
        [SerializeField] private AudioClip _maskChange;
        [SerializeField] private AudioClip _playerDeath;
        [SerializeField] private AudioClip _levelComplete;
        [SerializeField] private AudioClip _maskPickup;
        [SerializeField] private AudioClip _defaultMask;

        private void Start()
        {
            Debug.Log("AudioManager Start called");
            Debug.Log($"Music source: {_musicSource}, Clip: {_backgroundMusic}");
            PlayMusic(_backgroundMusic);
            Debug.Log($"Music is playing: {_musicSource.isPlaying}");
            Debug.Log($"Music volume: {_musicSource.volume}");
            Debug.Log($"Music mute: {_musicSource.mute}");
        }

        private void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        private void PlaySfx(AudioClip clip)
        {
            _sfxSource.PlayOneShot(clip);
        }
        
        public void PlayMaskChange() => PlaySfx(_maskChange);
        public void PlayPlayerDeath() => PlaySfx(_playerDeath);
        public void PlayLevelComplete() => PlaySfx(_levelComplete);
        public void PlayMaskPickup() => PlaySfx(_maskPickup);
        public void PlayDefaultMask() => PlaySfx(_defaultMask);
    }
}