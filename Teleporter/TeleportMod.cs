using ModAPI;
using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TheForest.Utils;
using UnityEngine;

namespace Teleporter
{
    internal class TeleportMod : MonoBehaviour
    {
        protected bool visible;

        protected GUIStyle labelStyle;

        public string TPname = "";

        public string Message = "";

        private static Xml xml = new Xml();

        private static string path;

        public Rect TPWindow = new Rect(420f, 0f, 300f, 300f);

        private bool clicked;

        private TeleportMod()
        {
            TeleportMod.path = SaveSlotUtils.GetLocalSlotPath();
        }

        [ExecuteOnGameStart]
        private static void AddToScene()
        {
            new GameObject("__Teleporter__").AddComponent<TeleportMod>();
            if (!System.IO.File.Exists(TeleportMod.path + "tp.xml"))
            {
                TeleportMod.xml.Create(TeleportMod.path);
            }
        }

        private void OnGUI()
        {
            if (!this.visible)
            {
                return;
            }
            UnityEngine.GUI.skin = ModAPI.GUI.Skin;
            Matrix4x4 arg_299_0 = UnityEngine.GUI.matrix;
            if (this.labelStyle == null)
            {
                this.labelStyle = new GUIStyle(UnityEngine.GUI.skin.label);
                this.labelStyle.fontSize = 12;
            }
            UnityEngine.GUI.Box(new Rect(10f, 10f, 400f, 280f), "Teleport menu", UnityEngine.GUI.skin.window);
            float num = 50f;
            UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Location Name", this.labelStyle);
            this.TPname = UnityEngine.GUI.TextField(new Rect(170f, num, 200f, 20f), this.TPname, 25);
            num += 30f;
            float x = LocalPlayer.GameObject.transform.position.x;
            float y = LocalPlayer.GameObject.transform.position.y;
            float z = LocalPlayer.GameObject.transform.position.z;
            if (UnityEngine.GUI.Button(new Rect(20f, num, 100f, 20f), "SAVE"))
            {
                TeleportMod.xml.Update(TeleportMod.path, this.TPname, x, y, z);
                this.Message = "Location '" + this.TPname + "' added!";
                this.TPname = "";
            }
            num += 30f;
            if (UnityEngine.GUI.Button(new Rect(280f, num, 100f, 20f), "Locations"))
            {
                this.clicked = true;
            }
            if (this.clicked)
            {
                this.TPWindow = UnityEngine.GUI.Window(0, this.TPWindow, new UnityEngine.GUI.WindowFunction(this.FillTPWindow), "TP Locations");
            }
            num += 30f;
            UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "x-" + x, this.labelStyle);
            num += 20f;
            UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "y-" + y, this.labelStyle);
            num += 20f;
            UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "z-" + z, this.labelStyle);
            num += 30f;
            UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), this.Message, this.labelStyle);
            UnityEngine.GUI.matrix = arg_299_0;
        }

        private void FillTPWindow(int windowID)
        {
            float num = 30f;
            foreach (BoltEntity current in BoltNetwork.entities)
            {
                if (current.StateIs<IPlayerState>())
                {
                    string name = current.GetState<IPlayerState>().name;
                    float x = current.GetState<IPlayerState>().Transform.Position.x;
                    float y = current.GetState<IPlayerState>().Transform.Position.y;
                    float z = current.GetState<IPlayerState>().Transform.Position.z;
                    if (UnityEngine.GUI.Button(new Rect(120f, num, 150f, 20f), name))
                    {
                        LocalPlayer.GameObject.transform.localPosition = new Vector3(x, y, z);
                    }
                    num += 30f;
                }
            }
            new System.Collections.Generic.List<Location>();
            foreach (Location current2 in TeleportMod.xml.Read(TeleportMod.path))
            {
                UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), current2.GetName(), this.labelStyle);
                if (UnityEngine.GUI.Button(new Rect(120f, num, 80f, 20f), "Teleport"))
                {
                    LocalPlayer.GameObject.transform.localPosition = new Vector3(current2.GetX(), current2.GetY(), current2.GetZ());
                }
                if (UnityEngine.GUI.Button(new Rect(205f, num, 80f, 20f), "Remove"))
                {
                    TeleportMod.xml.Delete(TeleportMod.path, current2);
                }
                num += 30f;
            }
            if (UnityEngine.GUI.Button(new Rect(20f, num, 100f, 20f), "Close"))
            {
                this.clicked = false;
            }
        }

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("TeleportMe"))
            {
                if (this.visible)
                {
                    this.Message = "";
                    LocalPlayer.FpCharacter.UnLockView();
                }
                else
                {
                    LocalPlayer.FpCharacter.LockView(true);
                }
                this.visible = !this.visible;
                if (this.clicked)
                {
                    this.clicked = false;
                }
            }
        }
    }
}
