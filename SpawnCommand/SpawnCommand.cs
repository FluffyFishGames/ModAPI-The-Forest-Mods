using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpawnCommand
{
    public class SpawnCommand
    {

        public static List<string> ObjectNames = new List<string>();
        public static Dictionary<string, GameObject> Objects = new Dictionary<string, GameObject>();

        [ModAPI.Attributes.ExecuteOnApplicationStart]
        public static void AddSpawnCommand()
        {
            ModAPI.Console.RegisterCommand(new ModAPI.Console.Command()
            {
                CommandName = "spawn",
                HelpText = "Spawns an item/object",
                OnSubmit = delegate (object[] objs) {
                    string name = (string) objs[0];
                    GameObject.Instantiate(Objects[name], TheForest.Utils.LocalPlayer.MainCam.transform.position + TheForest.Utils.LocalPlayer.MainCam.transform.forward * 2f, Quaternion.identity);
                },
                Parameters = new List<ModAPI.Console.IConsoleParameter>()
                {
                    new ModAPI.Console.BaseConsoleParameter()
                    {
                        IsOptional = false,
                        UseAutoComplete = true,
                        Name = "Object",
                        ListValueRequired = true,
                        TooltipText = "",
                        Values = ObjectNames
                    }
                }
            });
        }

        protected static Dictionary<string, int> MutantCounts = new Dictionary<string, int>();
        protected static void AddEnemy(string name, GameObject go)
        {
            if (go != null)
            {
                if (!MutantCounts.ContainsKey(name))
                    MutantCounts.Add(name, 0);

                bool add = true;
                for (int i = 0; i < MutantCounts[name]; i++)
                {
                    string n = "";
                    if (i > 0)
                        n = i+"";
                    if (Objects["Enemy." + name + n] == go)
                    {
                        add = false;
                        break;
                    }
                }
                if (add)
                {
                    string nk = "";
                    if (MutantCounts[name] > 0)
                        nk = MutantCounts[name] + "";
                    ObjectNames.Add("Enemy."+name + nk);
                    Objects.Add("Enemy."+name + nk, go);
                    MutantCounts[name]++;
                }
            }
        }
        [ModAPI.Attributes.ExecuteEveryFrame]
        public static void FindEnemies()
        {
            if (!Objects.ContainsKey("Enemy.mutant"))
            {
                spawnMutants[] m = (spawnMutants[]) GameObject.FindObjectsOfTypeAll(typeof(spawnMutants));
                for (int i = 0; i < m.Length; i++)
                {
                    spawnMutants mk = m[i];
                    AddEnemy("mutant", mk.mutant);
                    AddEnemy("mutant_female", mk.mutant_female);
                    AddEnemy("mutant_pale", mk.mutant_pale);
                    AddEnemy("armsy", mk.armsy);
                    AddEnemy("vags", mk.vags);
                    AddEnemy("baby", mk.baby);
                    AddEnemy("fat", mk.fat);
                }
            }
        }

        [ModAPI.Attributes.ExecuteOnGameStart]
        public static void FindObjects()
        {
            ObjectNames.Clear();
            Objects.Clear();
            try
            {
                GreebleZone[] zones = (GreebleZone[])GameObject.FindObjectsOfTypeAll(typeof(GreebleZone));
                foreach (GreebleZone zone in zones)
                {
                    foreach (GreebleDefinition definition in zone.GreebleDefinitions)
                    {
                        string name = "Prop." + definition.Prefab.name;
                        if (!ObjectNames.Contains(name))
                        {
                            ObjectNames.Add(name);
                            Objects.Add(name, definition.Prefab);
                        }
                    }
                }

                AnimalSpawnZone[] animalSpawns = GameObject.FindObjectsOfType<AnimalSpawnZone>();
                foreach (AnimalSpawnZone aZone in animalSpawns)
                {
                    foreach (AnimalSpawnConfig config in aZone.Spawns)
                    {
                        string name = "Animal." + config.Prefab.name;
                        if (!ObjectNames.Contains(name))
                        {
                            ObjectNames.Add(name);
                            Objects.Add(name, config.Prefab);
                        }
                    }
                }
                
            } catch (System.Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }
    }
}
