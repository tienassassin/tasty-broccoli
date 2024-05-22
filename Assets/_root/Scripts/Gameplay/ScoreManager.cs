using System;
using CardMatch.Core;
using CardMatch.Editor;
using CardMatch.Sound;
using UnityEngine;
using Logger = CardMatch.Utils.Logger;

namespace CardMatch.Gameplay {
    public class ScoreManager : MonoBehaviour {
        [SerializeField, ReadOnly] private int _matchesLeft;
        
        private ScoreData _scoreData;
        
        //   configs
        private const int COMBO_LIFE_LIMIT = 5;

        public void Initialize(GameData gameData, int numberOfMatches) {
            if (gameData == null) {
                //   no save game found, start from 0 score
                _scoreData = new ScoreData();
            } else {
                //   get score from last session
                _scoreData = gameData.ScoreData;
            }
            
            _matchesLeft = numberOfMatches;
            UpdateScore();
        }

        public ScoreData ScoreData() {
            return _scoreData;
        }

        public void Match() {
            _scoreData.MoveCount++;
            _scoreData.AddScore();
            _scoreData.CalculateMaxCombo();
            _scoreData.CurrentComboLife = COMBO_LIFE_LIMIT;
            _matchesLeft--;
            if (_matchesLeft == 1) {
                GameManager.Instance().SelectLastTwoCardsAutomatically();
            }else if (_matchesLeft == 0) {
                GameManager.Instance().OnLevelCompleted();
            }
            
            UpdateScore();
        }

        public void Mismatch() {
            _scoreData.MoveCount++;
            _scoreData.LostComboLife();
            
            UpdateScore();
        }

        private void UpdateScore() {
            MessageDispatcher<MessageID.ScoreUpdatedEventHandler>.Handle()?.Invoke(_scoreData, COMBO_LIFE_LIMIT);
        }
    }

    [Serializable]
    public struct ScoreData {
        public int MoveCount;
        public int CurrentScore;
        public int CurrentCombo;
        public int CurrentComboLife;
        public int MaxCombo;

        public void AddScore() {
            CurrentScore += ++CurrentCombo;
        }
        
        public void CalculateMaxCombo() {
            MaxCombo = Mathf.Max(MaxCombo, CurrentCombo);
        }

        public void LostComboLife() {
            CurrentComboLife = Mathf.Max(CurrentComboLife - 1, 0);
            if (CurrentComboLife == 0) {
                CurrentCombo = 0;
            }
        }
    } 
}