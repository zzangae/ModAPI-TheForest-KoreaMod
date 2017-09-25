using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheatMenu050
{
    internal class Atmosphere : TheForestAtmosphere
    {
        protected override void Update()
        {            
            if (CheatMenuComponent.CaveLight > 0f)
            {
                this.CaveAddLight1 = new Color(CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight);
                this.CaveAddLight2 = new Color(CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight);
                this.CaveAddLight1Intensity = CheatMenuComponent.CaveLight;
                this.CaveAddLight2Intensity = CheatMenuComponent.CaveLight;
                base.Update();
                return;
            }
            base.Update();
        }
    }
}
