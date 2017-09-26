using ModAPI;
using ModAPI.Attributes;
using System;
using TheForest.Utils;
using UnityEngine;

namespace CheatMenu050
{
    public class CheatMenuComponent : MonoBehaviour
    {
        protected bool visible;

        public static bool GodMode = false;

        public static float SpeedMultiplier = 1f;

        public static float JumpMultiplier = 1f;

        public static bool FlyMode = false;

        public static bool NoClip = false;

        public static float TimeSpeed = 0.13f;

        public static bool InstantTree = false;

        public static bool InstantBuild = false;

        public static bool RemoveBuild = false;

        public static float CaveLight = 0f;

        public static int ForceWeather = -1;

        public static bool FreezeWeather = false;

        public static bool TreeCutEnabled = false;

        public static bool AutoBuild = false;

        public static bool SleepTimer = false;

        protected float a = 2f;

        protected GUIStyle labelStyle;

        protected static bool Already = false;

        protected int Tab;

        public static bool InstaKill = false;

        public static bool Rebreather = false;

        protected static bool FixHealth = false;

        protected static bool FixBatteryCharge = false;

        protected static bool FixFullness = false;

        protected static bool FixStamina = false;

        protected static bool FixEnergy = false;

        protected static bool FixThirst = false;

        protected static bool FixStarvation = false;

        protected static bool FixBodyTemp = false;

        protected static float FixedHealth = -1f;

        protected static float FixedBatteryCharge = -1f;

        protected static float FixedFullness = -1f;

        protected static float FixedStamina = -1f;

        protected static float FixedEnergy = -1f;

        protected static float FixedThirst = -1f;

        protected static float FixedStarvation = -1f;

        protected static float FixedBodyTemp = -1f;

        public static bool FreeCam = false;

        public static bool FreezeTime = false;

        protected GameObject Sphere;

        protected float massTreeRadius = 10f;

        protected bool DestroyTree;

        protected bool LastFreezeTime;

        protected bool LastFreeCam;

        protected float rotationY;

        public static bool removeBuildings = false;

        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            new GameObject("__CheatMenu__").AddComponent<CheatMenuComponent>();
        }

