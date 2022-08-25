using ABI_RC.Core.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MelonLoader.MelonLogger;

namespace NocturnalLibrary.Utils
{
    public class Actions
    {
        public static Action OnUiLoaded = () => {};

        public static Action<bool> OnMenuToggle = (bool value) => { };

        public static Action<float> OnMenuScale = (float value) => { };

        public static Action<PuppetMaster> OnAvatarChanged = (PuppetMaster value) => { };

        public static Action<PlayerNameplate> OnNamePlateCreate = (PlayerNameplate value) => { };

        public static Action<Dissonance.VoicePlayerState, string> OnUserJoined = (Dissonance.VoicePlayerState state, string value) => { };

        public static Action<Dissonance.VoicePlayerState, string> OnUserLeft = (Dissonance.VoicePlayerState state, string value) => { };

        public static Action<Dissonance.VoicePlayerState> OnUserStartTalking = (Dissonance.VoicePlayerState value) => { };

        public static Action<Dissonance.VoicePlayerState> OnUserStopTalking = (Dissonance.VoicePlayerState value) => { };


    }
}
