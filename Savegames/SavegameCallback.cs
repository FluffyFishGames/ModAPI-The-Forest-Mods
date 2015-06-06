using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Savegames
{
    public class SavegameCallback
    {
        public string DisplayName;
        public GameObject Receiver;
        public string CallbackName;

        public void Execute()
        {
            Receiver.SendMessage(CallbackName, DisplayName);
        }
    }
}
