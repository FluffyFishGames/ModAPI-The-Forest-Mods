using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheatMenu
{
    class CStructure : TheForest.Buildings.Creation.Craft_Structure
    {
        protected override void Update()
        {
            if (CheatMenuComponent.InstantBuild && ModAPI.Input.GetButtonDown("InstantBuild"))
            {
                for (int i = 0; i < this._requiredIngredients.Count; i++)
                {
                    int a = _requiredIngredients[i]._itemID;
                    for (int j = 0; j < a; j++)
                        this.AddIngrendient_Actual(i, true);
                }
            }
            else
            {
                base.Update();
            }
        }
    }
}
