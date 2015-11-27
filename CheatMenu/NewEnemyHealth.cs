using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheatMenu
{
    class NewEnemyHealth : EnemyHealth
    {
        protected override void Hit(int damage)
        {
            if (CheatMenuComponent.InstaKill)
                base.Hit(damage * 1000000);
            else
                base.Hit(damage);
        }
    }
}
