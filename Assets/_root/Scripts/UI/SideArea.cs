using System;
using CardMatch.Core;
using CardMatch.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch.UI {
    public class SideArea : MonoBehaviour {
        [SerializeField] private TMP_Text _txtLevel;
        [SerializeField] private TMP_Text _txtScore;
        [SerializeField] private TMP_Text _txtCombo;
        [SerializeField] private TMP_Text _txtMove;
        [SerializeField] private Image _imgComboLife;

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

        private void OnScoreUpdated(ScoreData scoreData, int comboLifeLimit) {
            _txtMove.text = scoreData.MoveCount.ToString();
            _txtScore.text = scoreData.CurrentScore.ToString();
            _txtCombo.text = scoreData.CurrentCombo.ToString();
            float comboLifeRatio = scoreData.CurrentComboLife / (float)comboLifeLimit;
            _imgComboLife.fillAmount = comboLifeRatio;
            _imgComboLife.color = Color.Lerp(Color.red, Color.green, comboLifeRatio);
        }
    }
}