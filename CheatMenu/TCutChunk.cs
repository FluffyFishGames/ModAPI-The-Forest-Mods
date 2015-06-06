using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheatMenu
{
    class TCutChunk : TreeCutChunk
    {
        protected override void Hit(float amount)
        {
            if (CheatMenuComponent.InstantTree)
            {
                this.MyTree.SendMessage("CheatMenuCutDown");
            }
            else
            {
                base.Hit(amount);
            }
        }
    }
}
