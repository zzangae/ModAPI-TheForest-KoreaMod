using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Tools;
using TheForest.Utils;
using UnityEngine;

namespace SleepTimer
{
    internal class SleepTimer : PlayerStats
    {
        protected override void Update()
        {
            if (ModAPI.Input.GetButtonDown("SleepTimer"))
            {
                if (!BoltNetwork.isClient && Scene.SceneTracker.allPlayers.Count >= 1)
                {
                    Scene.MutantSpawnManager.offsetSleepAmounts();
                    Scene.MutantControler.startSetupFamilies();
                    EventRegistry.Player.Publish(TfEvent.Slept, null);
                    base.NextSleepTime = Scene.Clock.ElapsedGameTime;
                    this.Invoke("TurnOffSleepCam", 3f);
                    this.Tired = 0f;
                    this.Atmos.TimeLapse();
                    Scene.HudGui.GuiCam.SetActive(false);
                    Scene.Cams.SleepCam.SetActive(true);
                    this.Energy += 100f;
                    return;
                }
                base.Wake();
            }
            base.Update();
        }
    }
}
