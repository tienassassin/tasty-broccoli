using System;
using CardMatch.Core;
using CardMatch.Editor;
using UnityEngine;
using Logger = CardMatch.Utils.Logger;

namespace CardMatch.Gameplay {
    public class ScoreManager : MonoBehaviour {
        [SerializeField, ReadOnly] private int _currentScore;
        [SerializeField, ReadOnly] private int _currentCombo;
        [SerializeField, ReadOnly] private int _currentComboLife;
        
        //   configs
        private const int COMBO_LIFE_LIMIT = 3;

        private void Start() {
            UpdateScore();
        }

        [Button]
        public void Match() {
            _currentCombo++;
            _currentScore += _currentCombo;
            _currentComboLife = COMBO_LIFE_LIMIT;
            
            UpdateScore();
        }

        [Button]
        public void Mismatch() {
            _currentComboLife = Mathf.Max(_currentComboLife - 1, 0);
            if (_currentComboLife == 0) {
                _currentCombo = 0;
            }
            
            UpdateScore();
        }

        public void UpdateScore() {
            MessageDispatcher<MessageID.OnScoreUpdated>.Handle()?.Invoke(_currentScore, _currentCombo, _currentComboLife);
        }
    }
}