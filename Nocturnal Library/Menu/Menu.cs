using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
namespace NocturnalLibrary.Menu
{
    internal class Menu : MonoBehaviour
    {
        public static Menu Instance { get; set; }
        public  GameObject Mask { get; set; }
        public GameObject ViewPort { get; set; }
        private GridLayoutGroup _viewPortComp { get; set; }
        private Button _buttonComp { get; set; }

        public GameObject Button { get; set; }


        void Start()
        {
            Instance = this;
            this.gameObject.layer = 5;
            this.transform.localPosition = new Vector3(-0.23f, 0, 0);
            this.transform.localEulerAngles = Vector3.zero;
            this.transform.localScale = new Vector3(0.026f, 0.03f, 1);
            this.gameObject.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            this.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
            Mask = GameObject.Instantiate(new GameObject("Mask"), this.transform);
            Mask.layer = 5;
            ViewPort = GameObject.Instantiate(Mask.gameObject, Mask.transform);
            ViewPort.transform.localScale = new Vector3(1.1f, 1, 1);
            Mask.gameObject.AddComponent<Image>().color = new Color(0, 0, 0, Utils.Config.s_instance.Js.ImageOpacity);
            Mask.gameObject.AddComponent<Mask>();
            Mask.gameObject.SetActive(false);
            Mask.transform.localScale = new Vector3(0.23f, 0.23f, 1);
            Mask.transform.localEulerAngles = Vector3.zero;
            Mask.transform.localPosition = new Vector3(36,0);
            _viewPortComp = ViewPort.AddComponent<GridLayoutGroup>();
            _viewPortComp.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _viewPortComp.cellSize = new Vector2(200, 350);
            _viewPortComp.constraintCount = 1;
            _viewPortComp.spacing = new Vector2(0,-315);
            ViewPort.AddComponent<ContentSizeFitter>().verticalFit = UnityEngine.UI.ContentSizeFitter.FitMode.MinSize;
            ViewPort.GetComponent<ContentSizeFitter>().horizontalFit = UnityEngine.UI.ContentSizeFitter.FitMode.MinSize;
            Button = new GameObject("MenuButton", new Type[] { typeof(Image) });
            Button.transform.parent = this.transform;
            _buttonComp = Button.gameObject.AddComponent<Button>();
            _buttonComp.colors = Extensions.ColorBlock;
            Button.transform.localScale = new Vector3(0.03f, 0.02f, 1);
            Button.transform.localPosition = new Vector3(26, 12.9964f, 0);
            Button.transform.localEulerAngles = Vector3.zero;
            Button.layer = 5;
            Button.gameObject.AddComponent<BoxCollider>().size = new Vector3(1, 1, 0.01f);
            Extensions.ChangeSpriteFromString(Button.GetComponent<Image>(), Utils.Config.s_instance.Js.ButtonBackgGround);
            GameObject Menu = GameObject.Instantiate(Utils.Objects.TextTmpGameObject, Button.transform);
            Menu.GetComponent<TMPro.TextMeshProUGUI>().text = "Menu";
            Menu.name = "textNoc";
            Menu.transform.localScale = new Vector3(0.5f, 0.67f, 1);
            Menu.transform.localPosition = new Vector3(44.7712f, 0, 0);
            Menu.transform.localEulerAngles = Vector3.zero;
            _buttonComp.onClick.AddListener(() =>
                Mask.gameObject.SetActive(!Mask.gameObject.activeSelf)
            );
            this.gameObject.AddComponent<GraphicRaycaster>().blockingObjects = GraphicRaycaster.BlockingObjects.None;
            this.gameObject.SetActive(false);
            ScrollRect scrollRect = this.gameObject.AddComponent<ScrollRect>();
            scrollRect.content = ViewPort.gameObject.GetComponent<RectTransform>();
            scrollRect.horizontal = false;
            scrollRect.scrollSensitivity = 3; 
            new DefaultMenu();
            Utils.Actions.UiLoaded.Invoke();
        }
    }
}
