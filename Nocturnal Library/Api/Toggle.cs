using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace NocturnalLibrary.Api
{
    public class Toggle
    {
        private GameObject _button { get; set; }
        private GameObject _text { get; set; }
        private UnityEngine.UI.Toggle _toggle { get; set; }
        private GameObject _iconOn { get; set; }
        private GameObject _iconOff{ get; set; }

        private TMPro.TextMeshProUGUI _textComp { get; set; }

        public Toggle(MenuHolder menu, string text, Action onAction, Action offAction, bool preValue = false)
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
            _button.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.7f);
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
            _button.transform.localScale = new Vector3(0.32f, 1.45f, 1);
            _iconOn = new GameObject("Icon On", new Type[] { typeof(Image) });
            _iconOn.transform.parent = _button.transform;
            _iconOn.SetActive(false);
            _iconOn.transform.localPosition = Vector3.zero;
            _iconOn.transform.localEulerAngles = Vector3.zero;
            _iconOn.transform.localScale = new Vector3(0.65f, 0.65f, 1);
            Extensions.ChangeSpriteFromString(_iconOn.GetComponent<Image>(), Utils.Config.s_instance.Js.ToggleIconOn).color = new Color(1,0,0,0.65f);
            _iconOff = GameObject.Instantiate(_iconOn, _iconOn.transform.parent);
            Extensions.ChangeSpriteFromString(_iconOff.GetComponent<Image>(), Utils.Config.s_instance.Js.ToggleIconOff).color = new Color(0, 1, 0, 0.65f);
            _iconOff.name = "Icon Off";
            _iconOff.transform.localPosition = Vector3.zero;
            Component.DestroyImmediate(_button.GetComponent<UnityEngine.UI.Button>());
            _toggle = _button.AddComponent<UnityEngine.UI.Toggle>();
            _toggle.isOn = preValue;
            if (_toggle.isOn)
            {
                _iconOff.SetActive(false);
                _iconOn.SetActive(true);
            }
            else
            {
                _iconOff.SetActive(true);
                _iconOn.SetActive(false);
            }
            _toggle.onValueChanged.AddListener((bool tog) => {
                if (tog)
                {
                    _iconOff.SetActive(false);
                    _iconOn.SetActive(true);
                    onAction.Invoke();
                    return;
                }
                _iconOff.SetActive(true);
                _iconOn.SetActive(false);
                offAction.Invoke();

            });
        }

    }
}
