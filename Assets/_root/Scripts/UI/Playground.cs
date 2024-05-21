using System;
using System.Collections;
using System.Collections.Generic;
using CardMatch.Core;
using CardMatch.Data;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch.UI {
    public class Playground : MonoBehaviour {
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private CardCell _cardCellPrefab;
        [SerializeField] private Transform _cardParent;
        
        //   configs
        private const float SPACE_RATIO = 0.2f;
        private const float MAX_CELL_SIZE = 200;

        private void Awake() {
            MessageDispatcher<MessageID.CardsLoadedEventHandler>.AddListener(OnCardsLoaded);
        }

        private void OnDestroy() {
            MessageDispatcher<MessageID.CardsLoadedEventHandler>.RemoveListener(OnCardsLoaded);
        }

        private void OnCardsLoaded(Card[] cards, float leakingDuration) {
            GetIdealGridSize(cards.Length, out int row, out int column);
            SetUpGridLayout(row, column);
            
            foreach (var card in cards) {
                var cardCell = Instantiate(_cardCellPrefab, _cardParent);
                cardCell.Initialize(card);
                cardCell.Leak(leakingDuration);
            }
        }
        
        private void GetIdealGridSize(int count, out int row, out int column) {
            row = (int)(Mathf.Sqrt(count) + Mathf.Epsilon);
            while (count % row != 0) {
                row--;
            }

            column = count / row;
        }

        private void SetUpGridLayout(int row, int column) {
            Rect gridRect = _gridLayout.GetComponent<RectTransform>().rect;
            float width = column + (column - 1) * SPACE_RATIO;
            float height = row + (row - 1) * SPACE_RATIO;
            float gridLayoutWidth = gridRect.width;
            float gridLayoutHeight = gridRect.height;
            float cellSize = Mathf.Min(gridLayoutHeight / height, gridLayoutWidth / width, MAX_CELL_SIZE);
            float spacing = cellSize * SPACE_RATIO;
            _gridLayout.cellSize = new Vector2(cellSize, cellSize);
            _gridLayout.spacing = new Vector2(spacing, spacing);
            _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayout.constraintCount = column;
        }
    }
}