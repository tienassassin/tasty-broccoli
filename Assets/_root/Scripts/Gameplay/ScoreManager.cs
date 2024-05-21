using System;
using CardMatch.Core;
using CardMatch.Editor;
using CardMatch.Sound;
using UnityEngine;
using Logger = CardMatch.Utils.Logger;

namespace CardMatch.Gameplay {
    public class ScoreManager : MonoBehaviour {
        [SerializeField, ReadOnly] private int _currentScore;
        [SerializeField, ReadOnly] private int _currentCombo;
        [SerializeField, ReadOnly] private int _currentComboLife;
        [SerializeField, ReadOnly] private int _maxCombo;
        [SerializeField, ReadOnly] private int _moveCount;
        [SerializeField, ReadOnly] private int _matchesLeft;
        
        //   configs
        private const int COMBO_LIFE_LIMIT = 3;

        public void Initialize(int numberOfMatches) {
            _matchesLeft = numberOfMatches;
            UpdateScore();
        }

        [Button]
        public void Match() {
            _moveCount++;
            _currentCombo++;
            _maxCombo = Mathf.Max(_maxCombo, _currentCombo);
            _currentScore += _currentCombo;
            _currentComboLife = COMBO_LIFE_LIMIT;
            _matchesLeft--;
            if (_matchesLeft == 0) {
                OnLevelCompleted();
            }
            
            UpdateScore();
        }

        [Button]
        public void Mismatch() {
            _moveCount++;
            _currentComboLife = Mathf.Max(_currentComboLife - 1, 0);
            if (_currentComboLife == 0) {
                _currentCombo = 0;
            }
            
            UpdateScore();
        }

        private void UpdateScore() {
            MessageDispatcher<MessageID.ScoreUpdatedEventHandler>.Handle()?.Invoke(_currentScore, _currentCombo, _currentComboLife, _moveCount);
        }
        
        private void OnLevelCompleted() {
            Logger.Log("Level Completed");
            SfxManager.Instance().PlaySfx(SfxID.GAME_OVER);
        }
    }
}