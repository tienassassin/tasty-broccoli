using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardMatch.Data {
    [CreateAssetMenu(fileName = "Level", menuName = "CardMatch/Level")]
    public class LevelSO : ScriptableObject {
        public Sprite[] cardFaces;
        
        public Card[] GetShuffledCardFaces() {
            Card[] shuffledCardFaces = new Card[cardFaces.Length * 2];
            for (int i = 0; i < cardFaces.Length; i++) {
                Card card = new Card {
                    ID = i,
                    Face = cardFaces[i]
                };
                shuffledCardFaces[i * 2] = card;
                shuffledCardFaces[i * 2 + 1] = card;
            }
            
            for (int i = shuffledCardFaces.Length - 1; i > 0; i--) {
                int k = Random.Range(0, shuffledCardFaces.Length);
                (shuffledCardFaces[k], shuffledCardFaces[i]) = (shuffledCardFaces[i], shuffledCardFaces[k]);
            }

            return shuffledCardFaces;
        }
    }
    
    [Serializable]
    public struct Card {
        public int ID;
        public Sprite Face;
    }
}