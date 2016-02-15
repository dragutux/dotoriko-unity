using DotOriko;
using System.Collections;
using UnityEngine;

namespace DotOriko.Audio {
    public sealed class AudioClipPlayer : DotOrikoComponent {

        [SerializeField]
        private AudioSource _source;

        public void SetClip(AudioClip clip, float pitch, float delay, bool loop, float volume) {
            this._source.clip = clip;
            this._source.pitch = pitch;
            this._source.loop = loop;
            _source.volume = volume;
            this.StartCoroutine(this.PlayClip(delay));

            Destroy(this.gameObject, delay + clip.length);
        }

        private IEnumerator PlayClip(float delay) {
            yield return new WaitForSeconds(delay);
            this._source.Play();
        }

    }
}
