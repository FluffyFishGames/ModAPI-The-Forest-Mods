using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheatMenu
{
    class Atmosphere : TheForestAtmosphere
    {
        protected override void Update()
        {
            if (CheatMenuComponent.CaveLight > 0f && InACave)
            {
                base.Update();
                RenderSettings.ambientLight = new Color(CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight);
                RenderSettings.ambientEquatorColor = new Color(CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight);
                RenderSettings.ambientGroundColor = new Color(CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight);
            }
            else
            {
                base.Update();
            }
        }
    }
}
