using System;
using System.Collections.Generic;
using CardMatch.Core;
using UnityEngine;

namespace CardMatch.Sound {
    public class SfxManager : Singleton<SfxManager> {
        [SerializeField] private Sfx[] _sfxes;
        [SerializeField] private SfxPlayer _sfxPlayerPrefab;

        private Dictionary<string, AudioClip> _clipDict = new();
        private List<SfxPlayer> _sfxPlayers = new();
        
        protected override void OnAwake() {
            foreach (var sfx in _sfxes) {
                _clipDict.TryAdd(sfx.ID, sfx.AudioClip);
            }
        }

        public void PlaySfx(string sfxId) {
            AudioClip clip = _clipDict[sfxId];
            SfxPlayer player = _sfxPlayers.Find(x => !x.gameObject.activeSelf);
            if (player == null) {
                player = Instantiate(_sfxPlayerPrefab, transform);
                _sfxPlayers.Add(player);
            }

            player.gameObject.SetActive(true);
            player.Play(clip);
        }
    }
    
    public class SfxID {
        public const string CARD_FLIP = "CARD_FLIP";
        public const string CARD_MATCH = "CARD_MATCH";
        public const string CARD_MISMATCH = "CARD_MISMATCH";
        public const string GAME_OVER = "GAME_OVER";
    }

    [Serializable]
    public struct Sfx {
        public string ID;
        public AudioClip AudioClip;
    }
}