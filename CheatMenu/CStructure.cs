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
                    string key = TheForest.Items.ItemDatabase.ItemById(_requiredIngredients[i]._itemID)._name.ToLower();
                    int left = (this._requiredIngredients[i]._amount - this._presentIngredients[i]._amount);
                    this._presentIngredients[i]._amount = this._requiredIngredients[i]._amount;
                    if (key == "stick") TheForest.Buildings.Creation.Craft_Structure.BuildNeedsSticks -= left;
                    if (key == "rock") TheForest.Buildings.Creation.Craft_Structure.BuildNeedsRocks -= left;
                    if (key == "log") TheForest.Buildings.Creation.Craft_Structure.BuildNeedsLogs -= left;
                    if (key == "leaf") TheForest.Buildings.Creation.Craft_Structure.BuildNeedsLeaves -= left;
                    if (key == "cloth") TheForest.Buildings.Creation.Craft_Structure.BuildNeedsCloth -= left;
                    if (key == "rope") TheForest.Buildings.Creation.Craft_Structure.BuildNeedsRope -= left;
                    if (key == "turtleshell") TheForest.Buildings.Creation.Craft_Structure.BuildNeedsTurtleShell -= left;
                    if (key == "molotov") TheForest.Buildings.Creation.Craft_Structure.BuildNeedsMolotov -= left;
                    if (key == "bombtimed") TheForest.Buildings.Creation.Craft_Structure.BuildNeedsExplosive -= left;
                }
                this.UpdateNeededRenderers();
                if (BoltNetwork.isRunning)
                    this.UpdateNetworkIngredients();
                RefreshBuildMission();
                this.CheckNeeded();
                TheForest.Utils.Scene.HudGui.DestroyIcon.SetActive(false);
            }
            else
            {
                base.Update();
            }
        }
    }
}
