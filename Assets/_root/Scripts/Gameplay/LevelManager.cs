using System;
using System.Linq;
using CardMatch.Core;
using CardMatch.Data;
using CardMatch.Editor;
using CardMatch.Utils;
using UnityEngine;
using Logger = CardMatch.Utils.Logger;

namespace CardMatch.Gameplay {
    public class LevelManager : MonoBehaviour {
        [SerializeField] private LevelSO[] _levels;
        [SerializeField, ReadOnly] private int _currentLevel;
        [SerializeField, ReadOnly] private int[] _cards;
        
        //   configs
        private const float LEAKING_DURATION = 2f;

        public void Initialize(GameData gameData, out int numberOfMatches) {
            if (gameData == null) {
                //   no save game found, start from level 0
                _currentLevel = 0;
                _cards = _levels[_currentLevel].GetShuffledCards();
            } else {
                //   get cards from last session
                _currentLevel = gameData.CurrentLevel;
                _cards = gameData.Cards;
            }

            numberOfMatches = _cards.Count(x => x > 0) / 2;
            MessageDispatcher<MessageID.CardsLoadedEventHandler>.Handle()?.Invoke(_cards, LEAKING_DURATION);
            MessageDispatcher<MessageID.LevelUpdatedEventHandler>.Handle()?.Invoke(_currentLevel);
        }

        public int CurrentLevel() {
            return _currentLevel;
        }
        
        public int[] Cards() {
            return _cards;
        }
        
        public void MarkCardAsMatched(int index, int card) {
            _cards[index] = -1 * card;
        }

        public void LevelUp(out int numberOfMatches) {
            _currentLevel++;
            if (_currentLevel >= _levels.Length) {
                Logger.Log("All levels completed!!! Resetting to level 0...");
                _currentLevel = 0;
            }
            
            _cards = _levels[_currentLevel].GetShuffledCards();
            numberOfMatches = _cards.Length / 2;
            MessageDispatcher<MessageID.CardsLoadedEventHandler>.Handle()?.Invoke(_cards, LEAKING_DURATION);
            MessageDispatcher<MessageID.LevelUpdatedEventHandler>.Handle()?.Invoke(_currentLevel);
        }
    }
}