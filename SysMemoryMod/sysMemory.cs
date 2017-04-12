using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ModAPI;
using TheForest.Utils;
using ModAPI.Attributes;

namespace SysMemoryMod
{
    public class sysMemory : MonoBehaviour
    {
        public bool ShowWindow;
        private float ShowFPS = 60f;
        public GUIStyle LayoutStyle;
        public GUIStyle txtStyle;

        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            new GameObject("__SystemMenu__").AddComponent<sysMemory>();
        }

        private void Awake()
        {
            this.LayoutStyle = new GUIStyle();
            this.LayoutStyle.normal.background = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            this.LayoutStyle.normal.background.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.25f));
            this.LayoutStyle.normal.background.Apply();
            this.LayoutStyle.normal.background.wrapMode = TextureWrapMode.Repeat;
            this.LayoutStyle.margin = new RectOffset(0, 0, 2, 0);
            this.LayoutStyle.padding = new RectOffset(3, 1, 1, 1);
            this.LayoutStyle.normal.textColor = Color.white;
            this.LayoutStyle.fixedHeight = 12f;
            this.LayoutStyle.fontSize = 12;
            this.txtStyle = new GUIStyle();
            this.txtStyle.normal.textColor = Color.white;
            this.txtStyle.fontSize = 12;
            this.txtStyle.margin = new RectOffset(0, 0, 0, 0);
            this.txtStyle.padding = new RectOffset(0, 0, 0, 0);
            UnityEngine.Object.DontDestroyOnLoad(this);
        }

        private void OnGUI()
        {
            Color color = UnityEngine.GUI.color;

            if (this.ShowWindow)
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[]
                {
                    GUILayout.Width((float)Screen.width),
                    GUILayout.Height(24f)
                });                
                GUILayout.FlexibleSpace();
                GUILayout.Label("FPS: " + (int)this.ShowFPS, UnityEngine.GUI.skin.button, new GUILayoutOption[0]);

                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.Label("Total Alloc: " + Profiler.GetTotalAllocatedMemory() / 1000u / 1000u + "MB", UnityEngine.GUI.skin.button, new GUILayoutOption[]
                {
                    GUILayout.MinWidth(140f)
                });
                GUILayout.Label("Total Reserved: " + Profiler.GetTotalReservedMemory() / 1000u / 1000u + "MB", UnityEngine.GUI.skin.button, new GUILayoutOption[]
                {
                    GUILayout.MinWidth(140f)
                });
                GUILayout.Label("Total Memory: " + GC.GetTotalMemory(false) / 1000L / 1000L + "MB", UnityEngine.GUI.skin.button, new GUILayoutOption[]
                {
                    GUILayout.MinWidth(100f)
                });
                GUILayout.EndVertical();

                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.Label((!Clock.InCave) ? "Not in cave" : "In cave", UnityEngine.GUI.skin.button, new GUILayoutOption[]
                {
                    GUILayout.Width(100f)
                });
                GUILayout.EndVertical();

                GUILayout.BeginVertical(new GUILayoutOption[0]);
                if (LocalPlayer.Inventory)
                {
                    GUILayout.Label(string.Concat(new object[]
                    {
                        "Local position",
                        "\nx: ",
                        LocalPlayer.Transform.position.x,
                        "\ny: ",
                        LocalPlayer.Transform.position.y,
                        "\nz: ",
                        LocalPlayer.Transform.position.z
                    }), UnityEngine.GUI.skin.button, new GUILayoutOption[]
                    {
                        GUILayout.Width(150f)
                    });
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.Label(string.Concat(new object[]
                {
                    "current time: ",
                    (int)FMOD_StudioEventEmitter.HoursSinceMidnight,
                    "h",
                    (int)((FMOD_StudioEventEmitter.HoursSinceMidnight - (float)((int)FMOD_StudioEventEmitter.HoursSinceMidnight)) * 60f),
                    (!Clock.Dark) ? " (d)" : " (n)"
                }), UnityEngine.GUI.skin.button, new GUILayoutOption[]
                {
                    GUILayout.Width(150f)
                });
                GUILayout.EndVertical();

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }

        private void Update()
        {
            this.ShowFPS = Mathf.Lerp(this.ShowFPS, 1f / Time.deltaTime, 0.05f);
            if (float.IsNaN(this.ShowFPS) || this.ShowFPS == 0f)
            {
                this.ShowFPS = 1f;
            }
            
            if (ModAPI.Input.GetButtonDown("open"))
            {
                if (this.ShowWindow)
                {
                    LocalPlayer.FpCharacter.UnLockView();
                }
                else
                {
                    LocalPlayer.FpCharacter.LockView(true);
                }
                this.ShowWindow = !this.ShowWindow;
            }
        }

        
    }
}
