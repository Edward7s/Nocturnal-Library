using System;
using UnityEngine.UI;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace NocturnalLibrary.Api
{
    public class MenuHolder : IDisposable
    {
        public int ObjectsCount { get; set; } = 0;
        private GameObject _text { get; set; }
        private TMPro.TextMeshProUGUI _textTmpProComp { get; set; }
        public GameObject Layout { get; set; }
        public GridLayoutGroup GridLayout { get; set; }

        private GameObject _menuObj { get; set; }
        public MenuHolder(string modName,string text) =>
            GenerateMenu(modName,text, Color.white);
        public MenuHolder(string modName,string text, Color textColor) =>
            GenerateMenu(modName, text, textColor);
        private void GenerateMenu(string modName,string text, Color textColor)
        {
            _menuObj = new GameObject("Holder " + modName + "_" + text, new Type[] { typeof(Image), typeof(Mask), typeof(LayoutElement) });
            _menuObj.transform.parent = Menu.Menu.Instance.ViewPort.transform;
            _menuObj.transform.localScale = new Vector3(0.4f, 0.08f, 1);
            _menuObj.transform.localPosition = Vector3.zero;
            _text = GameObject.Instantiate(Utils.Objects.TextTmpGameObject, _menuObj.transform);
            _text.AddComponent<LayoutElement>().ignoreLayout = true;
            _textTmpProComp = _text.GetComponent<TMPro.TextMeshProUGUI>();
            _textTmpProComp.text = $"<size=75>{modName}</size> {text}";
            _textTmpProComp.color = textColor;
            _textTmpProComp.outlineColor = textColor;
            _textTmpProComp.outlineWidth = 0.1f;
            _textTmpProComp.maskable = false;
            _textTmpProComp.enableWordWrapping = false;
            _textTmpProComp.alignment = TMPro.TextAlignmentOptions.TopLeft;
            _text.transform.localScale = new Vector3(0.16f, 0.8f, 1);
            _text.transform.localPosition = new Vector3(-70.6f, 191, 0);
            _text.name = "Title";
            Layout = new GameObject("GridLayour", typeof(GridLayoutGroup));
            Layout.transform.parent = _menuObj.transform;
            Layout.transform.localScale = Vector3.one;
            Layout.transform.localPosition = new Vector3(-74.5f,74.3f, 0);
            Layout.transform.localEulerAngles = Vector3.zero;
            GridLayout = Layout.GetComponent<GridLayoutGroup>();
            GridLayout.constraintCount = 6;
            GridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            GridLayout.spacing = new Vector2(-70, 65);
            Extensions.ChangeSpriteFromString(_menuObj.GetComponent<Image>(), Utils.Config.s_instance.Js.BackGround).color = new Color(0, 0, 0, 0.6f); ;
        }

        public void Dispose()
        {
            _menuObj = null;
            _text = null;
            _textTmpProComp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
