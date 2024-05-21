using System.IO;
using UnityEngine;

namespace CardMatch.Utils {
    public static class Constants {
        public static class FilePaths {
            public static string GAME_DATA_PATH = Path.Combine(Application.persistentDataPath, "game_data.dat");
        }
    }
}