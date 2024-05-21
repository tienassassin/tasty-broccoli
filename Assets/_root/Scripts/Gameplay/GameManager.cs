using System;
using System.Collections.Generic;
using System.IO;
using CardMatch.Core;
using CardMatch.Editor;
using CardMatch.Sound;
using CardMatch.UI;
using CardMatch.Utils;
using UnityEngine;

namespace CardMatch.Gameplay {
    public class GameManager : Singleton<GameManager> {
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private LevelManager _levelManager;
        
        [SerializeField, ReadOnly] private CardCell _previousCard;
        
        public GameData _data;
        
        
        protected override void OnAwake() {
            
        }

        private void Start() {
            _levelManager.Initialize(0);
            _scoreManager.Initialize(_levelManager.GetNumberOfMatches());
        }

        [Button]
        public void Save() {
            FileManager.Save(_data, Constants.FilePaths.GAME_DATA_PATH);
        }

        [Button]
        public void Load() {
            _data = FileManager.Load<GameData>(Constants.FilePaths.GAME_DATA_PATH);
        }

        public void SelectCard(CardCell card) {
            if (_previousCard == null) {
                _previousCard = card;
                return;
            }

            if (_previousCard.ID() == card.ID()) {
                _previousCard.Match();
                card.Match();
                _scoreManager.Match();
                SfxManager.Instance().PlaySfx(SfxID.CARD_MATCH);
            } else {
                _previousCard.Hide();
                card.Hide();
                _scoreManager.Mismatch();
                SfxManager.Instance().PlaySfx(SfxID.CARD_MISMATCH);
            }

            _previousCard = null;
        }
    }

    [Serializable]
    public class GameData {
        public int a;
    }
}