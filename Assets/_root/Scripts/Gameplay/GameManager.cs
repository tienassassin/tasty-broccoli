using System;
using System.Collections.Generic;
using System.IO;
using CardMatch.Core;
using CardMatch.Editor;
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

        [ContextMenu("save")]
        public void Save() {
            FileManager.Save(_data, Constants.FilePaths.GAME_DATA_PATH);
        }

        [ContextMenu("load")]
        public void Load() {
            _data = FileManager.Load<GameData>(Constants.FilePaths.GAME_DATA_PATH);
        }

        public void SelectCard(CardCell card) {
            if (_previousCard == null) {
                _previousCard = card;
                return;
            }

            if (_previousCard.ID() == card.ID()) {
                _previousCard.OnMatched();
                card.OnMatched();
                _scoreManager.Match();
            } else {
                _previousCard.HideCard();
                card.HideCard();
                _scoreManager.Mismatch();
            }

            _previousCard = null;
        }
    }

    [Serializable]
    public class GameData {
        public int a;
    }
}