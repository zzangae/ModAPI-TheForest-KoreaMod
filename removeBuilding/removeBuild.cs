using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ModAPI;
using ModAPI.Attributes;
using TheForest.Utils;

namespace removeBuilding
{
    public class removeBuild : MonoBehaviour
    {
        protected bool visible;
        protected GUIStyle labelStyle;
        public Vector2 scrollPosition = Vector2.zero;
        private float cY;
        public static bool removeBuildings = false;

        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            new GameObject("__removeBuild__").AddComponent<removeBuild>();
        }

        void OnGUI()
        {
            if (this.visible)
            {
                UnityEngine.GUI.skin = ModAPI.Gui.Skin;
                Matrix4x4 matrix = UnityEngine.GUI.matrix;

                if (this.labelStyle == null)
                {
                    this.labelStyle = new GUIStyle(UnityEngine.GUI.skin.label);
                    this.labelStyle.fontSize = 12;
                }

                UnityEngine.GUI.Box(new Rect(20f, 20f, 400f, 450f), "Menu", UnityEngine.GUI.skin.window);
                this.scrollPosition = UnityEngine.GUI.BeginScrollView(new Rect(10f, 50f, 390f, 350f), this.scrollPosition, new Rect(0f, 0f, 350f, this.cY));
                this.cY = 25f;

                UnityEngine.GUI.Label(new Rect(20f, this.cY, 150f, 20f), "destroy building : ", this.labelStyle);
                removeBuild.removeBuildings = UnityEngine.GUI.Toggle(new Rect(170f, this.cY, 20f, 30f), removeBuild.removeBuildings, "");

                UnityEngine.GUI.EndScrollView();
                if (UnityEngine.GUI.Button(new Rect(170f, 410f, 100f, 20f), "Close"))
                {
                    this.visible = false;
                }
                UnityEngine.GUI.matrix = matrix;
            }
        }


        void Update()
        {
            if (ModAPI.Input.GetButtonDown("OpenMenu"))
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
