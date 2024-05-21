using System;
using CardMatch.Core;
using CardMatch.Gameplay;
using TMPro;
using UnityEngine;

namespace CardMatch.UI {
    public class SideArea : MonoBehaviour {
        [SerializeField] private TMP_Text _txtScore;
        [SerializeField] private TMP_Text _txtCombo;
        [SerializeField] private TMP_Text _txtComboLife;
        [SerializeField] private TMP_Text _txtMove;

        private void Awake() {
            MessageDispatcher<MessageID.ScoreUpdatedEventHandler>.AddListener(OnScoreUpdated);
        }

        private void OnDestroy() {
            MessageDispatcher<MessageID.ScoreUpdatedEventHandler>.RemoveListener(OnScoreUpdated);
        }

        private void OnScoreUpdated(ScoreData scoreData) {
            _txtMove.text = scoreData.MoveCount.ToString();
            _txtScore.text = scoreData.CurrentScore.ToString();
            _txtCombo.text = scoreData.CurrentCombo.ToString();
            _txtComboLife.text = scoreData.CurrentComboLife.ToString();
        }
    }
}