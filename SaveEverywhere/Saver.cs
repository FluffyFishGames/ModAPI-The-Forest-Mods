using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaveEverywhere
{
    class Saver
    {
        [ModAPI.Attributes.ExecuteEveryFrame(true)]
        public static void CheckSave()
        { 
            if (ModAPI.Input.GetButtonDown("Save"))
            {
                TheForest.Utils.LocalPlayer.Stats.JustSave();
            }
        }
    }
}
