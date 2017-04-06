using System;
using System.Collections.Generic;
using ModAPI;
using ModAPI.Attributes;
using TheForest.Items;
using TheForest.Items.Craft;
using TheForest.Utils;
using UnityEngine;

namespace SpawnCommand
{
    public class SpawnCommand : MonoBehaviour
    {
        public static List<string> ItemNames = new List<string>();
        public static string lastObject = "";
        public static string lastObjectType = "";
        protected static Dictionary<string, int> MutantCounts = new Dictionary<string, int>();
        public static List<string> ObjectNames = new List<string>();
        public static Dictionary<string, GameObject> Objects = new Dictionary<string, GameObject>();



        protected static void AddEnemy(string name, GameObject go)
        {
            if (go != null)
            {
                if (!MutantCounts.ContainsKey(name))
                {
                    MutantCounts.Add(name, 0);
                }
                bool flag = true;
                for (int i = 0; i < MutantCounts[name]; i++)
                {
                    string str = "";
                    if (i > 0)
                    {
                        str = i + "";
                    }
                    if (Objects["Enemy." + name + str] == go)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    Dictionary<string, int> dictionary;
                    string str3;
                    string str2 = "";
                    if (MutantCounts[name] > 0)
                    {
                        str2 = MutantCounts[name] + "";
                    }
                    ObjectNames.Add("Enemy." + name + str2);
                    Objects.Add("Enemy." + name + str2, go);
                    (dictionary = MutantCounts)[str3 = name] = dictionary[str3] + 1;
                }
            }
        }

        


        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            //new GameObject("__SpawnCommand__").AddComponent<SpawnCommand.SpawnCommand>();
            GameObject GO = new GameObject("__SpawnCommand__");
            GO.AddComponent<SpawnCommand>();
        }

