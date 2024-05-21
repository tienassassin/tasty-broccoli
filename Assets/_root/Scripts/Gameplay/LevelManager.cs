using System;
using CardMatch.Core;
using CardMatch.Data;
using CardMatch.Editor;
using UnityEngine;
using Logger = CardMatch.Utils.Logger;

namespace CardMatch.Gameplay {
    public class LevelManager : MonoBehaviour {
        [SerializeField] private LevelSO[] _levels;
        [SerializeField, ReadOnly] private Card[] _cards;
        
        //   configs
        private const float LEAKING_DURATION = 2f;

        public void Initialize(int level) {
            GenerateCards(level);
        }
        
        public int GetNumberOfMatches() {
            return _cards.Length / 2;
        }

        private void GenerateCards(int level) {
            _cards = _levels[level].GetShuffledCardFaces();
            MessageDispatcher<MessageID.CardsLoadedEventHandler>.Handle()?.Invoke(_cards, LEAKING_DURATION);
        }
    }
}