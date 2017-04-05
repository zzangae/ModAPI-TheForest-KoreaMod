using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NoFog
{
    internal class main : TheForestAtmosphere
    {
        [Priority(200)]
        protected override void Update()
        {
            base.Update();
            this.FogColor = new Color(0f, 0f, 0f, 0f);
            if (Sunshine.Instance.Ready)
            {
                Sunshine.Instance.ScatterColor = this.FogColor;
            }
            if (this.InACave)
            {
                this.FogColor = new Color(0f, 0f, 0f, 0f);
            }
            this.CurrentFogColor = this.FogColor;
            Shader.SetGlobalColor("_FogColor", this.FogColor);
            this.ChangeFogAmount();
        }

        [Priority(200)]
        protected override void ChangeFogAmount()
        {
            this.FogCurrent = 300000f;
            TheForestAtmosphere.Instance.FogCurrent = 300000f;
        }
    }
}
