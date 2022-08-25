using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace NocturnalLibrary.Api
{
    public class Slider
    {
        private GameObject _button { get; set; }
        private GameObject _text { get; set; }
        private TMPro.TextMeshProUGUI _textComp { get; set; }
        public UnityEngine.UI.Slider SliderComp { get; set; }
        private GameObject _fillGameObject { get; set; }

        public Slider(MenuHolder menu, string text, Action<float> onValueChange, float preValue = 0,Action action = null)
        {
            if (menu.ObjectsCount > 11)
            {
                MelonLoader.MelonLogger.Msg("Can Not Have More Then 12 Objects In A Menu Please Create Another One");
                return;
            }
            menu.ObjectsCount++;
            _button = GameObject.Instantiate(Menu.Menu.Instance.Button, menu.Layout.transform);
            _button.AddComponent<LayoutElement>().minHeight = 100;
            _button.GetComponent<LayoutElement>().minWidth = 100;
            _button.transform.localScale = new Vector3(1, 1, 1);
            _text = _button.transform.Find("textNoc").gameObject;
            _textComp = _text.GetComponent<TMPro.TextMeshProUGUI>();
            _textComp.text = text;
            _textComp.fontSize = 35;
            _textComp.alignment = TMPro.TextAlignmentOptions.Center;
            _textComp.raycastTarget = false;
            _text.transform.localPosition = new Vector3(-87f, 0, 0);
            _text.GetComponent<RectTransform>().sizeDelta = new Vector2(-340, 100);
            _text.transform.localScale = new Vector3(0.53f, 0.67f, 1);
            _button.transform.localScale = new Vector3(0.28f, 1.25f, 1);
            Component.DestroyImmediate(_button.GetComponent<UnityEngine.UI.Button>());
            SliderComp = _button.AddComponent<UnityEngine.UI.Slider>();
            SliderComp.value = preValue;
            SliderComp.direction = UnityEngine.UI.Slider.Direction.BottomToTop;
            _fillGameObject = new GameObject("SliderFill");
            _button.GetComponent<UnityEngine.UI.Image>().ChangeSpriteFromString(Utils.Config.s_instance.Js.SliderBackGround);
            _fillGameObject.gameObject.AddComponent<Image>().ChangeSpriteFromString(Utils.Config.s_instance.Js.SliderBackGroundFillArea).color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            _fillGameObject.transform.parent = _button.transform;
            _fillGameObject.transform.localScale = Vector3.one;
            _fillGameObject.transform.localPosition = new Vector3(0, 0);
            _fillGameObject.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            _fillGameObject.transform.localEulerAngles = Vector3.zero;
            _fillGameObject.layer = 5;
            SliderComp.fillRect = _fillGameObject.GetComponent<RectTransform>();
            SliderComp.onValueChanged.AddListener((float value) =>
            {
                action.Invoke();
                onValueChange(value);

            });
            SliderComp.colors = Extensions.ColorBlock;

        }
    }
}
