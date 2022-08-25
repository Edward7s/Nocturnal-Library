using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace NocturnalLibrary.Api
{
    public class Button : IDisposable
    {
        public GameObject Object { get; set; }
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
            Object = GameObject.Instantiate(Menu.Menu.Instance.Button, menu.Layout.transform);
            Object.AddComponent<LayoutElement>().minHeight = 100;
            Object.GetComponent<LayoutElement>().minWidth = 100;
            Object.transform.localScale = new Vector3(1, 1, 1);
            _text = Object.transform.Find("textNoc").gameObject;
            _textComp = _text.GetComponent<TMPro.TextMeshProUGUI>();
            _textComp.text = text;
            _textComp.fontSize = 35;
            _textComp.alignment = TMPro.TextAlignmentOptions.Center;
            _textComp.raycastTarget = false;   
            _text.transform.localPosition = new Vector3(-87f, 0, 0);
            _text.GetComponent<RectTransform>().sizeDelta = new Vector2(-340, 100);
            _text.transform.localScale = new Vector3(0.53f, 0.67f, 1);
            Object.transform.localScale = new Vector3(0.32f, 1.45f, 1);
            Object.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => action.Invoke());
        }

        public void Dispose()
        {
            _text = null;
            Object = null;
            _textComp = null;
        }


    }
}
