using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Savegames
{
    class NewPlayerInventory : TheForest.Items.Inventory.PlayerInventory
    {
        override public void TogglePauseMenu()
        {
            if (NewHudGui.SaveOpened)
            {
                TheForest.Utils.Scene.HudGui.SendMessage("CloseSave");
            }
            else if (this.CurrentView == PlayerViews.Pause)
            {
                TheForest.Utils.LocalPlayer.FpCharacter.UnLockView();
                TheForest.Utils.Scene.HudGui.CheckHudState();
                TheForest.Utils.Scene.HudGui.PauseMenu.SetActive(false);
                this.CurrentView = PlayerViews.World;
                Time.timeScale = 1f;
            }
            else
            {
                TheForest.Utils.LocalPlayer.FpCharacter.LockView(true);
                this.SendMessage("CloseBuildMode");
                TheForest.Utils.Scene.HudGui.PauseMenu.SetActive(true);
                this.CurrentView = PlayerViews.Pause;
                Time.timeScale = 0.0f;
            }
        }
    }
}
