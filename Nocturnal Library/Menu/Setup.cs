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
using NocturnalLibrary.Utils;
using ABI_RC.Core.Player;
using MelonLoader;
using static Gaia.GaiaConstants;

namespace NocturnalLibrary.Menu
{
    public class Setup
    {
        public static Setup Instance { get; set; } = null;

        private HarmonyLib.Harmony Harmony = new HarmonyLib.Harmony(Guid.NewGuid().ToString());
        internal Setup()
        {
            try
            {
                new GameObject("Nocturnal Ui").AddComponent<Menu>().transform.parent = GameObject.Find("/Cohtml/QuickMenu").transform;
                Instance = this;
                Harmony.Patch(typeof(CVR_MenuManager).GetMethod(nameof(CVR_MenuManager.ToggleQuickMenu)), null, typeof(HarmonyPatches).GetMethod(nameof(HarmonyPatches.ToggleMenu)).ToHarmonyMethod());
                Harmony.Patch(typeof(CVR_MenuManager).GetMethod(nameof(CVR_MenuManager.SetScale)), null, typeof(HarmonyPatches).GetMethod(nameof(HarmonyPatches.MenuScale)).ToHarmonyMethod());
                Harmony.Patch(typeof(PlayerNameplate).GetMethod(nameof(PlayerNameplate.UpdateNamePlate)), null, typeof(HarmonyPatches).GetMethod(nameof(HarmonyPatches.Plate)).ToNewHarmonyMethod());
              Harmony.Patch(typeof(PuppetMaster).GetMethod(nameof(PuppetMaster.AvatarInstantiated)), null, typeof(HarmonyPatches).GetMethod(nameof(HarmonyPatches.AvatarChanged)).ToNewHarmonyMethod());
                RootLogic.Instance.comms.OnPlayerEnteredRoom += (Dissonance.VoicePlayerState state, string str) => Actions.OnUserJoined.Invoke(state, str);
                RootLogic.Instance.comms.OnPlayerExitedRoom += (Dissonance.VoicePlayerState state, string str) => Actions.OnUserLeft.Invoke(state, str);
                RootLogic.Instance.comms.OnPlayerStartedSpeaking += (Dissonance.VoicePlayerState state) => Actions.OnUserStartTalking.Invoke(state);
                RootLogic.Instance.comms.OnPlayerStoppedSpeaking += (Dissonance.VoicePlayerState state) => Actions.OnUserStopTalking.Invoke(state);
            }
            catch (Exception ex) { MelonLogger.Msg(ex); }
           
        }
    }

    public class HarmonyPatches
    {
        public static void ToggleMenu(bool __0)
        {
            Actions.OnMenuToggle.Invoke(__0);
            Menu.Instance.gameObject.SetActive(__0);
        }

        public static void MenuScale(float __0) =>
                        Actions.OnMenuScale.Invoke(__0);
        public static void Plate(PlayerNameplate __instance) =>
                                   Actions.OnNamePlateCreate.Invoke(__instance);

        public static void AvatarChanged(PuppetMaster __instance) =>
                             Actions.OnAvatarChanged.Invoke(__instance);
    }
}
