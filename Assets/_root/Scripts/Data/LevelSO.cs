using System;
using System.Collections.Generic;
using CardMatch.Editor;
using UnityEngine;
using Logger = CardMatch.Utils.Logger;
using Random = UnityEngine.Random;

namespace CardMatch.Data {
    [CreateAssetMenu(fileName = "Level", menuName = "CardMatch/Level")]
    public class LevelSO : ScriptableObject {
        [SerializeField] private int[] _cards;
        
        public int[] GetShuffledCards() {
            if (HasDuplicateCards()) {
                Logger.Log($"Duplicate cards found in {name}. Please fix it");
                return null;
            }
            
            int[] shuffledCards = new int[_cards.Length * 2];
            for (int i = 0; i < _cards.Length; i++) {
                shuffledCards[i * 2] = _cards[i];
                shuffledCards[i * 2 + 1] = _cards[i];
            }
            
            for (int i = shuffledCards.Length - 1; i > 0; i--) {
                int k = Random.Range(0, shuffledCards.Length);
                (shuffledCards[k], shuffledCards[i]) = (shuffledCards[i], shuffledCards[k]);
            }

            return shuffledCards;
        }

        private bool HasDuplicateCards() {
            HashSet<int> cardSet = new HashSet<int>();
            foreach (var card in _cards) {
                if (!cardSet.Add(card)) return true;
            }

            return false;
        }
    }
}