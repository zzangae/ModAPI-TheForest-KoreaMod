using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModAPI;
using ModAPI.Attributes;
using TheForest.Items;
using TheForest.Items.Craft;
using TheForest.Utils;
using UnityEngine;

namespace InventoryMod
{
    internal class Inventory : MonoBehaviour
    {
        protected bool visible;

        protected GUIStyle labelStyle;

        public Vector2 scrollPosition = Vector2.zero;

        private float cY;

        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            new GameObject("__InventoryMenu__").AddComponent<Inventory>();
        }

        private void OnGUI()
        {
            if (this.visible)
            {                
                UnityEngine.GUI.skin = ModAPI.Interface.Skin;
                Matrix4x4 matrix = UnityEngine.GUI.matrix;
                if (this.labelStyle == null)
                {
                    this.labelStyle = new GUIStyle(UnityEngine.GUI.skin.label);
                    this.labelStyle.fontSize = 12;
                }
                UnityEngine.GUI.Box(new Rect(10f, 10f, 400f, 450f), "Inventory menu", UnityEngine.GUI.skin.window);
                this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 390f, 350f), this.scrollPosition, new Rect(0f, 0f, 350f, this.cY));
                this.cY = 25f;
                for (int i = 0; i < ItemDatabase.Items.Length; i++)
                {
                    UnityEngine.GUI.Label(new Rect(20f, this.cY, 150f, 20f), ItemDatabase.Items[i]._name, this.labelStyle);
                    string layerId = (ItemDatabase.Items[i]._id).ToString();
                    if (UnityEngine.GUI.Button(new Rect(170f, this.cY, 70f, 20f), "Add"))
                    {

                        LocalPlayer.Inventory.AddItem(ItemDatabase.Items[i]._id, 1, false, false, null);

                    }
                    if (UnityEngine.GUI.Button(new Rect(250f, this.cY, 70f, 20f), "MAX"))
                    {
                        for (int i2 = 0; i2 < ItemDatabase.Items.Length; ++i2)
                        {
                            LocalPlayer.Inventory.AddItem(ItemDatabase.Items[i]._id, ItemDatabase.Items.Length, false, false, null);
                        }
                    }
                    UnityEngine.GUI.Label(new Rect(320f, this.cY, 150f, 20f), "( "+layerId+" )", this.labelStyle);
                    this.cY += 30f;
                }
                UnityEngine.GUI.EndScrollView();
                if (UnityEngine.GUI.Button(new Rect(280f, 410f, 100f, 20f), "Close"))
                {
                    this.visible = false;
                }
                UnityEngine.GUI.matrix = matrix;
            }
        }

        private void GenerateList()
        {
            for (int i = 0; i < ItemDatabase.Items.Length; i++) {
                ModAPI.Console.Write(string.Concat(new object[] {"itemName", ItemDatabase.Items[i]._name, " itemID: ", ItemDatabase.Items[i]._id}), "InventoryMod");
            }
        }

        private void Update()
        {            
            if (ModAPI.Input.GetButtonDown("Start"))                
            {
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
}
