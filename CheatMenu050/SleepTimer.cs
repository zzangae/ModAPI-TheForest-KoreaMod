using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using TheForest.Tools;
using TheForest.UI;
using UnityEngine;
using Pathfinding;

namespace CheatMenu050
{
    internal class SleepTimer : PlayerStats
    {
        protected override void Update()
        {
            if (ModAPI.Input.GetButtonDown("SleepTimer") && CheatMenuComponent.SleepTimer)
            {
                if (!BoltNetwork.isClient || SteamClientDSConfig.isDSFirstClient)
                {
                    if (Scene.SceneTracker.allPlayers.Count >= 1)
                    {
                        Transform transform2 = this.mutantControl.findClosestEnemy(transform);
                        if (transform2 && !LocalPlayer.IsInCaves && (LocalPlayer.ScriptSetup.targetFunctions.visibleEnemies.Count > 0 || Vector3.Distance(transform.position, transform2.transform.position) < 65f))
                        {
                            GraphNode node = AstarPath.active.GetNearest(transform2.transform.position, NNConstraint.Default).node;
                            uint area = node.Area;
                            NNConstraint nNConstraint = new NNConstraint();
                            nNConstraint.constrainArea = true;
                            int area2 = (int)area;
                            nNConstraint.area = area2;
                            GraphNode node2 = AstarPath.active.GetNearest(transform.position, nNConstraint).node;
                            Vector3 a = new Vector3((float)(node2.position[0] / 1000), (float)(node2.position[1] / 1000), (float)(node2.position[2] / 1000));
                            if (Vector3.Distance(a, LocalPlayer.Transform.position) < 6f)
                            {
                                base.StartCoroutine("setupSleepEncounter", transform2.gameObject);
                                this.GoToSleepFake();
                                return;
                            }
                        }
                    }
                    Scene.MutantSpawnManager.offsetSleepAmounts();
                    Scene.MutantControler.startSetupFamilies();
                    EventRegistry.Player.Publish(TfEvent.Slept, null);
                }
                this.Invoke("TurnOffSleepCam", 3f);
                this.Tired = 0f;
                this.Atmos.TimeLapse();
                Scene.HudGui.ToggleAllHud(false);
                Scene.Cams.SleepCam.SetActive(true);
                this.Energy += 100f;
                return;
            }
            base.Update();
        }
    }
}
