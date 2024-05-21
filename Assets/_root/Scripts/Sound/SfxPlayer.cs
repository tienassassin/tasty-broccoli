using System.Collections;
using CardMatch.Core;
using UnityEngine;

namespace CardMatch.Sound {
    public class SfxPlayer : MonoBehaviour {
        [SerializeField] private AudioSource _audioSource;
        
        public void Play(AudioClip clip) {
            if (clip == null) return;

            name = clip.name;
            _audioSource.clip = clip;
            _audioSource.Play();

            StartCoroutine(DoDisable(clip.length));
        }

        IEnumerator DoDisable(float delay) {
            yield return Common.GetWait(delay);
            _audioSource.Stop();
            gameObject.SetActive(false);
        }
    }
}