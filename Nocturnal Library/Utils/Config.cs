using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NocturnalLibrary.Utils
{
    internal class Config
    {
        internal static Config s_instance { get; private set; }

        public Json Js { get; private set; }
        public class Json
        {
            public float ImageOpacity { get; set; }
            public string BackGround { get; set; }
            public string ButtonBackgGround { get; set; }
            public string SliderBackGround { get; set; }
            public string SliderBackGroundFillArea { get; set; }
            public string ToggleIconOn { get; set; }
            public string ToggleIconOff { get; set; }

        }
        private static float ImageOpacity { get; } = 0.3f;
        private static string BackGround { get; } = "https://nocturnal-client.xyz/Resources/maskres.png";
        private static string ButtonBackgGround { get; } = "https://nocturnal-client.xyz/Resources/ButtonIcon.png";
        private static string SliderBackGround { get; } = "https://nocturnal-client.xyz/Resources/Slider.png";
        private static string SliderBackGroundFillArea { get; } = "https://nocturnal-client.xyz/Resources/slider%20white.png";
        private static string ToggleIconOn { get; } = "https://nocturnal-client.xyz/Resources/on.png";
        private static string ToggleIconOff { get; } = "https://nocturnal-client.xyz/Resources/Checkmark.png";

        public Config()
        {
            s_instance = this;

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "//Nocturnal"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "//Nocturnal");

            if (!File.Exists(Directory.GetCurrentDirectory() + "//Nocturnal/LibraryConfig.Json"))
            {
                using (WebClient wc = new WebClient())
                {
                    Json json = new Json();
                    var props = typeof(Json).GetProperties();
                    object prop;
                    for (int i = 0; i < props.Length; i++)
                    {
                        prop = typeof(Config).GetProperty(props[i].Name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(typeof(Config));
                        if (prop.GetType() == typeof(string) && prop.ToString().Contains("https://"))
                        {
                            props[i].SetValue(json, Convert.ToBase64String(wc.DownloadData(prop.ToString())));
                            continue;
                        }
                        props[i].SetValue(json, prop);
                    }
                    File.WriteAllText(Directory.GetCurrentDirectory() + "//Nocturnal//LibraryConfig.Json", JsonConvert.SerializeObject(json));
                    wc.Dispose();
                }
            }
            Js = JsonConvert.DeserializeObject<Json>(File.ReadAllText(Directory.GetCurrentDirectory() + "//Nocturnal//LibraryConfig.Json"));
            using (WebClient wc = new WebClient())
            {
                var props = typeof(Json).GetProperties();
                object prop;
                for (int i = 0; i < props.Length; i++)
                {
                    if (props[i].GetValue(Js) != null) continue;

                    prop = typeof(Config).GetProperty(props[i].Name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(typeof(Config));
                    if (prop.GetType() == typeof(string) && prop.ToString().Contains("https://"))
                    {
                        props[i].SetValue(Js, Convert.ToBase64String(wc.DownloadData(prop.ToString())));
                        continue;
                    }
                    props[i].SetValue(Js, prop);
                }
                SaveConfig();
                wc.Dispose();
            }
            Js = JsonConvert.DeserializeObject<Json>(File.ReadAllText(Directory.GetCurrentDirectory() + "//Nocturnal//LibraryConfig.Json"));
            DefaultMenu.SaveConigs.Add(() => s_instance.SaveConfig());
        }

        public void SaveConfig() => File.WriteAllText(Directory.GetCurrentDirectory() + "//Nocturnal//LibraryConfig.Json", JsonConvert.SerializeObject(Js));

    }
}








