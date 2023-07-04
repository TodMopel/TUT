using UnityEngine;
using System.IO;

namespace TodMopel
{
    public static class SaveManager
    {
        private static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Resources/SaveFiles";
        //private static readonly string SAVE_FOLDER = Application.dataPath + "/Resources/SaveFiles";

        public static void Init()
        {
            if (!Directory.Exists(SAVE_FOLDER)) {
                Directory.CreateDirectory(SAVE_FOLDER);
            }
        }

        public static void Save(string saveName, string saveString)
        {
            File.WriteAllText(SAVE_FOLDER + "/" + saveName + "_saveFile.txt", saveString);
        }
        public static string Load(string saveName)
        {
            if (File.Exists(SAVE_FOLDER + "/" + saveName + "_saveFile.txt")) {
                string _jsonSave = File.ReadAllText((SAVE_FOLDER + "/" + saveName + "_saveFile.txt"));
                return _jsonSave;
            } else {
                Debug.Log("NoSaves Folder");
                return null;
            }
        }
    }
}
