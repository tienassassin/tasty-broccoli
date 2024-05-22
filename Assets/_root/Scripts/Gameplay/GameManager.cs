using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using CardMatch.Core;
using CardMatch.Editor;
using CardMatch.Sound;
using CardMatch.UI;
using CardMatch.Utils;
using UnityEngine;
using Logger = CardMatch.Utils.Logger;

namespace CardMatch.Gameplay {
    public class GameManager : Singleton<GameManager> {
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private DataManager _dataManager;

        [SerializeField, ReadOnly] private CardCell _previousCardCell;
        
        //   configs
        private const float GAME_OVER_DELAY = 2f;

        protected override void OnAwake() { }

        private void Start() {
            _dataManager.Initialize(out GameData gameData);
            _levelManager.Initialize(gameData, out int numberOfMatches);
            _scoreManager.Initialize(gameData, numberOfMatches);
        }

        private void OnDestroy() {
            GameData gameData = new GameData {
                CurrentLevel = _levelManager.CurrentLevel(),
                Cards = _levelManager.Cards(),
                ScoreData = _scoreManager.ScoreData()
            };
            _dataManager.Save(gameData);
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
                _levelManager.MarkCardAsMatched(_previousCardCell.Index(), _previousCardCell.Card());
                _levelManager.MarkCardAsMatched(cardCell.Index(), cardCell.Card());
                SfxManager.Instance().PlaySfx(SfxID.CARD_MATCH);
            } else {
                _previousCardCell.Hide();
                cardCell.Hide();
                _scoreManager.Mismatch();
                SfxManager.Instance().PlaySfx(SfxID.CARD_MISMATCH);
            }

            _previousCardCell = null;
        }

        public void SelectLastTwoCardsAutomatically() {
            MessageDispatcher<MessageID.SelectLastCardsAutomaticallyEventHandler>.Handle()?.Invoke();
        }

        public void OnLevelCompleted() {
            Logger.Log("Level Completed");
            SfxManager.Instance().PlaySfx(SfxID.GAME_OVER);
            StartCoroutine(DoMoveToNextLevel());
        }

        IEnumerator DoMoveToNextLevel() {
            yield return Common.GetWait(GAME_OVER_DELAY);
            _levelManager.LevelUp(out int numberOfMatches);
            _scoreManager.Initialize(null, numberOfMatches);
        }
    }

    [Serializable]
    public class GameData {
        public int CurrentLevel;
        public int[] Cards;
        public ScoreData ScoreData;
    }
}