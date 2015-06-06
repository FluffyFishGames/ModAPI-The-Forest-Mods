using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Serialization;

namespace Savegames
{
    class SavegameHolder
    {
        protected static string start = "PlayerPrefsFile___";
        protected static string end = "__.dat";
        public delegate void SavegameAdded(string name);
        public static SavegameAdded OnSavegameAdded;
        protected static bool Initialized = false;
        public static void Initialize()
        {
            if (Initialized) return;
            Initialized = true;
            string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath);
            
            foreach (string file in files)
            {
                string fileName = System.IO.Path.GetFileName(file);
                if (fileName.StartsWith(start) && fileName.EndsWith(end))
                {
                    string name = fileName.Substring(start.Length, fileName.Length - start.Length - end.Length);
                    Savegames.Add(name);
                }
            }
        }

        protected static List<string> Savegames = new List<string>();

        public static void Save(string name)
        {
            try
            {
                if (name == "Standard savegame")
                    name = "RESUME";
                if (!Savegames.Contains(name))
                {
                    if (OnSavegameAdded != null)
                        OnSavegameAdded(name);
                    Savegames.Add(name);
                }
                NewLevelSerializer.SavegameName = name;
                TheForest.Utils.Scene.Cams.SaveCam.SetActive(true);
                LevelSerializer.Checkpoint();
                TheForest.Utils.LocalPlayer.Stats.Invoke("TurnOffSleepCam", 2f);
            }
            catch (Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }

        public static void Load(string name)
        {
            ModAPI.Log.Write("LOAD => " + name);
            if (name == "Standard savegame")
                name = "RESUME";
            ModAPI.Log.Write("LOAD => " + name);
            NewLevelSerializer.SavegameName = name;
            ModAPI.Log.Write("LOAD => " + name);
            
        }

        public static List<string> GetDisplayNames()
        {
            List<string> ret = new List<string>();
            foreach (string Savegame in Savegames)
            {
                string displayName = Savegame;
                if (displayName == "RESUME")
                    displayName = "Standard savegame";
                ret.Add(displayName);
            }

            return ret;
        }
    }
}
