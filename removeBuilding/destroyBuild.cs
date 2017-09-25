using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.World;

namespace removeBuilding
{
    internal class destroyBuild
    {
        public static LocalizedHitData GetLocalizedHitData(LocalizedHitData data)
        {
            if (!removeBuild.removeBuildings)
            {
                return data;
            }
            return new LocalizedHitData(data._position, 100000f);
        }
    }
}
