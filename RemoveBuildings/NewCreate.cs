using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RemoveBuildings
{
    class NewCreate : TheForest.Buildings.Creation.Craft_Structure
    {
        protected override void Update()
        {
            if (!this._initialized)
                return;

            TheForest.Utils.Scene.HudGui.DestroyIcon.SetActive(false);
            base.Update();
            if (Input.GetButtonDown("Craft"))
            {
                this.CancelBlueprint();
                return;
            }
        }
    }
}