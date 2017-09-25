using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Buildings.World;
using TheForest.World;

namespace CheatMenu050
{
    internal class BuildingHealthWorld : BuildingHealth
    {
        public override void LocalizedHit(LocalizedHitData data)
        {
            base.LocalizedHit(destroyBuild.GetLocalizedHitData(data));
        }
    }
}
