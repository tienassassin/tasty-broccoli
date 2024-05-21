using System;
using CardMatch.Core;
using CardMatch.Data;
using CardMatch.Editor;
using UnityEngine;
using Logger = CardMatch.Utils.Logger;

namespace CardMatch.Gameplay {
    public class LevelManager : MonoBehaviour {
        [SerializeField] private LevelSO[] _levels;
        [SerializeField, ReadOnly] private int[] _cards;
        
        //   configs
        private const float LEAKING_DURATION = 2f;

        public void Initialize(int level, out int numberOfMatches) {
            _cards = _levels[level].GetShuffledCards();
            if (_cards == null) {
                numberOfMatches = -1;
                return;
            }

            numberOfMatches = _cards.Length / 2;
            MessageDispatcher<MessageID.CardsLoadedEventHandler>.Handle()?.Invoke(_cards, LEAKING_DURATION);
        }
    }
}