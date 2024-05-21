using System;
using System.IO;
using CardMatch.Core;
using CardMatch.Utils;
using UnityEngine;

namespace CardMatch.Gameplay {
    public class GameManager : Singleton<GameManager> {
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private LevelManager _levelManager;

        public GameData _data;
        
        protected override void OnAwake() {
            
        }

        [ContextMenu("save")]
        public void Save() {
            FileManager.Save(_data, Constants.FilePaths.GAME_DATA_PATH);
        }

        [ContextMenu("load")]
        public void Load() {
            _data = FileManager.Load<GameData>(Constants.FilePaths.GAME_DATA_PATH);
        }
    }

    [Serializable]
    public class GameData {
        public int a;
    }
}