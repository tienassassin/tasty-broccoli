using System;
using CardMatch.Core;
using CardMatch.Data;
using CardMatch.Editor;
using UnityEngine;
using Logger = CardMatch.Utils.Logger;

namespace CardMatch.Gameplay {
    public class LevelManager : MonoBehaviour {
        [SerializeField] private LevelSO[] _levels;
        [SerializeField, ReadOnly] private Sprite[] _cardFaces;

        private void Start() {
            GetCardFaces();
            LoadCards();
        }

        private void GetCardFaces() {
            _cardFaces = _levels[0].GetShuffledCardFaces();
        }

        private void LoadCards() {
            MessageDispatcher<MessageID.OnCardsLoaded>.Handle()?.Invoke(_cardFaces);
        }
    }
}