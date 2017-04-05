using ModAPI;
using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using UnityEngine;

namespace Removebuildings
{
    public class RemoveBuildingsBehaviour : MonoBehaviour
    {
        protected System.Collections.Generic.List<string> buildingNames;

        protected GameObject removeClone;

        protected UITexture removeCloneTexture;

        protected bool ShowRemoveIcon;

        protected UILabel label;

        protected bool Initialized;

        protected Camera camera;

        [ExecuteOnGameStart]
        public static void Init()
        {
            new GameObject("__RemoveBuildings__").AddComponent<RemoveBuildingsBehaviour>();
        }

        private void Initialize()
        {
            this.Initialized = true;
            this.buildingNames = new System.Collections.Generic.List<string>
        {
            "Ex_EffigyBuilt",
            "Trap_SpikeWall",
            "Trap_Deadfall",
            "Trap_TripWire_Explosive",
            "Trap_Rabbit",
            "Ex_RockFenceChunkBuilt",
            "Bed_Built",
            "WalkwayStraightBuilt",
            "Ex_RoofBuilt",
            "TreesapCollectorBuilt",
            "Target_Built",
            "Ex_FloorBuilt",
            "Ex_FoundationBuilt",
            "Ex_StairsBuilt",
            "Ex_PlatformBuilt",
            "Ex_WallDefensiveChunkBuilt",
            "Ex_WallChunkBuilt",
            "Ex_StickFenceChunkBuilt",
            "FireBuilt",
            "FireStandBuilt",
            "BonFireBuilt",
            "FireBuiltRockPit",
            "LeafHutBuilt",
            "ShelterBuilt",
            "LogCabinBuilt",
            "LogCabin_Small_Built",
            "TreeHouse_Built_MP",
            "TreeHouseChalet_Built_MP",
            "Stick_HolderBuilt",
            "LogHolderBuilt",
            "MultiSledBuilt",
            "rock_HolderBuilt",
            "WeaponRack",
            "HolderExplosives_Built",
            "MedicineCabinet_Built",
            "HolderSnacks_Built",
            "WallBuilt",
            "WallBuiltDefensive",
            "WallBuilt_Doorway",
            "WallBuilt_Window",
            "StairCaseBuilt",
            "TreePlatform_Built",
            "PlatformBridgeBuilt",
            "SpikeDefenseBuilt",
            "FoundationBuilt",
            "FloorBuilt",
            "WallExBuilt",
            "StickMarkerBuilt",
            "RopeBuilt",
            "WalkwayStraightBuilt",
            "WorkBenchBuilt",
            "GazeboBuilt",
            "Trap_Rabbit",
            "Trap_TripWire_Explosive",
            "Trap_Deadfall",
            "Trap_SpikeWall",
            "Trap_RopeBuilt",
            "RabbitCageBuilt",
            "GardenBuilt",
            "DryingRackBuilt",
            "WaterCollector_Built",
            "RaftBuilt",
            "HouseBoat_Small",
            "EffigyHead",
            "EffigyBigBuilt",
            "EffigySmallBuilt",
            "EffigyRainBuilt",
            "PlatformExBuilt"
        };
            if (Scene.HudGui != null && Scene.HudGui.DestroyIcon != null && Scene.HudGui.DestroyIcon.gameObject != null)
            {
                GameObject gameObject = null;
                for (int i = 0; i < Scene.HudGui.PauseMenu.transform.childCount; i++)
                {
                    Transform child = Scene.HudGui.PauseMenu.transform.GetChild(i);
                    if (child.name == "Panel - Main")
                    {
                        gameObject = child.gameObject;
                        break;
                    }
                }
                Transform child2 = gameObject.transform.GetChild(0);
                GameObject gameObject2 = null;
                for (int j = 0; j < child2.childCount; j++)
                {
                    Transform child3 = child2.GetChild(j);
                    if (child3.name == "Button - Continue")
                    {
                        gameObject2 = child3.gameObject;
                        break;
                    }
                }
                this.removeClone = NGUITools.AddChild(Scene.HudGui.DestroyIcon.transform.parent.gameObject, Scene.HudGui.DestroyIcon.gameObject);
                UnityEngine.Object.Destroy(this.removeClone.transform.GetChild(0).gameObject);
                this.removeClone.transform.localPosition = Scene.HudGui.DestroyIcon.transform.localPosition;
                this.removeCloneTexture = this.removeClone.GetComponent<UITexture>();
                this.removeCloneTexture.alpha = 1f;
                this.removeCloneTexture.mainTexture = Resources.GetTexture("RemoveBuilding.png", "RemoveBuildings");
                GameObject expr_4A6 = NGUITools.AddChild(this.removeClone, gameObject2.transform.GetChild(0).gameObject);
                expr_4A6.GetComponent<UILabel>().text = ModAPI.Input.GetKeyBindingAsString("RemoveBuilding", "RemoveBuildings");
                Transform transform = expr_4A6.transform;
                transform.localPosition += new Vector3(0f, -70f, 0f);
            }
        }

        private void Update()
        {
            if (!this.Initialized)
            {
                this.Initialize();
            }
            if (this.camera == null)
            {
                this.camera = LocalPlayer.MainCam;
            }
            if (this.camera != null)
            {
                try
                {
                    RaycastHit[] expr_86 = Physics.RaycastAll(new Ray(this.camera.transform.position + this.camera.transform.forward * 1f, this.camera.transform.forward), 5f);
                    if (expr_86.Length == 0)
                    {
                        this.removeCloneTexture.gameObject.SetActive(false);
                    }
                    RaycastHit[] array = expr_86;
                    int i = 0;
                    while (i < array.Length)
                    {
                        RaycastHit raycastHit = array[i];
                        Transform transform = raycastHit.collider.transform;
                        bool flag = false;
                        while (!flag && !(transform == null))
                        {
                            foreach (string current in this.buildingNames)
                            {
                                if (transform.name.StartsWith(current))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                break;
                            }
                            transform = transform.parent;
                        }
                        if (flag)
                        {
                            this.ShowRemoveIcon = true;
                            this.removeCloneTexture.gameObject.SetActive(true);
                            if (ModAPI.Input.GetButtonDown("RemoveBuilding", "RemoveBuildings"))
                            {
                                UnityEngine.Object.Destroy(transform.gameObject);
                                break;
                            }
                            break;
                        }
                        else
                        {
                            this.ShowRemoveIcon = false;
                            this.removeCloneTexture.gameObject.SetActive(false);
                            i++;
                        }
                    }
                }
                catch (System.Exception arg_17B_0)
                {
                    Log.Write(arg_17B_0.ToString(), "RemoveBuildings");
                }
            }
        }
    }
}