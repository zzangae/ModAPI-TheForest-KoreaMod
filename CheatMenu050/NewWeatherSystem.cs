using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using TheForest.World;
using UnityEngine;

namespace CheatMenu050
{
    public class NewWeatherSystem : WeatherSystem
    {
        protected float ResetCloudTime = 0f;

        protected override void TryRain()
        {
            if (CheatMenuComponent.FreezeWeather)
            {
                return;
            }
            base.TryRain();
        }

        protected override void Update()
        {
            if (this.ResetCloudTime > 0f)
            {
                this.ResetCloudTime -= Time.deltaTime;
                if (this.ResetCloudTime <= 0f)
                {
                    this.CloudSmoothTime = 20f;
                }
            }
            if (CheatMenuComponent.ForceWeather >= 0)
            {
                this.AllOff();
                Scene.RainFollowGO.SetActive(true);
                Scene.RainTypes.SnowConstant.SetActive(false);
                if (CheatMenuComponent.ForceWeather == 1)
                {                    
                    this.TurnOn(WeatherSystem.RainTypes.Light);
                    CloudSmoothTime = 1f;
                }
                if (CheatMenuComponent.ForceWeather == 2)
                {                 
                    this.TurnOn(WeatherSystem.RainTypes.Medium);
                    CloudSmoothTime = 1f;
                }
                if (CheatMenuComponent.ForceWeather == 3)
                {                 
                    this.TurnOn(WeatherSystem.RainTypes.Heavy);
                    CloudSmoothTime = 1f;
                }
                if (CheatMenuComponent.ForceWeather == 4)
                {
                    this.RainDiceStop = 1;
                    this.GrowClouds();
                    this.ReduceClouds();
                    this.CloudOvercastTargetValue = UnityEngine.Random.Range(0.55f, 1f);
                    this.CloudOpacityScaleTargetValue = UnityEngine.Random.Range(1f, 1.2f);
                }
                if (CheatMenuComponent.ForceWeather == 5)
                {
                    this.State = WeatherSystem.States.Raining;
                    Scene.RainTypes.SnowLight.SetActive(true);
                    CloudSmoothTime = 1f;
                }
                if (CheatMenuComponent.ForceWeather == 6)
                {
                    this.State = WeatherSystem.States.Raining;
                    Scene.RainTypes.SnowMedium.SetActive(true);
                    CloudSmoothTime = 1f;
                }
                if (CheatMenuComponent.ForceWeather == 7)
                {
                    this.State = WeatherSystem.States.Raining;
                    Scene.RainTypes.SnowHeavy.SetActive(true);
                    CloudSmoothTime = 1f;
                }
                    CheatMenuComponent.ForceWeather = -1;
            }
            base.Update();
        }
    }
}
