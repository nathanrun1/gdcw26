using UnityEngine;
using Utilities;

namespace Managers
{
    public class AudioManager : PersistentSingleton<AudioManager>
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

        protected override void Awake()
        {
            base.Awake();
            if (_otherInstanceExists) return;
            PlayMusic(_backgroundMusic);
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