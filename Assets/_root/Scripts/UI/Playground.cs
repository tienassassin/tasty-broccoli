using System;
using CardMatch.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch.UI {
    public class Playground : MonoBehaviour {
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private Transform _cardParent;

        //   configs
        private const float SPACE_RATIO = 0.2f;
        private const float MAX_CELL_SIZE = 200;

        private void Awake() {
            MessageDispatcher<MessageID.OnCardsLoaded>.AddListener(OnCardsLoaded);

            foreach (Transform child in _cardParent) {
                Destroy(child.gameObject);
            }
        }

        private void OnDestroy() {
            MessageDispatcher<MessageID.OnCardsLoaded>.RemoveListener(OnCardsLoaded);
        }

        private void OnCardsLoaded(Sprite[] cardFaces) {
            GetIdealGridSize(cardFaces.Length, out int row, out int column);
            SetUpGridLayout(row, column);
            
            foreach (var face in cardFaces) {
                var card = Instantiate(_cardPrefab, _cardParent);
                card.Initialize(face);
            }
        }
        
        private void GetIdealGridSize(int totalCard, out int row, out int column) {
            row = (int)(Mathf.Sqrt(totalCard) + Mathf.Epsilon);
            while (totalCard % row != 0) {
                row--;
            }

            column = totalCard / row;
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