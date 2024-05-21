using CardMatch.Core;
using CardMatch.Editor;
using CardMatch.Utils;
using UnityEngine;

namespace CardMatch.Gameplay {
    public class DataManager : MonoBehaviour {
        public void Initialize(out GameData gameData) {
            gameData = FileManager.Load<GameData>(Constants.FilePaths.GAME_DATA_PATH);
        }
        
        public void Save(GameData data) {
            FileManager.Save(data, Constants.FilePaths.GAME_DATA_PATH);
        }
    }
}