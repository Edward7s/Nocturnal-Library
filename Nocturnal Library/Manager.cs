using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;
using ABI_RC.Core.InteractionSystem;
using System.IO;
using NocturnalLibrary.Menu;
using System.Collections;
using UnityEngine;
using System.Diagnostics;
namespace NocturnalLibrary
{
    public class Manager
    {
        private static float s_version = 0.1f;
     /*  private static bool s_harmony = false;
       private static Type s_harmonyInstance { get; set; }
        private static Type s_harmonyMethod { get; set; }*/

        public static string CheckVersion()
        {         
            using (WebClient wc = new WebClient())
            {
                if (s_version < float.Parse(wc.DownloadString("https://raw.githubusercontent.com/Edward7s/Nocturnal-Library/master/Nocturnal%20Library/Version.txt?token=GHSAT0AAAAAABR34D7AENWIGJTVVQ34EL5IYYHVV3Q").Trim()))
                {
                    wc.Dispose();
                    return "Checking Library Version....\n Library Version Is OutDated Please Download The New one. \n https://github.com/Edward7s/Nocturnal-Library";
                }
                wc.Dispose();
            }
            return "Checking Library Version....\nLibrary Version Its Up To Date";   
        }

        public static void Initialize()
        {
            MelonLoader.MelonLogger.Msg(CheckVersion());
            if (Setup.Instance != null) return;
            new GameObject("Awaiter").AddComponent<Utils.Waiter>();
        }

       

        /*  public static string SetupHarmony()
            {
                if (s_harmony) return null;
                Assembly asmb = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("0Harmony")).FirstOrDefault();
                Type Type = asmb.GetTypes().Where(x => x.Name == "Harmony" && x.Namespace == "HarmonyLib").FirstOrDefault();
                s_harmonyMethod = asmb.GetTypes().Where(x => x.Name == "HarmonyMethod" && x.Namespace == "HarmonyLib").FirstOrDefault();
                s_harmonyInstance = Activator.CreateInstance(Type, new object[] { Guid.NewGuid().ToString() }).GetType();
                s_harmonyInstance.GetMethod("Patch").Invoke(s_harmonyInstance, new object[] { typeof(CVR_MenuManager).GetMethod(nameof(CVR_MenuManager.ToggleQuickMenu)),
                    null,Activator.CreateInstance(s_harmonyMethod,new object[] {typeof(Manager).GetMethod(nameof(patch))}), null,null,null});
                return "Loaded Harmony";
            }

            public static void patch(bool __0) =>  
                File.AppendAllText(Directory.GetCurrentDirectory() + "//BepInEx//LogOutput.log", "////////////////" + __0);*/





    }
}
