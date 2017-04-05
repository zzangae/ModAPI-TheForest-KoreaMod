using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.UI;
using TheForest.Utils;

namespace pewf_sleepez
{
    internal class SleepEZ : SleepTimerDisplay
    {
        protected override void Update()
        {
            if (LocalPlayer.Stats)
            {
                LocalPlayer.Stats.NextSleepTime = 0f;
                this._fillSprite.fillAmount = 1f;
            }
        }
    }
}
