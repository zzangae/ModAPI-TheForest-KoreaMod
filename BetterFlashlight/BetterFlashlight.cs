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
            if (BoltNetwork.get_isRunning() && (!BoltNetwork.get_isRunning() || !base.get_entity().get_isAttached() || !base.get_entity().get_isOwner()))
            {
                return;
            }
            LocalPlayer.Stats.BatteryCharge -= this._batterieCostPerSecond * Time.get_deltaTime();
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
            if (!BoltNetwork.get_isRunning())
            {
                return;
            }
            base.get_state().set_BatteryTorchIntensity(this._mainLight.get_intensity());
            base.get_state().set_BatteryTorchEnabled(this._mainLight.get_enabled());
            base.get_state().set_BatteryTorchColor(this._mainLight.get_color());
        }

        protected override void TorchLowerLight()
        {
            this.SetIntensity(Random.Range(0.6f, 0.45f));
        }

        protected override void TorchLowerLightMore()
        {
            this.SetIntensity(Random.Range(0.4f, 0.2f));
        }

        protected override void TorchLowerLightEvenMore()
        {
            this.SetIntensity(Random.Range(0.2f, 0.03f));
        }
    }
}
