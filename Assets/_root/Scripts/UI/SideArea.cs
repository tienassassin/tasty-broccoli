using System;
using CardMatch.Core;
using CardMatch.Gameplay;
using TMPro;
using UnityEngine;

namespace CardMatch.UI {
    public class SideArea : MonoBehaviour {
        [SerializeField] private TMP_Text _txtLevel;
        [SerializeField] private TMP_Text _txtScore;
        [SerializeField] private TMP_Text _txtCombo;
        [SerializeField] private TMP_Text _txtComboLife;
        [SerializeField] private TMP_Text _txtMove;

        private void Awake() {
            MessageDispatcher<MessageID.ScoreUpdatedEventHandler>.AddListener(OnScoreUpdated);
            MessageDispatcher<MessageID.LevelUpdatedEventHandler>.AddListener(OnLevelUpdated);
        }

        private void OnDestroy() {
            MessageDispatcher<MessageID.ScoreUpdatedEventHandler>.RemoveListener(OnScoreUpdated);
            MessageDispatcher<MessageID.LevelUpdatedEventHandler>.RemoveListener(OnLevelUpdated);
        }
        
        private void OnLevelUpdated(int level) {
            _txtLevel.text = "level " + level;
        }

        private void OnScoreUpdated(ScoreData scoreData) {
            _txtMove.text = scoreData.MoveCount.ToString();
            _txtScore.text = scoreData.CurrentScore.ToString();
            _txtCombo.text = scoreData.CurrentCombo.ToString();
            _txtComboLife.text = scoreData.CurrentComboLife.ToString();
        }
    }
}