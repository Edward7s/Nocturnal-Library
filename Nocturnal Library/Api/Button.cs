using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace NocturnalLibrary.Api
{
    internal class Button : IDisposable
    {
        private GameObject _button { get; set; }
        private GameObject _text { get; set; }
        private TMPro.TextMeshProUGUI _textComp { get; set; }

        public Button(MenuHolder menu, string text, Action action)
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
            _button.transform.localScale = new Vector3(0.32f, 1.45f, 1);
            _button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => action.Invoke());
            Dispose();
        }

        public void Dispose()
        {
            _text = null;
            _button = null;
            _textComp = null;
        }


    }
}
