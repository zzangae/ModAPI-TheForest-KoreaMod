using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TheForest.Items;
using TheForest.Items.Craft;
using TheForest.Utils;
using UnityEngine;

namespace InventoryMod2
{
    internal class Inventory : MonoBehaviour
    {
        public Vector2 scPoWeapons = Vector2.zero;
        public Vector2 scPoItems = Vector2.zero;
        public Vector2 scPoRes = Vector2.zero;
        public Vector2 scPoAnimalHeads = Vector2.zero;
        public Vector2 scPoOther = Vector2.zero;
        public Vector2 scPoAll = Vector2.zero;
        private List<Item> listWeapons = new List<Item>();
        private List<Item> listItems = new List<Item>();
        private List<Item> listResources = new List<Item>();
        private List<Item> listAnimalHeads = new List<Item>();
        private List<Item> listOther = new List<Item>();
        private string[] catWeapons = new string[22]
        {
            "arrow",
            "axe",            
            "ArrowFire",
            "bow",
            "bomb",
            "club",
            "PoisonnedArrow",
            "katana",
            "flaregun",
            "molotov",
            "dynamite",
            "spear",
            "spearRaise",            
            "upgraded",            
            "flintlock",
            "bowFire",            
            "drawBow",
            "smallAxe",
            "repairHammer",
            "chainSaw",
            "RecurveBow",
            "camCorder"
        };

        private string[] catItems = new string[21]
        {
            "lighter",
            "lighterIgnite",
            "walkman",
            "torch",
            "firestick",
            "compass",
            "flare",
            "snowshoes",
            "boots",
            "quiver",
            "pouch",
            "waterskin",
            "pedometer",
            "rebreather",
            "canister",
            "armor",
            "rockbag",
            "stickbag",
            "metaltintray",
            "draw",
            "pot"
        };

        private string[] catResources = new string[47]
        {
            "cboard",
            "cloth",
            "glass",
            "hairspray",
            "cod",
            "coneflower",
            "bone",
            "stick",
            "rock",
            "leaf",
            "tree",
            "sap",
            "battery",
            "booze",
            "rope",
            "food",
            "soda",
            "energy",
            "chocolate",
            "circuit",
            "cash",
            "tooth",
            "berry",
            "arm",
            "berry",
            "aloe",
            "leg",
            "lizard",
            "shell",
            "meds",
            "watch",
            "feather",
            "skin",
            "chicory",
            "mari",
            "coin",
            "paint",
            "skull",
            "seed",
            "meat",
            "rabbit dead",
            "rabbit alive",
            "log",
            "map",
            "generic",
            "genericWide",
            "genericHoldPouch"
        };

        private string[] catAnimals = new string[1] 
        {
            "head"
        };

        protected bool visible;
        protected GUIStyle labelStyle;
        private float cY1;
        private float cY2;
        private float cY3;
        private float cY4;
        private float cY5;
        private bool sorted;
        private bool skip;
        private string buffer;
        protected int Tab;

        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {   
            new GameObject("__InventoryMenu__").AddComponent<Inventory>();
        }

        private void Sort()
        {
            this.sorted = true;
            for (int index = 0; index < ItemDatabase.Items.Length; ++index)
            {
                this.skip = false;
                this.buffer = ItemDatabase.Items[index]._name.ToLower();
                foreach (string catWeapon in this.catWeapons)
                {                    
                    if (this.buffer.Contains(catWeapon))
                    {
                        this.listWeapons.Add(ItemDatabase.Items[index]);
                        this.skip = true;
                        break;
                    }
                }
                if (!this.skip)
                {
                    foreach (string catAnimal in this.catAnimals)
                    {
                        if (this.buffer.Contains(catAnimal))
                        {
                            this.listAnimalHeads.Add(ItemDatabase.Items[index]);
                            this.skip = true;
                            break;
                        }
                    }
                    if (!this.skip)
                    {
                        foreach (string catItem in this.catItems)
                        {
                            if (this.buffer.Contains(catItem))
                            {
                                this.listItems.Add(ItemDatabase.Items[index]);
                                this.skip = true;
                                break;
                            }
                        }
                        if (!this.skip)
                        {
                            foreach (string catResource in this.catResources)
                            {
                                if (this.buffer.Contains(catResource))
                                {
                                    this.listResources.Add(ItemDatabase.Items[index]);
                                    this.skip = true;
                                    break;
                                }
                            }
                            if (!this.skip)
                            {
                                this.listOther.Add(ItemDatabase.Items[index]);
                            }
                        }
                    }
                }
            }
            this.listWeapons.Sort((Comparison<Item>)((x, y) => string.Compare(x._name, y._name)));
            this.listItems.Sort((Comparison<Item>)((x, y) => string.Compare(x._name, y._name)));
            this.listResources.Sort((Comparison<Item>)((x, y) => string.Compare(x._name, y._name)));
            this.listAnimalHeads.Sort((Comparison<Item>)((x, y) => string.Compare(x._name, y._name)));
            this.listOther.Sort((Comparison<Item>)((x, y) => string.Compare(x._name, y._name)));
        }

