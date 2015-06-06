using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serialization;

namespace Savegames
{
    class NewLevelSerializer : LevelSerializer
    {
        public static string SavegameName;
        
        public new static void Resume()
        {
            Resume2();
        }

        public static void Resume2()
        {
            string @str = PlayerPrefsFile.GetString(LevelSerializer.PlayerName + "__" + SavegameName + "__", string.Empty);
            if (string.IsNullOrEmpty(@str))
            {
                return;
            }
            UnitySerializer.Deserialize<LevelSerializer.SaveEntry>(Convert.FromBase64String(@str)).Load();
        }

        public new static void PerformSaveCheckPoint(string name, bool urgent)
        {
            LevelSerializer.SaveEntry saveEntry = LevelSerializer.CreateSaveEntry(name, urgent);
            PlayerPrefsFile.SetString(LevelSerializer.PlayerName + "__" + SavegameName + "__", Convert.ToBase64String(UnitySerializer.Serialize((object)saveEntry)));
            PlayerPrefsFile.Save();
        }
    }
}
