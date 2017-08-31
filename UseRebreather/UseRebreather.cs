using FMOD.Studio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TheForest.Graphics;
using TheForest.Utils;
using UnityEngine;

namespace UseRebreather
{
    class URebreather : PlayerStats
    {   
        protected override void Update()
        {            
            if (ModAPI.Input.GetButtonDown("use") && FMOD_StudioSystem.instance && this.AirBreathing.CurrentLungAirTimer.IsRunning)
            {
                this.AirBreathing.RebreatherIsEquipped = true;
                this.AirBreathing.UseRebreather = true;

                if (this.AirBreathing.UseRebreather)
                {
                    this.AirBreathing.CurrentRebreatherAir -= Time.deltaTime;
                    Scene.HudGui.AirReserve.fillAmount = this.AirBreathing.CurrentRebreatherAir / this.AirBreathing.MaxRebreatherAirCapacity;
                    Scene.HudGui.AirReserveOutline.SetActive(true);
                    FMOD_StudioSystem.instance.PlayOneShot(this.GaspForAirEvent, base.transform.position, delegate (FMOD.Studio.EventInstance instance)
                    {
                        float value = 85f;
                        if (!this.AirBreathing.UseRebreather)
                        {
                            value = (this.AirBreathing.CurrentLungAir - (float)this.AirBreathing.CurrentLungAirTimer.Elapsed.TotalSeconds) / this.AirBreathing.MaxLungAirCapacity * 100f;
                        }
                        return true;
                    });
                }
                this.AirBreathing.CurrentLungAirTimer.Stop();
                this.AirBreathing.CurrentLungAirTimer.Reset();
                this.AirBreathing.CurrentLungAir = this.AirBreathing.MaxLungAirCapacity;
                Scene.HudGui.AirReserve.gameObject.SetActive(false);
                Scene.HudGui.AirReserveOutline.SetActive(false);
            }
            base.Update();
        }
    }            
}
    

