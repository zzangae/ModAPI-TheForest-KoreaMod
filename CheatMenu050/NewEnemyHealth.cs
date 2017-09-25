using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheatMenu050
{
    internal class NewEnemyHealth : EnemyHealth
    {
        public override void Hit(int damage)
        {
            if (CheatMenuComponent.InstaKill)
            {
                base.Die();
            }
            else
            {
                base.HitReal(damage);
            }
        }
    }
}