        private void OnGUI()
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
                UnityEngine.GUI.Box(new Rect(10f, 10f, 400f, 430f), "", UnityEngine.GUI.skin.window);
                this.Tab = UnityEngine.GUI.Toolbar(new Rect(10f, 10f, 400f, 30f), this.Tab, new GUIContent[]
                {
                    new GUIContent("Cheats"),
                    new GUIContent("Environment"),
                    new GUIContent("Player"),
                    new GUIContent("Other")
                }, UnityEngine.GUI.skin.GetStyle("Tabs"));
                float num = 50f;
                if (this.Tab == 0)
                {
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "God mode:", this.labelStyle);
                    GodMode = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), GodMode, "");
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Flymode:", this.labelStyle);
                    FlyMode = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), FlyMode, "");
                    num += 30f;
                    if (FlyMode)
                    {
                        UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "No clip:", this.labelStyle);
                        NoClip = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), NoClip, "");
                        num += 30f;
                    }
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "InstaTree:", this.labelStyle);
                    InstantTree = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), InstantTree, "");
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "InstaBuild/Repair:", this.labelStyle);
                    InstantBuild = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), InstantBuild, "");
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "AutoBuild:", this.labelStyle);
                    AutoBuild = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), AutoBuild, "");
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "DestroyBuild:", this.labelStyle);
                    removeBuildings = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), removeBuildings, "");
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "InstaKill:", this.labelStyle);
                    InstaKill = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), InstaKill, "");
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "SleepTimer :", this.labelStyle);
                    SleepTimer = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), SleepTimer, "");
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Enable tree cut key:", this.labelStyle);
                    TreeCutEnabled = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), TreeCutEnabled, "");
                    num += 30f;
                }
                if (this.Tab == 1)
                {
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Speed of time:", this.labelStyle);
                    TheForestAtmosphere.Instance.RotationSpeed = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), TheForestAtmosphere.Instance.RotationSpeed, 0f, 10f);
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(280f, num, 100f, 20f), "Reset"))
                    {
                        TheForestAtmosphere.Instance.RotationSpeed = 0.13f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Time:", this.labelStyle);
                    TheForestAtmosphere.Instance.TimeOfDay = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), TheForestAtmosphere.Instance.TimeOfDay, 0f, 360f);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Cave light:", this.labelStyle);
                    CaveLight = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), CaveLight, 0f, 1f);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Weather", this.labelStyle);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "FreezeWeather:", this.labelStyle);
                    FreezeWeather = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), FreezeWeather, "");
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), "Clear Weather"))
                    {
                        ForceWeather = 0;
                    }
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), "Cloudy"))
                    {
                        ForceWeather = 4;
                    }
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), "Light rain"))
                    {
                        ForceWeather = 1;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), "Light Snow"))
                    {
                        ForceWeather = 5;
                    }
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), "Medium rain"))
                    {
                        ForceWeather = 2;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), "Medium Snow"))
                    {
                        ForceWeather = 6;
                    }
                    num += 30f;
                    if (UnityEngine.GUI.Button(new Rect(20f, num, 180f, 20f), "Heavy rain"))
                    {
                        ForceWeather = 3;
                    }
                    if (UnityEngine.GUI.Button(new Rect(220f, num, 180f, 20f), "Heavy Snow"))
                    {
                        ForceWeather = 7;
                    }
                    num += 30f;
                }
                if (this.Tab == 2)
                {
                    UnityEngine.GUI.Label(new Rect(370f, num, 150f, 20f), "Fix", this.labelStyle);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Health:", this.labelStyle);
                    if (!FixHealth)
                    {
                        LocalPlayer.Stats.Health = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Health, 0f, 100f);
                    }
                    else
                    {
                        FixedHealth = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), FixedHealth, 0f, 100f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Health * 10f) / 10f));
                    FixHealth = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), FixHealth, "");
                    if (FixHealth)
                    {
                        if (FixedHealth == -1f)
                        {
                            FixedHealth = LocalPlayer.Stats.Health;
                        }
                    }
                    else
                    {
                        FixedHealth = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Battery charge:", this.labelStyle);
                    if (!FixBatteryCharge)
                    {
                        LocalPlayer.Stats.BatteryCharge = (float)((int)UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.BatteryCharge, 0f, 100f));
                    }
                    else
                    {
                        FixedBatteryCharge = (float)((int)UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), FixedBatteryCharge, 0f, 100f));
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.BatteryCharge * 10f) / 10f));
                    FixBatteryCharge = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), FixBatteryCharge, "");
                    if (FixBatteryCharge)
                    {
                        if (FixedBatteryCharge == -1f)
                        {
                            FixedBatteryCharge = LocalPlayer.Stats.BatteryCharge;
                        }
                    }
                    else
                    {
                        FixedBatteryCharge = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Fullness:", this.labelStyle);
                    if (!FixFullness)
                    {
                        LocalPlayer.Stats.Fullness = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Fullness, 0f, 1f);
                    }
                    else
                    {
                        FixedFullness = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), FixedFullness, 0f, 1f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Fullness * 10f) / 10f));
                    FixFullness = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), FixFullness, "");
                    if (FixFullness)
                    {
                        if (FixedFullness == -1f)
                        {
                            FixedFullness = LocalPlayer.Stats.Fullness;
                        }
                    }
                    else
                    {
                        FixedFullness = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Stamina:", this.labelStyle);
                    if (!FixStamina)
                    {
                        LocalPlayer.Stats.Stamina = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Stamina, 0f, 100f);
                    }
                    else
                    {
                        FixedStamina = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), FixedStamina, 0f, 100f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Stamina * 10f) / 10f));
                    FixStamina = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), FixStamina, "");
                    if (FixStamina)
                    {
                        if (FixedStamina == -1f)
                        {
                            FixedStamina = LocalPlayer.Stats.Stamina;
                        }
                    }
                    else
                    {
                        FixedStamina = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Energy:", this.labelStyle);
                    if (!FixEnergy)
                    {
                        LocalPlayer.Stats.Energy = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Energy, 0f, 100f);
                    }
                    else
                    {
                        FixedEnergy = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), FixedEnergy, 0f, 100f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Energy * 10f) / 10f));
                    FixEnergy = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), FixEnergy, "");
                    if (FixEnergy)
                    {
                        if (FixedEnergy == -1f)
                        {
                            FixedEnergy = LocalPlayer.Stats.Energy;
                        }
                    }
                    else
                    {
                        FixedEnergy = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Thirst:", this.labelStyle);
                    if (!FixThirst)
                    {
                        LocalPlayer.Stats.Thirst = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Thirst, 0f, 1f);
                    }
                    else
                    {
                        FixedThirst = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), FixedThirst, 0f, 1f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Thirst * 10f) / 10f));
                    FixThirst = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), FixThirst, "");
                    if (FixThirst)
                    {
                        if (FixedThirst == -1f)
                        {
                            FixedThirst = LocalPlayer.Stats.Thirst;
                        }
                    }
                    else
                    {
                        FixedThirst = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Starvation:", this.labelStyle);
                    if (!FixStarvation)
                    {
                        LocalPlayer.Stats.Starvation = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.Starvation, 0f, 1f);
                    }
                    else
                    {
                        FixedStarvation = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), FixedStarvation, 0f, 1f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.Starvation * 10f) / 10f));
                    FixStarvation = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), FixStarvation, "");
                    if (FixStarvation)
                    {
                        if (FixedStarvation == -1f)
                        {
                            FixedStarvation = LocalPlayer.Stats.Starvation;
                        }
                    }
                    else
                    {
                        FixedStarvation = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Body Temp:", this.labelStyle);
                    if (!FixBodyTemp)
                    {
                        LocalPlayer.Stats.BodyTemp = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), LocalPlayer.Stats.BodyTemp, 10f, 60f);
                    }
                    else
                    {
                        FixedBodyTemp = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 160f, 30f), FixedBodyTemp, 10f, 60f);
                    }
                    UnityEngine.GUI.Label(new Rect(340f, num, 40f, 20f), string.Concat((float)Mathf.RoundToInt(LocalPlayer.Stats.BodyTemp * 10f) / 10f));
                    FixBodyTemp = UnityEngine.GUI.Toggle(new Rect(370f, num, 20f, 20f), FixBodyTemp, "");
                    if (FixBodyTemp)
                    {
                        if (FixedBodyTemp == -1f)
                        {
                            FixedBodyTemp = LocalPlayer.Stats.BodyTemp;
                        }
                    }
                    else
                    {
                        FixedBodyTemp = -1f;
                    }
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Speed:", this.labelStyle);
                    SpeedMultiplier = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), SpeedMultiplier, 1f, 10f);
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Jump power:", this.labelStyle);
                    JumpMultiplier = UnityEngine.GUI.HorizontalSlider(new Rect(170f, num + 3f, 210f, 30f), JumpMultiplier, 1f, 10f);
                    num += 30f;
                }
                if (this.Tab == 3)
                {
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Free cam:", this.labelStyle);
                    FreeCam = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), FreeCam, "");
                    num += 30f;
                    UnityEngine.GUI.Label(new Rect(20f, num, 150f, 20f), "Freeze time:", this.labelStyle);
                    FreezeTime = UnityEngine.GUI.Toggle(new Rect(170f, num, 20f, 30f), FreezeTime, "");
                    num += 30f;
                    UnityEngine.GUI.matrix = matrix;
                }
            }
        }

        private void Start()
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Mesh sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
            GameObject gameObject2 = new GameObject();
            gameObject2.name = "Inverted Sphere";
            MeshFilter meshFilter = gameObject2.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();
            Vector3[] vertices = sharedMesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = vertices[i];
            }
            meshFilter.sharedMesh.vertices = vertices;
            int[] triangles = sharedMesh.triangles;
            for (int j = 0; j < triangles.Length; j += 3)
            {
                int num = triangles[j];
                triangles[j] = triangles[j + 2];
                triangles[j + 2] = num;
            }
            meshFilter.sharedMesh.triangles = triangles;
            Vector3[] normals = sharedMesh.normals;
            for (int k = 0; k < normals.Length; k++)
            {
                normals[k] = -normals[k];
            }
            meshFilter.sharedMesh.normals = normals;
            meshFilter.sharedMesh.uv = sharedMesh.uv;
            meshFilter.sharedMesh.uv2 = sharedMesh.uv2;
            meshFilter.sharedMesh.RecalculateBounds();
            DestroyImmediate(gameObject);
            this.Sphere = gameObject2;
            this.Sphere.AddComponent<MeshRenderer>();
            this.Sphere.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Legacy Shaders/Transparent/Diffuse"));
            this.Sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1f, 0f, 0f, 0.9f));
            this.Sphere.GetComponent<MeshRenderer>().enabled = false;
            this.Sphere.GetComponent<Collider>().enabled = false;
        }

        private void Update()
        {
            if (FreezeTime && !this.LastFreezeTime)
            {
                Time.timeScale = 0f;
                this.LastFreezeTime = true;
            }
            if (!FreezeTime && this.LastFreezeTime)
            {
                Time.timeScale = 1f;
                this.LastFreezeTime = false;
            }
            if (FreeCam && !this.LastFreeCam)
            {
                LocalPlayer.CamFollowHead.enabled = false;
                LocalPlayer.CamRotator.enabled = false;
                LocalPlayer.MainRotator.enabled = false;
                LocalPlayer.FpCharacter.enabled = false;
                this.LastFreeCam = true;
            }
            if (!FreeCam && this.LastFreeCam)
            {
                LocalPlayer.CamFollowHead.enabled = true;
                LocalPlayer.CamRotator.enabled = true;
                LocalPlayer.MainRotator.enabled = true;
                LocalPlayer.FpCharacter.enabled = true;
                this.LastFreeCam = false;
            }
            if (FreeCam)
            {
                bool arg_143_0 = TheForest.Utils.Input.GetButton("Crouch");
                bool arg_F3_0 = TheForest.Utils.Input.GetButton("Run");
                bool button = TheForest.Utils.Input.GetButton("Jump");
                float num = 0.1f;
                if (arg_F3_0)
                {
                    num = 2f;
                }
                Vector3 b = Camera.main.transform.rotation * (new Vector3(TheForest.Utils.Input.GetAxis("Horizontal"), 0f, TheForest.Utils.Input.GetAxis("Vertical")) * num);
                if (button)
                {
                    b.y += num;
                }
                if (arg_143_0)
                {
                    b.y -= num;
                }
                Camera.main.transform.position += b;
                float y = Camera.main.transform.localEulerAngles.y + TheForest.Utils.Input.GetAxis("Mouse X") * 15f;
                this.rotationY += TheForest.Utils.Input.GetAxis("Mouse Y") * 15f;
                this.rotationY = Mathf.Clamp(this.rotationY, -80f, 80f);
                Camera.main.transform.localEulerAngles = new Vector3(-this.rotationY, y, 0f);
            }
            if (ModAPI.Input.GetButtonDown("FreezeTime"))
            {
                FreezeTime = !FreezeTime;
            }
            if (ModAPI.Input.GetButtonDown("FreeCam"))
            {
                FreeCam = !FreeCam;
            }
            if (ModAPI.Input.GetButton("MassTree") && TreeCutEnabled)
            {
                if (UnityEngine.Input.mouseScrollDelta != Vector2.zero)
                {
                    this.massTreeRadius = Mathf.Clamp(this.massTreeRadius + UnityEngine.Input.mouseScrollDelta.y, 20f, 100f);
                }
                this.Sphere.GetComponent<MeshRenderer>().enabled = true;
                this.Sphere.transform.position = LocalPlayer.Transform.position;
                this.Sphere.transform.localScale = new Vector3(this.massTreeRadius * 2f, this.massTreeRadius * 2f, this.massTreeRadius * 2f);
                this.DestroyTree = true;
            }
            else
            {
                if (this.DestroyTree)
                {
                    RaycastHit[] array = Physics.SphereCastAll(this.Sphere.transform.position, this.massTreeRadius, new Vector3(1f, 0f, 0f));
                    for (int i = 0; i < array.Length; i++)
                    {
                        RaycastHit raycastHit = array[i];
                        if (raycastHit.collider.GetComponent<TreeHealth>() != null)
                        {
                            raycastHit.collider.gameObject.SendMessage("Explosion", 100f);
                        }
                    }
                    this.DestroyTree = false;
                }
                this.Sphere.GetComponent<MeshRenderer>().enabled = false;
            }
            if (FixBodyTemp)
            {
                LocalPlayer.Stats.BodyTemp = FixedBodyTemp;
            }
            if (FixBatteryCharge)
            {
                LocalPlayer.Stats.BatteryCharge = FixedBatteryCharge;
            }
            if (FixEnergy)
            {
                LocalPlayer.Stats.Energy = FixedEnergy;
            }
            if (FixHealth)
            {
                LocalPlayer.Stats.Health = FixedHealth;
            }
            if (FixStamina)
            {
                LocalPlayer.Stats.Stamina = FixedStamina;
            }
            if (FixFullness)
            {
                LocalPlayer.Stats.Fullness = FixedFullness;
            }
            if (FixStarvation)
            {
                LocalPlayer.Stats.Starvation = FixedStarvation;
            }
            if (FixThirst)
            {
                LocalPlayer.Stats.Thirst = FixedThirst;
            }
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
