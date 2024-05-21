using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Logger = CardMatch.Utils.Logger;

namespace CardMatch.Core {
    public static class FileManager {
        public static void Save(object data, string path) {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Create)) {
                MemoryStream memoryStream = new MemoryStream();
                formatter.Serialize(memoryStream, data);
                byte[] binaryData = memoryStream.ToArray();
                string base64Data = Convert.ToBase64String(binaryData);
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(base64Data);
                writer.Close();
                Logger.Log("Game data saved");
            }
        }

        public static T Load<T>(string path) {
            if (!File.Exists(path)) {
                Logger.Log("Game data file not found");
                return default;
            }
            
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open)) {
                StreamReader reader = new StreamReader(stream);
                string base64Data = reader.ReadToEnd();
                byte[] binaryData = Convert.FromBase64String(base64Data);
                MemoryStream memoryStream = new MemoryStream(binaryData);
                return (T) formatter.Deserialize(memoryStream);
            }
        }
    }
}