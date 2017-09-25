using FMOD.Studio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using UnityEngine;

namespace CheatMenu050
{
    class Stats : PlayerStats
    {
        [ModAPI.Attributes.Priority(1000)]
        protected override void hitFallDown()
        {
            if (!CheatMenuComponent.GodMode)
                base.hitFallDown();
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void HitFire()
        {
            if (!CheatMenuComponent.GodMode)
                base.HitFire();
        }

        [ModAPI.Attributes.Priority(1000)]
        public override void hitFromEnemy(int getDamage)
        {
            if (!CheatMenuComponent.GodMode)
                base.hitFromEnemy(getDamage);
        }       

        [ModAPI.Attributes.Priority(1000)]
        public override void HitShark(int damage)
        {
            if (!CheatMenuComponent.GodMode)
                base.HitShark(damage);
        }
        
        [ModAPI.Attributes.Priority(1000)]
        protected override void FallDownDead()
        {
            if (!CheatMenuComponent.GodMode)
                base.FallDownDead();
        }        

        [ModAPI.Attributes.Priority(1000)]
        protected override void HitFromPlayMaker(int damage)
        {
            if (!CheatMenuComponent.GodMode)
                base.HitFromPlayMaker(damage);
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void Fell()
        {
            if (!CheatMenuComponent.GodMode)
                base.Fell();
        }
        
        [ModAPI.Attributes.Priority(1000)]
        protected override void KnockOut()
        {
            if (!CheatMenuComponent.GodMode)
                base.KnockOut();
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void Bleed()
        {
            if (!CheatMenuComponent.GodMode)
                base.Bleed();
        }

        [ModAPI.Attributes.Priority(1000)]
        public override void Poison()
        {
            if (!CheatMenuComponent.GodMode)
                base.Poison();
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void HitPoison()
        {
            if (!CheatMenuComponent.GodMode)
                base.HitPoison();
        }

        [ModAPI.Attributes.Priority(1000)]
        public override void Hit(int damage, bool ignoreArmor, PlayerStats.DamageType type)
        {
            if (!CheatMenuComponent.GodMode)
                base.Hit(damage, ignoreArmor, type);
        }

        protected override void Update()
        {
            if (CheatMenuComponent.GodMode)
            {                
                this.IsBloody = false;
                this.FireWarmth = true;
                this.SunWarmth = true;
                this.IsCold = false;
                this.Health = 100f;
                this.Armor = 100;
                this.Fullness = 1f;
                this.Stamina = 100f;
                this.Energy = 100f;
                this.Hunger = 0;
                this.Thirst = 0f;
                this.Starvation = 0;                              
            }
            
            if (CheatMenuComponent.Rebreather && this.AirBreathing.CurrentLungAirTimer.IsRunning && LocalPlayer.WaterViz.ScreenCoverage > this.AirBreathing.ScreenCoverageThreshold && !this.Dead)
            {
                if (!Scene.HudGui.AirReserve.gameObject.activeSelf)
                {
                    Scene.HudGui.AirReserve.gameObject.SetActive(true);
                }
                if (!this.AirBreathing.UseRebreather && this.AirBreathing.RebreatherIsEquipped && this.AirBreathing.CurrentRebreatherAir > 0f)
                {
                    this.AirBreathing.UseRebreather = true;
                }
                if (this.AirBreathing.UseRebreather)
                {                    
                    this.AirBreathing.CurrentRebreatherAir -= Time.deltaTime;
                    Scene.HudGui.AirReserve.fillAmount = this.AirBreathing.CurrentRebreatherAir / this.AirBreathing.MaxRebreatherAirCapacity;
                    if (this.AirBreathing.CurrentRebreatherAir < 0f)
                    {
                        this.AirBreathing.CurrentLungAir = 0f;
                        this.AirBreathing.UseRebreather = false;
                    }
                    else if (this.AirBreathing.CurrentRebreatherAir < this.AirBreathing.OutOfAirWarningThreshold)
                    {
                        if (!Scene.HudGui.AirReserveOutline.activeSelf)
                        {
                            Scene.HudGui.AirReserveOutline.SetActive(true);
                        }
                    }
                    else if (Scene.HudGui.AirReserveOutline.activeSelf)
                    {
                        Scene.HudGui.AirReserveOutline.SetActive(false);
                    }
                }
                FMOD_StudioSystem.instance.PlayOneShot(this.GaspForAirEvent, base.transform.position, delegate (FMOD.Studio.EventInstance instance)
                {
                    float value = 85f;
                    value = (this.AirBreathing.CurrentLungAir - (float)this.AirBreathing.CurrentLungAirTimer.Elapsed.TotalSeconds) / this.AirBreathing.MaxLungAirCapacity * 100f;                    
                    return true;
                });
                this.AirBreathing.CurrentLungAirTimer.Stop();
                this.AirBreathing.CurrentLungAirTimer.Reset();
                this.AirBreathing.CurrentLungAir = this.AirBreathing.MaxLungAirCapacity;
                Scene.HudGui.AirReserve.gameObject.SetActive(false);
                Scene.HudGui.AirReserveOutline.SetActive(false);
            }
            else
            {                
                this.AirBreathing.CurrentRebreatherAir -= Time.deltaTime;              
                this.AirBreathing.CurrentLungAirTimer.Start();
            }            
            base.Update();
        }

        protected override void KillPlayer()
        {
            if (!CheatMenuComponent.GodMode)
                base.KillPlayer();
        }

        public override void HitFood()
        {
            if (!CheatMenuComponent.GodMode)
                base.HitFood();
        }

        public override void HitFoodDelayed(int damage)
        {
            if (!CheatMenuComponent.GodMode)
                base.HitFoodDelayed(damage);
        }
    }
}
