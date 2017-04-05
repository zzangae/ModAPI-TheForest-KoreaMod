using UnityEngine;
using TheForest.Items;
using TheForest.Utils;

namespace InventoryMod
{
    class Inventory : MonoBehaviour
    {
        protected bool visible;

        protected GUIStyle labelStyle;

        public Vector2 scrollPosition = Vector2.zero;

        private float cY;

        [ModAPI.Attributes.ExecuteOnGameStart]
        static void AddMeToScene()
        {
            GameObject GO = new GameObject("__InventoryMenu__");
            GO.AddComponent<Inventory>();
        }

        private void OnGUI()
        {
            if (this.visible)
            {
                GUI.skin = ModAPI.GUI.Skin;

                Matrix4x4 Matrix = GUI.matrix;

                if (labelStyle == null)
                {
                    labelStyle = new GUIStyle(GUI.skin.label);
                    labelStyle.fontSize = 12;
                }
                
                GUI.Box(new Rect(10, 10, 400, 450), "Cheat menu", GUI.skin.window);
                scrollPosition = GUI.BeginScrollView(new Rect(10, 50, 390, 350), scrollPosition, new Rect(0, 0, 350, cY));
                this.cY = 25f;
                for (int i = 0; i < ItemDatabase.Items.Length; ++i)
                {
                    GUI.Label(new Rect(20f, cY, 150f, 20f), ItemDatabase.Items[i]._name, labelStyle);
                    if(GUI.Button(new Rect(170f, cY, 150f, 20f), "Add"))
                    {
                        LocalPlayer.Inventory.AddItem(ItemDatabase.Items[i]._id, 1, false, false, (WeaponStatUpgrade.Types)(-2), 0f);
                    }
                    this.cY += 30f;
                }
                GUI.EndScrollView();
                
                if(GUI.Button(new Rect(20f, 410f, 100f, 20f), "Close"))
                {
                    this.visible = false;
                }

                GUI.matrix = Matrix;
            }
        }

        private void GenerateList()
        {
            for (int i = 0; i < ItemDatabase.Items.Length; i++)
            {
                ModAPI.Console.Write(string.Concat(new object[]
                {
                    "itemName",
                    ItemDatabase.Items[i]._name,
                    " itemID: ",
                    ItemDatabase.Items[i]._id
                }), "InventoryMod");
            }
        }

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("Start"))
            {
                if (this.visible)
                {
                    TheForest.Utils.LocalPlayer.FpCharacter.UnLockView();
                }
                else
                {
                    TheForest.Utils.LocalPlayer.FpCharacter.LockView();
                }
                this.visible = !this.visible;
            }

        }
    }
}
