using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using TheForest.Items;
using TheForest.Items.Craft;
using TheForest.Utils;
using UnityEngine;

namespace InventoryMod2
{
    internal class Inventory : MonoBehaviour
    {
        public Vector2 scrollPosition = Vector2.zero;
        public Vector2 scrollPosition2 = Vector2.zero;
        private List<Item> listWeapons = new List<Item>();
        private List<Item> listItems = new List<Item>();
        private List<Item> listResources = new List<Item>();
        private List<Item> listAnimals = new List<Item>();
        private List<Item> listOther = new List<Item>();
        private string[] catWeapons = new string[12]
        {
            "club",
            "axe",
            "katana",
            "flaregun",
            "molotov",
            "dynamite",
            "spear",
            "bomb",
            "upgraded",
            "bow",
            "arrows",
            "flintlock"
        };

        private string[] catItems = new string[18]
        {
            "lighter",
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
            "metaltintray"
        };

        private string[] catResources = new string[43]
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
            "log"
        };

        private string[] catAnimals = new string[1] 
        {
            "head"
        };

        protected bool visible;
        protected GUIStyle labelStyle;
        private float cY;
        private float cY2;
        private bool sorted;
        private bool skip;
        private string buffer;

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
                    if (this.buffer.Contains(catWeapon) && !this.buffer.Contains("part"))
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
                            this.listAnimals.Add(ItemDatabase.Items[index]);
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
            this.listAnimals.Sort((Comparison<Item>)((x, y) => string.Compare(x._name, y._name)));
            this.listOther.Sort((Comparison<Item>)((x, y) => string.Compare(x._name, y._name)));
        }

        private void OnGUI()
        {
            if (!this.visible)
            {
                return;
            }
            UnityEngine.GUI.skin = ModAPI.GUI.Skin;
            Matrix4x4 matrix = UnityEngine.GUI.matrix;
            if (this.labelStyle == null)
            {
                this.labelStyle = new GUIStyle(UnityEngine.GUI.skin.label);
                this.labelStyle.fontSize = 12;
            }
            UnityEngine.GUI.Box(new Rect(5f, 5f, 700f, 610f), "Inventory menu", UnityEngine.GUI.skin.window);
            this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(5f, 45f, 350f, 550f), this.scrollPosition, new Rect(0.0f, 0.0f, 330f, this.cY));
            this.cY = 25f;
            UnityEngine.GUI.Label(new Rect(20f, this.cY, 150f, 20f), "Weapons", this.labelStyle);
            this.cY = this.cY + 30f;
            for (int index1 = 0; index1 < this.listWeapons.Count; ++index1)
            {
                UnityEngine.GUI.Label(new Rect(20f, this.cY, 150f, 20f), this.listWeapons[index1]._name, this.labelStyle);
                if (this.listWeapons[index1]._name.Equals("Arrows") || this.listWeapons[index1]._name.Equals("FlareGunAmmo") || this.listWeapons[index1]._name.Equals("FlintlockAmmo"))
                {
                    if (UnityEngine.GUI.Button(new Rect(170f, this.cY, 70f, 20f), "Add"))
                    {
                        LocalPlayer.Inventory.AddItem(this.listWeapons[index1]._id, 1, false, false, null);
                    }
                    if (UnityEngine.GUI.Button(new Rect(250f, this.cY, 70f, 20f), "Add 10"))
                    {
                        for (int index2 = 0; index2 < 10; ++index2)
                        {
                            LocalPlayer.Inventory.AddItem(this.listWeapons[index1]._id, 1, false, false, null);
                        }
                    }
                }
                else if (UnityEngine.GUI.Button(new Rect(170f, this.cY, 150f, 20f), "Add"))
                {
                    LocalPlayer.Inventory.AddItem(this.listWeapons[index1]._id, 1, false, false, null);
                }
                this.cY = this.cY + 30f;
            }
            UnityEngine.GUI.Label(new Rect(20f, this.cY, 150f, 20f), "Items", this.labelStyle);
            this.cY = this.cY + 30f;
            for (int index = 0; index < this.listItems.Count; ++index)
            {
                UnityEngine.GUI.Label(new Rect(20f, this.cY, 150f, 20f), this.listItems[index]._name, this.labelStyle);
                if (UnityEngine.GUI.Button(new Rect(170f, this.cY, 150f, 20f), "Add"))
                {
                    LocalPlayer.Inventory.AddItem(this.listItems[index]._id, 1, false, false, null);
                }
                this.cY = this.cY + 30f;
            }
            UnityEngine.GUI.EndScrollView();
            this.scrollPosition2 = UnityEngine.GUI.BeginScrollView(new Rect(350f, 45f, 350f, 550f), this.scrollPosition2, new Rect(0.0f, 0.0f, 330f, this.cY2));
            this.cY2 = 25f;
            UnityEngine.GUI.Label(new Rect(20f, this.cY2, 150f, 20f), "Resources", this.labelStyle);
            this.cY2 = this.cY2 + 30f;
            for (int index1 = 0; index1 < this.listResources.Count; ++index1)
            {
                UnityEngine.GUI.Label(new Rect(20f, this.cY2, 150f, 20f), this.listResources[index1]._name, this.labelStyle);
                if (UnityEngine.GUI.Button(new Rect(170f, this.cY2, 70f, 20f), "Add 1"))
                {
                    LocalPlayer.Inventory.AddItem(this.listResources[index1]._id, 1, false, false, null);
                }
                if (UnityEngine.GUI.Button(new Rect(250f, this.cY2, 70f, 20f), "Add 5"))
                {
                    for (int index2 = 0; index2 < 5; ++index2)
                    {
                        LocalPlayer.Inventory.AddItem(this.listResources[index1]._id, 1, false, false, null);
                    }
                }
                this.cY2 = this.cY2 + 30f;
            }
            UnityEngine.GUI.Label(new Rect(20f, this.cY2, 150f, 20f), "Other", this.labelStyle);
            this.cY2 = this.cY2 + 30f;
            for (int index = 0; index < this.listOther.Count; ++index)
            {
                UnityEngine.GUI.Label(new Rect(20f, this.cY2, 150f, 20f), this.listOther[index]._name, this.labelStyle);
                if (UnityEngine.GUI.Button(new Rect(170f, this.cY2, 150f, 20f), "Add"))
                {
                    LocalPlayer.Inventory.AddItem(this.listOther[index]._id, 1, false, false, null);
                }
                this.cY2 = this.cY2 + 30f;
            }
            UnityEngine.GUI.Label(new Rect(20f, this.cY2, 150f, 20f), "Heads", this.labelStyle);
            this.cY2 = this.cY2 + 30f;
            for (int index = 0; index < this.listAnimals.Count; ++index)
            {
                UnityEngine.GUI.Label(new Rect(20f, this.cY2, 150f, 20f), this.listAnimals[index]._name, this.labelStyle);
                if (UnityEngine.GUI.Button(new Rect(170f, this.cY2, 150f, 20f), "Add"))
                {
                    LocalPlayer.Inventory.AddItem(this.listAnimals[index]._id, 1, false, false, null);
                }
                this.cY2 = this.cY2 + 30f;
            }
            UnityEngine.GUI.EndScrollView();
            UnityEngine.GUI.matrix = matrix;
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
