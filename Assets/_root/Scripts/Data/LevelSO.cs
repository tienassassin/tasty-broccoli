using UnityEngine;

namespace CardMatch.Data {
    [CreateAssetMenu(fileName = "Level", menuName = "CardMatch/Level")]
    public class LevelSO : ScriptableObject {
        public Sprite[] cardFaces;
        
        public Sprite[] GetShuffledCardFaces() {
            Sprite[] shuffledCardFaces = new Sprite[cardFaces.Length * 2];
            for (int i = 0; i < cardFaces.Length; i++) {
                shuffledCardFaces[i * 2] = cardFaces[i];
                shuffledCardFaces[i * 2 + 1] = cardFaces[i];
            }
            
            for (int i = shuffledCardFaces.Length - 1; i > 0; i--) {
                int k = Random.Range(0, shuffledCardFaces.Length);
                (shuffledCardFaces[k], shuffledCardFaces[i]) = (shuffledCardFaces[i], shuffledCardFaces[k]);
            }

            return shuffledCardFaces;
        }
    }
}