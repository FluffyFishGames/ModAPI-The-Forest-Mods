using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savegames
{
    class NewPlayerStats : PlayerStats
    {
        public override void JustSave()
        {
            TheForest.Utils.LocalPlayer.FpCharacter.LockView(true);
            this.SendMessage("CloseBuildMode");
            TheForest.Utils.Scene.HudGui.PauseMenu.SetActive(true);
            TheForest.Utils.Scene.HudGui.SendMessage("OpenPause");
            TheForest.Utils.LocalPlayer.Inventory.CurrentView = TheForest.Items.Inventory.PlayerInventory.PlayerViews.Pause;
            //base.JustSave();
        }
    }
}
