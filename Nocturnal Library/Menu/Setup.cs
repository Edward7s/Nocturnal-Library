using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABI_RC.Core;
using ABI_RC.Core.InteractionSystem;
using ABI_RC.Core.Savior;
using UnityEngine;
using UnityEngine.UI;

namespace NocturnalLibrary.Menu
{
    public class Setup
    {
        public static Setup Instance { get; set; } = null;

        private HarmonyLib.Harmony Harmony = new HarmonyLib.Harmony(Guid.NewGuid().ToString());
        internal Setup()
        {
            new GameObject("Nocturnal Ui").AddComponent<Menu>().transform.parent = GameObject.Find("/Cohtml/QuickMenu").transform;

            Instance = this;
            Harmony.Patch(typeof(CVR_MenuManager).GetMethod(nameof(CVR_MenuManager.ToggleQuickMenu)), null, typeof(HarmonyPatches).GetMethod(nameof(HarmonyPatches.ToggleMenu)).ToHarmonyMethod());
         //   Harmony.Patch(typeof(CVR_MenuManager).GetMethod(nameof(CVR_MenuManager.SetScale)), null, typeof(HarmonyPatches).GetMethod(nameof(HarmonyPatches.MenuScale)).ToHarmonyMethod());
        }
    }

    public class HarmonyPatches
    {
        private static float s_scaleFactor { get; set; }
        public static void ToggleMenu(bool __0) =>     
            Menu.Instance.gameObject.SetActive(__0);
        

        public static void MenuScale(float __0)
        {
            s_scaleFactor = __0 / 1.8f;
            if (MetaPort.Instance.isUsingVr)
                s_scaleFactor *= 0.5f;
       
            Menu.Instance.transform.localScale = new Vector3(s_scaleFactor / 1550 +  0.0171f ,  s_scaleFactor / 1550 + 0.0171f , 1);
        }
    }
}
