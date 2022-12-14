using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine;
using ABI_RC.Core.Networking.IO.Social;
using ABI_RC.Core.Player;
using ABI_RC.Core;

namespace NocturnalLibrary
{
     public static class Extensions
    {
        public static ColorBlock ColorBlock { get; } = new ColorBlock() { 
        normalColor = (Color)new Color32(40, 40, 40,180),
        highlightedColor = (Color)new Color32(61, 61, 61,240),
        pressedColor = (Color)new Color32(104, 104, 104 ,255),
        fadeDuration = 0.1f,
        disabledColor = (Color)new Color32(20, 20, 20, 70),
        colorMultiplier = 1,
        selectedColor = (Color)new Color32(40, 40, 40, 180),
        };

        public static Color FloatArrToColor32(this float[] floatArr)
        {
            if (floatArr.Length == 4)
                return new Color(byte.Parse(Math.Round(floatArr[0]).ToString()), byte.Parse(Math.Round(floatArr[1]).ToString()), byte.Parse(Math.Round(floatArr[2]).ToString()), byte.Parse(Math.Round(floatArr[3]).ToString()));
            return new Color(byte.Parse(Math.Round(floatArr[0]).ToString()), byte.Parse(Math.Round(floatArr[1]).ToString()), byte.Parse(Math.Round(floatArr[2]).ToString()), 255);
        }
        public static Color FloatArrToColor(this float[] floatArr)
        {
            if (floatArr.Length == 4)
                return (Color)new Color32(byte.Parse(floatArr[0].ToString()), byte.Parse(floatArr[1].ToString()), byte.Parse(floatArr[2].ToString()), byte.Parse(floatArr[3].ToString()));
            return (Color)new Color32(byte.Parse(floatArr[0].ToString()), byte.Parse(floatArr[1].ToString()), byte.Parse(floatArr[2].ToString()), 1);

        }
        public static byte[] FloatArrToByteArr(this float[] floatArr)
        {
            if (floatArr.Length == 4)
                return new byte[] { byte.Parse(floatArr[0].ToString()), byte.Parse(floatArr[1].ToString()), byte.Parse(floatArr[2].ToString()), byte.Parse(floatArr[3].ToString()) };
            return new byte[] { byte.Parse(floatArr[0].ToString()), byte.Parse(floatArr[1].ToString()), byte.Parse(floatArr[2].ToString()),255};
        }
        public static Color IntArrToColor32(this int[] intArr)
        {
            if (intArr.Length == 4)
                return (Color)new Color32(byte.Parse(intArr[0].ToString()), byte.Parse(intArr[1].ToString()), byte.Parse(intArr[2].ToString()), byte.Parse(intArr[3].ToString()));
            return (Color)new Color32(byte.Parse(intArr[0].ToString()), byte.Parse(intArr[1].ToString()), byte.Parse(intArr[2].ToString()), 255 );
        }

        public static byte ToByte(this float floatnumb)
            => byte.Parse(floatnumb.ToString());

        private static Texture2D _Texture2d { get; set; }

        public static HarmonyMethod ToHarmonyMethod(this MethodInfo MethodInfo) =>
            new HarmonyMethod(MethodInfo);

        public static Image ChangeSpriteFromString(this Image Image, string ImageBase64, int pixels = 200, Vector4 border = new Vector4())
        {
            _Texture2d = new Texture2D(256, 256);
            ImageConversion.LoadImage(_Texture2d, Convert.FromBase64String(ImageBase64));
            Image.sprite = Sprite.Create(_Texture2d, new Rect(0, 0, _Texture2d.width, _Texture2d.height), new Vector2(0, 0), pixels, 1000u, SpriteMeshType.FullRect, border, false);
            return Image;
        }

        public static bool IsFriend(string UserID)
        {
            if (Friends.List.Where(x => x.UserId == UserID).FirstOrDefault() != null) return true;
            return false;
        }

        public static GameObject GetPlayer(string UserId) =>
            GameObject.Find(CVRPlayerManager.Instance.NetworkPlayers.Find(x => x.Uuid == UserId).Uuid);

        public static GameObject LocalPlayer() => RootLogic.Instance.localPlayerRoot;

        public static void Respawn() => RootLogic.Instance.Respawn();
            

    }
}