        private void OnGUI()
        {
            if (!this.visible)
            {
                return;
            }

            GUI.skin = ModAPI.Interface.Skin;
            Matrix4x4 matrix = GUI.matrix;
            if (this.labelStyle == null)
            {
                this.labelStyle = new GUIStyle(GUI.skin.label);
                this.labelStyle.fontSize = 12;
            }
            GUI.Box(new Rect(5f, 5f, 400f, 430f), "Inventory menu", GUI.skin.window);
            this.Tab = UnityEngine.GUI.Toolbar(new Rect(5f, 5f, 400f, 30f), this.Tab, new GUIContent[]
                {
                    new GUIContent("Weapons"),
                    new GUIContent("Items"),
                    new GUIContent("Res."),
                    new GUIContent("Head"),
                    new GUIContent("Other"),
                    new GUIContent("All")
                }, UnityEngine.GUI.skin.GetStyle("Tabs"));
            if (this.Tab == 0)
            {
                this.scPoWeapons = GUI.BeginScrollView(new Rect(5f, 45f, 390f, 370f), this.scPoWeapons, new Rect(0.0f, 0.0f, 330f, this.cY1));
                this.cY1 = 25f;
                //Weapons
                for (int index1 = 0; index1 < this.listWeapons.Count; ++index1)
                {
                    GUI.Label(new Rect(20f, this.cY1, 150f, 20f), this.listWeapons[index1]._name, this.labelStyle);
                    if (this.listWeapons[index1]._name.Equals("Arrows") || this.listWeapons[index1]._name.Equals("ArrowFire") || this.listWeapons[index1]._name.Equals("PoisonnedArrow") || this.listWeapons[index1]._name.Equals("BombTimed") || this.listWeapons[index1]._name.Equals("dynamite") || this.listWeapons[index1]._name.Equals("FlareGunAmmo") || this.listWeapons[index1]._name.Equals("FlintlockAmmo") || this.listWeapons[index1]._name.Equals("Molotov") || this.listWeapons[index1]._name.Equals("CrossbowAmmo"))
                    {
                        if (GUI.Button(new Rect(210f, this.cY1, 70f, 20f), "Add"))
                        {
                            LocalPlayer.Inventory.AddItem(this.listWeapons[index1]._id, 1, false, false, null);
                        }
                        if (GUI.Button(new Rect(290f, this.cY1, 70f, 20f), "MAX"))
                        {
                            for (int index2 = 0; index2 < this.listWeapons.Count; ++index2)
                            {
                                LocalPlayer.Inventory.AddItem(this.listWeapons[index1]._id, this.listWeapons.Count, false, false, null);
                            }
                        }
                    }
                    else if (GUI.Button(new Rect(210f, this.cY1, 150f, 20f), "Add"))
                    {
                        LocalPlayer.Inventory.AddItem(this.listWeapons[index1]._id, 1, false, false, null);
                    }
                    this.cY1 = this.cY1 + 30f;
                }
                GUI.EndScrollView();
            }
            if (this.Tab == 1)
            {
                this.scPoItems = GUI.BeginScrollView(new Rect(5f, 45f, 390f, 370f), this.scPoItems, new Rect(0.0f, 0.0f, 330f, this.cY2));
                this.cY2 = 25f;
                //Items
                for (int index = 0; index < this.listItems.Count; ++index)
                {
                    GUI.Label(new Rect(20f, this.cY2, 150f, 20f), this.listItems[index]._name, this.labelStyle);
                    if (GUI.Button(new Rect(210f, this.cY2, 150f, 20f), "Add"))
                    {
                        LocalPlayer.Inventory.AddItem(this.listItems[index]._id, 1, false, false, null);
                    }
                    this.cY2 = this.cY2 + 30f;
                }
                GUI.EndScrollView();
            }            
            if (this.Tab == 2)
            {
                this.scPoRes = GUI.BeginScrollView(new Rect(5f, 45f, 390f, 370f), this.scPoRes, new Rect(0.0f, 0.0f, 330f, this.cY3));
                this.cY3 = 25f;
                //Resource
                for (int index1 = 0; index1 < this.listResources.Count; ++index1)
                {
                    GUI.Label(new Rect(20f, this.cY3, 150f, 20f), this.listResources[index1]._name, this.labelStyle);
                    if (GUI.Button(new Rect(210f, this.cY3, 70f, 20f), "Add"))
                    {
                        LocalPlayer.Inventory.AddItem(this.listResources[index1]._id, 1, false, false, null);
                    }
                    if (GUI.Button(new Rect(290f, this.cY3, 70f, 20f), "MAX"))
                    {
                        for (int index2 = 0; index2 < this.listResources.Count; ++index2)
                        {
                            LocalPlayer.Inventory.AddItem(this.listResources[index1]._id, this.listResources.Count, false, false, null);
                        }
                    }
                    this.cY3 = this.cY3 + 30f;
                }
                GUI.EndScrollView();
            }
            if (this.Tab == 3)
            {
                this.scPoAnimalHeads = GUI.BeginScrollView(new Rect(5f, 45f, 390f, 370f), this.scPoAnimalHeads, new Rect(0.0f, 0.0f, 330f, this.cY4));
                this.cY4 = 25f;
                //AnimalHeads
                for (int index = 0; index < this.listAnimalHeads.Count; ++index)
                {
                    GUI.Label(new Rect(20f, this.cY4, 150f, 20f), this.listAnimalHeads[index]._name, this.labelStyle);
                    if (GUI.Button(new Rect(210f, this.cY4, 150f, 20f), "Add"))
                    {
                        LocalPlayer.Inventory.AddItem(this.listAnimalHeads[index]._id, 1, false, false, null);
                    }
                    this.cY4 = this.cY4 + 30f;
                }
                GUI.EndScrollView();
            }
            if (this.Tab == 4)
            {
                this.scPoOther = GUI.BeginScrollView(new Rect(5f, 45f, 390f, 370f), this.scPoOther, new Rect(0.0f, 0.0f, 330f, this.cY4));
                this.cY4 = 25f;
                //Other
                for (int index = 0; index < this.listOther.Count; ++index)
                {
                    GUI.Label(new Rect(20f, this.cY4, 150f, 20f), this.listOther[index]._name, this.labelStyle);
                    if (GUI.Button(new Rect(210f, this.cY4, 150f, 20f), "Add"))
                    {
                        LocalPlayer.Inventory.AddItem(this.listOther[index]._id, 1, false, false, null);
                    }
                    this.cY4 = this.cY4 + 30f;
                }                
                GUI.EndScrollView();
            }
            if (this.Tab ==5)
            {
                this.scPoAll = GUI.BeginScrollView(new Rect(5f, 45f, 390f, 370f), this.scPoAll, new Rect(0.0f, 0.0f, 330f, this.cY5));
                this.cY5 = 25f;
                //All Items
                GUI.Label(new Rect(20f, this.cY5, 150f, 20f), "[ All Items ]", this.labelStyle);
                if (GUI.Button(new Rect(100f, this.cY5, 70f, 20f), "Give all"))
                {
                    for (int i = 0; i < ItemDatabase.Items.Length; i++)
                    {
                        Item item = ItemDatabase.Items[i];
                        try
                        {
                            if (item._maxAmount >= 0)
                            {
                                LocalPlayer.Inventory.AddItem(item._id, 1000 - LocalPlayer.Inventory.AmountOf(item._id, true), true, false, null);
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }

                this.cY5 = this.cY5 + 35f;
                //list Items
                GUI.Label(new Rect(20f, this.cY5, 150f, 20f), "[ List Items - item number ]", this.labelStyle);
                this.cY5 = 55f;
                foreach (Item item in ItemDatabase.Items.OrderBy((Item i) => i._name))
                {
                    this.cY5 = this.cY5 + 30f;
                    string text = item._name + " [" + item._id + "]";
                    GUI.Label(new Rect(20f, this.cY5, 150f, 20f), text, this.labelStyle);
                }
                GUI.EndScrollView();
                GUI.matrix = matrix;
            }
        }

        private void GenerateList()
        {
            for (int index = 0; index < ItemDatabase.Items.Length; ++index)
            {
                ModAPI.Console.Write("itemName" + ItemDatabase.Items[index]._name + " itemID: " + (object)ItemDatabase.Items[index]._id, "InventoryModSorted");
            }
        }

        private void Update()
        {
            if (!this.sorted)
            {
                this.Sort();
            }
            if (!ModAPI.Input.GetButtonDown("StartInventoryMenu2"))
            {
                return;
            }
            if (this.visible)
            {
                LocalPlayer.FpCharacter.UnLockView();
            }
            else
            {
                LocalPlayer.FpCharacter.LockView(true);
            }
            this.visible = !this.visible;
        }
    }
}
