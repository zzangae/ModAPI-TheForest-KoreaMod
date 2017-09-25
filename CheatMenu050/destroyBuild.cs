using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.World;

namespace CheatMenu050
{
    internal class destroyBuild
    {
        public static LocalizedHitData GetLocalizedHitData(LocalizedHitData data)
        {
            if (!CheatMenuComponent.removeBuildings)
            {
                return data;
            }
            return new LocalizedHitData(data._position, 100000f);
        }
    }
}
