using System;
using CardMatch.Core;
using TMPro;
using UnityEngine;

namespace CardMatch.UI {
    public class SideArea : MonoBehaviour {
        [SerializeField] private TMP_Text _txtScore;
        [SerializeField] private TMP_Text _txtCombo;
        [SerializeField] private TMP_Text _txtComboLife;

        private void Awake() {
            MessageDispatcher<MessageID.OnScoreUpdated>.AddListener(OnScoreUpdated);
        }

        private void OnDestroy() {
            MessageDispatcher<MessageID.OnScoreUpdated>.RemoveListener(OnScoreUpdated);
        }

        private void OnScoreUpdated(int score, int combo, int comboLife) {
            _txtScore.text = score.ToString();
            _txtCombo.text = combo.ToString();
            _txtComboLife.text = comboLife.ToString();
        }
    }
}