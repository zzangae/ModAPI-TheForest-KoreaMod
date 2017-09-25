using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheatMenu050
{
    internal class THealth : TreeHealth
    {
        protected override void Hit()
        {
            if (CheatMenuComponent.InstantTree)
            {
                this.Explosion(100f);
                return;
            }
            base.Hit();
        }
    }
}
