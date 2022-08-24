using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NocturnalLibrary.Api;
using System.Diagnostics;
using System.IO;

namespace NocturnalLibrary.Menu
{
    class DefaultMenu
    {
        public DefaultMenu()
        {
            MenuHolder defaultMenu = new MenuHolder("Library", "Default Menu",(UnityEngine.Color)new UnityEngine.Color32(255, 130, 174,255));
            new Button(defaultMenu, "Save Configs",()=>
            {
                for (int i = 0; i < Utils.DefaultMenu.SaveConigs.Count; i++)
                    Utils.DefaultMenu.SaveConigs[i].Invoke();
            });
            new Button(defaultMenu, "Close", () =>
                Process.GetCurrentProcess().Kill()
            );  
            new Slider(defaultMenu, "BackGround", x => Utils.Config.s_instance.Js.ImageOpacity = x, Utils.Config.s_instance.Js.ImageOpacity, () => Menu.Instance.Mask.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(0, 0, 0, Utils.Config.s_instance.Js.ImageOpacity));
        }

    }
}
