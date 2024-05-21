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
        
        [SerializeField, ReadOnly] private CardCell _previousCardCell;
        
        public GameData _data;
        
        
        protected override void OnAwake() {
            
        }

        private void Start() {
            _levelManager.Initialize(4, out int numberOfMatches);
            _scoreManager.Initialize(numberOfMatches);
        }

        [Button]
        public void Save() {
            FileManager.Save(_data, Constants.FilePaths.GAME_DATA_PATH);
        }

        [Button]
        public void Load() {
            _data = FileManager.Load<GameData>(Constants.FilePaths.GAME_DATA_PATH);
        }

        public void SelectCardCell(CardCell cardCell) {
            if (_previousCardCell == null) {
                _previousCardCell = cardCell;
                return;
            }

            if (_previousCardCell.Card() == cardCell.Card()) {
                _previousCardCell.Match();
                cardCell.Match();
                _scoreManager.Match();
                SfxManager.Instance().PlaySfx(SfxID.CARD_MATCH);
            } else {
                _previousCardCell.Hide();
                cardCell.Hide();
                _scoreManager.Mismatch();
                SfxManager.Instance().PlaySfx(SfxID.CARD_MISMATCH);
            }

            _previousCardCell = null;
        }
    }

    [Serializable]
    public class GameData {
        public int a;
    }
}