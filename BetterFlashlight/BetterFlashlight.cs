using Bolt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items.World;
using TheForest.Utils;
using UnityEngine;

namespace BetterFlashlight
{
    public class BetterTorch : BatteryBasedLight
    {
        protected override void Update()
        {
            if (BoltNetwork.isRunning && (!BoltNetwork.isRunning || !base.entity.isAttached || !base.entity.isOwner))
            {
                return;
            }            
            LocalPlayer.Stats.BatteryCharge -= this._batterieCostPerSecond * Time.deltaTime;
            if ((double)LocalPlayer.Stats.BatteryCharge > 20.0)
            {
                this.SetIntensity(0.85f);
            }
            else if ((double)LocalPlayer.Stats.BatteryCharge < 20.0)
            {
                if ((double)LocalPlayer.Stats.BatteryCharge < 10.0)
                {
                    if ((double)LocalPlayer.Stats.BatteryCharge < 5.0)
                    {
                        if ((double)LocalPlayer.Stats.BatteryCharge <= 0.0)
                        {
                            this._player.StashLeftHand();
                        }
                        else
                        {
                            this.TorchLowerLightEvenMore();
                        }
                    }
                    else
                    {
                        this.TorchLowerLightMore();
                    }
                }
                else
                {
                    this.TorchLowerLight();
                }
            }
            if (!BoltNetwork.isRunning)
            {
                return;
            }
            base.state.BatteryTorchIntensity = this._mainLight.intensity;
            base.state.BatteryTorchEnabled = this._mainLight.enabled;
            base.state.BatteryTorchColor = this._mainLight.color;
        }

        protected override void TorchLowerLight()
        {
            this.SetIntensity(UnityEngine.Random.Range(0.6f, 0.45f));
        }

        protected override void TorchLowerLightMore()
        {
            this.SetIntensity(UnityEngine.Random.Range(0.4f, 0.2f));
        }

        protected override void TorchLowerLightEvenMore()
        {
            this.SetIntensity(UnityEngine.Random.Range(0.2f, 0.03f));
        }
    }
}
