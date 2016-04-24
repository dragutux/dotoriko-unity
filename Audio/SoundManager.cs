using DotOriko.Core;
using System.Collections.Generic;
using UnityEngine;

namespace DotOriko.Audio {
    public enum SoundEffect {
        Click,
        CoinCollect,
        Explosion,
        EmptyAmmo,
        Woosh,
        Sword
    }

    public enum MusicTheme {
        Game,
        Menu
    }

    public sealed class SoundManager : DotOrikoSingleton<SoundManager> {

        private static Dictionary<SoundEffect, string> _typeToUrl;

        static SoundManager() {
            _typeToUrl = new Dictionary<SoundEffect, string>();
            _typeToUrl.Add(SoundEffect.Click, "Sounds/FX/Click");
            _typeToUrl.Add(SoundEffect.CoinCollect, "Sounds/FX/coin");
            _typeToUrl.Add(SoundEffect.EmptyAmmo, "Sounds/FX/Click");
            _typeToUrl.Add(SoundEffect.Explosion, "Sounds/FX/explosion");
            _typeToUrl.Add(SoundEffect.Woosh, "Sounds/FX/woosh_2");
            _typeToUrl.Add(SoundEffect.Sword, "Sounds/FX/slice");

            //_musicThemeToUrl = new Dictionary<MusicTheme, string>();
            //_musicThemeToUrl.Add(MusicTheme.Game, "Sounds/Music/music_main");

            //Instance.PlayMusic(MusicTheme.Game);

        }

        private AudioSource _musicAudioSource = null;

        private readonly List<AudioSource> _effectSoundPool;
        private Dictionary<string, AudioClip> cachedClips;

		private bool _backgroundMusicEnabled = true;
        private bool _soundEffectEnabled = true;

        public bool IsBackgroundMusicEnabled {
            get {
                return _backgroundMusicEnabled;
            }
            set {
                _backgroundMusicEnabled = value;

                if (_backgroundMusicEnabled) {
                    //PlayMusic(MusicTheme.Game);
					_musicAudioSource.mute = false;
                } else {
                    StopMusic();
                }
            }
        }

        public bool IsSoundEffectEnabled {
            get {
                return _soundEffectEnabled;
            }
            set {
                _soundEffectEnabled = value;
            }
        }

        private AudioClipPlayer PlayerPrefab {
            get {
                if (this.playerPrefab == null) {
                    var pref = Resources.Load("Prefabs/Audio/AudioClipPlayer") as GameObject;
                    this.playerPrefab = pref.GetComponent<AudioClipPlayer>();
                }
                return this.playerPrefab;
            }
        }

        private AudioClipPlayer playerPrefab;

        public SoundManager() {
            this._effectSoundPool = new List<AudioSource>();
            this.cachedClips = new Dictionary<string, AudioClip>();
        }

        protected override void OnInitialize() {
            _musicAudioSource = gameObject.AddComponent<AudioSource>();
            _musicAudioSource.loop = true;
            _musicAudioSource.playOnAwake = false;
			_musicAudioSource.Stop ();

            /*for (int i = 0; i < SFX_POOL_SIZE; i++) {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();

                audioSource.loop = false;

                _effectSoundPool.Add(audioSource);
            }*/
        }

        public void StopMusic() {
            if (_musicAudioSource.clip != null) {
				_musicAudioSource.mute = true;


                //Resources.UnloadAsset(_musicAudioSource.clip);
            }
        }

        public void PlayMusic(MusicTheme theme) {
            if (IsBackgroundMusicEnabled) {
                if (_musicAudioSource.clip == null) {
                    //_musicAudioSource.clip = Resources.Load(_musicThemeToUrl[theme]) as AudioClip;
                }
                //Resources.UnloadAsset(_musicAudioSource.clip);

                _musicAudioSource.Play();
            }
        }

        public void PlayEffect(SoundEffect effect, float pitch = 1, float delay = 0, bool loop = false, float volume = 1) {
            if (IsSoundEffectEnabled) {
                var clip = this.GetSound(_typeToUrl[effect]);
                this.PlayAudioClip(clip, pitch, delay, loop, volume);
            }
        }

        /*public void PlayEffect(WeaponType effect) {
            if (IsSoundEffectEnabled) {
                var clip = this.GetSound(_weaponTypeToUrl[effect]);
                this.PlayAudioClip(clip, Random.Range(0.7f, 1.3f), 0, false, 0.5f);
            }
        }*/

        private void PlayAudioClip(AudioClip clip, float pitch, float delay, bool loop, float volume) {
            var player = Instantiate(this.PlayerPrefab) as AudioClipPlayer;
            player.SetClip(clip, pitch, delay, loop, volume);
        }

        private AudioClip GetSound(string link) {
            if (this.cachedClips.ContainsKey(link)) {
                return this.cachedClips[link];
            } else {
                var clip = Resources.Load(link) as AudioClip;
                this.cachedClips.Add(link, clip);
                return clip;
            }
        }


#if UNITY_EDITOR
        private void OnGUI() {

            if (GUI.Button(new Rect(0, 300, 100, 30), "Turn Sound " + (IsBackgroundMusicEnabled ? "Off" : "On"))) {
                IsBackgroundMusicEnabled = !IsBackgroundMusicEnabled;
            }

            if (GUI.Button(new Rect(0, 330, 100, 30), "Turn SFX " + (IsSoundEffectEnabled ? "Off" : "On"))) {
                IsSoundEffectEnabled = !IsSoundEffectEnabled;
            }
        }
#endif
    }
}
