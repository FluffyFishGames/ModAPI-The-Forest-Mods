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
                CaveAddLight1 = new Color(CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight);
                CaveAddLight2 = new Color(CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight, CheatMenuComponent.CaveLight); ;
                CaveAddLight1Intensity = CheatMenuComponent.CaveLight;
                CaveAddLight2Intensity = CheatMenuComponent.CaveLight;
                base.Update();
            }
            else
            {
                base.Update();
            }
        }
    }
}
