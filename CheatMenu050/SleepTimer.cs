using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using TheForest.Tools;
using TheForest.UI;
using UnityEngine;

namespace CheatMenu050
{
    public class SleepTimer : MonoBehaviour
    {
        private TheForestAtmosphere Atmos = null;
        public float SanityPerInGameHourOfSleep = 0.75f;

        [SerializeThis]
        public PlayerStats.SanityData Sanity;

        [SerializeThis]
        public float CurrentSanity = 100f;

        [SerializeThis]
        public PlayerStats.InfectionData FoodPoisoning;

        [SerializeThis]
        public float TimeOfDay;        

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("SleepTimer") && CheatMenuComponent.SleepTimer)
            {
                this.Atmos.NoTimeLapse();
                float num = Scene.Clock.ElapsedGameTime - Scene.Clock.NextSleepTime;
                Scene.Clock.NextSleepTime = Scene.Clock.ElapsedGameTime + 0.95f - num;
                this.Sanity.OnSlept(num * 24f);
                this.FoodPoisoning.Cure();                
            }
        }

        public void OnSlept(float hours)
        {
            this.SanityChange(this.SanityPerInGameHourOfSleep * hours);
        }

        private void SanityChange(float value)
        {
            this.CurrentSanity = Mathf.Clamp(this.CurrentSanity + value, 0f, 100f);
        }
    }
}