        [ExecuteOnApplicationStart]
        public static void AddSpawnCommand()
        {
            ModAPI.Console.Command command = new ModAPI.Console.Command
            {
                CommandName = "spawn",
                HelpText = "Spawns an item/object",
                OnSubmit = delegate (object[] objs) {
                    string str = (string)objs[0];
                    lastObject = str;
                    lastObjectType = "spawn";
                    UnityEngine.Object.Instantiate(Objects[str], LocalPlayer.MainCam.transform.position + ((Vector3)(LocalPlayer.MainCam.transform.forward * 2f)), Quaternion.identity);
                }
            };
            List<ModAPI.Console.IConsoleParameter> list = new List<ModAPI.Console.IConsoleParameter>();
            ModAPI.Console.BaseConsoleParameter item = new ModAPI.Console.BaseConsoleParameter
            {
                IsOptional = false,
                UseAutoComplete = true,
                Name = "Object",
                ListValueRequired = true,
                TooltipText = "",
                Values = ObjectNames
            };
            list.Add(item);
            command.Parameters = list;
            ModAPI.Console.RegisterCommand(command);
            ModAPI.Console.Command command2 = new ModAPI.Console.Command
            {
                CommandName = "add",
                HelpText = "Add items to inventory",
                OnSubmit = delegate (object[] objs) {
                    string str = (string)objs[0];
                    int amount = 1;
                    for (int j = 0; j < ItemDatabase.Items.Length; j++)
                    {
                        try
                        {
                            if ((ItemDatabase.Items[j]._name == str) || (str == "All"))
                            {
                                if (str == "All")
                                {
                                    amount = 50;
                                }
                                if ((ItemDatabase.Items[j]._name != "BombTimed") || (str != "All"))
                                {
                                    lastObject = ItemDatabase.Items[j]._id.ToString();
                                    lastObjectType = "add";
                                    LocalPlayer.Inventory.AddItem(ItemDatabase.Items[j]._id, amount, false, false, ~WeaponStatUpgrade.Types.smashDamage);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            ModAPI.Console.Lines.Add("[Item]: Error while adding " + ItemDatabase.Items[j]._name);
                        }
                    }
                }
            };
            List<ModAPI.Console.IConsoleParameter> list2 = new List<ModAPI.Console.IConsoleParameter>();
            ModAPI.Console.BaseConsoleParameter parameter2 = new ModAPI.Console.BaseConsoleParameter
            {
                IsOptional = false,
                UseAutoComplete = true,
                Name = "Object",
                ListValueRequired = true,
                TooltipText = "",
                Values = ItemNames
            };
            list2.Add(parameter2);
            command2.Parameters = list2;
            ModAPI.Console.RegisterCommand(command2);
        }

        [ExecuteEveryFrame(true)]
        public static void FindEnemies()
        {
            if (!Objects.ContainsKey("Enemy.mutant"))
            {
                foreach (spawnMutants mutants in (spawnMutants[])FindObjectsOfTypeAll(typeof(spawnMutants)))
                {
                    AddEnemy("mutant", mutants.mutant);
                    AddEnemy("mutant_female", mutants.mutant_female);
                    AddEnemy("mutant_pale", mutants.mutant_pale);
                    AddEnemy("armsy", mutants.armsy);
                    AddEnemy("vags", mutants.vags);
                    AddEnemy("baby", mutants.baby);
                    AddEnemy("fat", mutants.fat);
                }
            }
        }

        [ExecuteOnGameStart]
        public static void FindObjects()
        {
            ObjectNames.Clear();
            ItemNames.Clear();
            Objects.Clear();
            try
            {
                GreebleZone[] zoneArray = (GreebleZone[])FindObjectsOfTypeAll(typeof(GreebleZone));
                foreach (GreebleZone zone in zoneArray)
                {
                    foreach (GreebleDefinition definition in zone.GreebleDefinitions)
                    {
                        string item = "Prop." + definition.Prefab.name;
                        if (!ObjectNames.Contains(item))
                        {
                            ObjectNames.Add(item);
                            Objects.Add(item, definition.Prefab);
                        }
                    }
                }
                for (int i = 0; i < ItemDatabase.Items.Length; i++)
                {
                    string str2 = ItemDatabase.Items[i]._name;
                    if (!ItemNames.Contains(str2))
                    {
                        ItemNames.Add(str2);
                    }
                }
                ItemNames.Add("All");
                foreach (AnimalSpawnZone zone2 in UnityEngine.Object.FindObjectsOfType<AnimalSpawnZone>())
                {
                    foreach (AnimalSpawnConfig config in zone2.Spawns)
                    {
                        string str3 = "Animal." + config.Prefab.name;
                        if (!ObjectNames.Contains(str3))
                        {
                            ObjectNames.Add(str3);
                            Objects.Add(str3, config.Prefab);
                        }
                    }
                }
                ObjectNames.Sort();
                ItemNames.Sort();
            }
            catch (Exception exception)
            {
                Log.Write(exception.ToString(), "SpawnCommand");
            }
        }

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("Respawn", "SpawnCommand"))
            {
                if (lastObjectType == "spawn")
                {
                    UnityEngine.Object.Instantiate(Objects[lastObject], LocalPlayer.MainCam.transform.position + ((Vector3)(LocalPlayer.MainCam.transform.forward * 2f)), Quaternion.identity);
                }
                else if (lastObjectType == "add")
                {
                    LocalPlayer.Inventory.AddItem(int.Parse(lastObject), 1, false, false, ~WeaponStatUpgrade.Types.smashDamage);
                }
            }
            if (ModAPI.Input.GetButton("RespawnInfinite", "SpawnCommand"))
            {
                if (lastObjectType == "spawn")
                {
                    UnityEngine.Object.Instantiate(Objects[lastObject], LocalPlayer.MainCam.transform.position + ((Vector3)(LocalPlayer.MainCam.transform.forward * 2f)), Quaternion.identity);
                }
                else if (lastObjectType == "add")
                {
                    LocalPlayer.Inventory.AddItem(int.Parse(lastObject), 1, false, false, ~WeaponStatUpgrade.Types.smashDamage);
                }
            }
        }


    }
}
