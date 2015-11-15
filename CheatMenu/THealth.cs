using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheatMenu
{
    class THealth : TreeHealth
    {
        protected override void Hit()
        {
            if (CheatMenuComponent.InstantTree)
            {
                this.Explosion(100f);
            }
            else
            {
                base.Hit();
            }
        }
    }
}