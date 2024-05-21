using System;
using CardMatch.Core;
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

        private void OnScoreUpdated(int score, int combo, int comboLife, int move) {
            _txtScore.text = score.ToString();
            _txtCombo.text = combo.ToString();
            _txtComboLife.text = comboLife.ToString();
            _txtMove.text = move.ToString();
        }
    }
}