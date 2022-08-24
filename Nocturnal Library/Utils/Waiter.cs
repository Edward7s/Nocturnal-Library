using ABI_RC.Core.Player;
using NocturnalLibrary.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static NocturnalLibrary.Utils.Actions;

namespace NocturnalLibrary.Utils
{
    internal class Waiter : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            InvokeRepeating("WaitForObjects", -1, 0.1f);
            new Config();
        }

        private void WaitForObjects()
        {
            if (Resources.FindObjectsOfTypeAll<PuppetMaster>().FirstOrDefault(x => x.name == "_NetworkedPlayerObject") == null) return;
            Objects.NamePlateContent = Resources.FindObjectsOfTypeAll<PuppetMaster>().FirstOrDefault(x => x.name == "_NetworkedPlayerObject").transform.Find("[NamePlate]/Canvas/Content").gameObject;
            Objects.TextTmpGameObject = Resources.FindObjectsOfTypeAll<TMPro.TextMeshProUGUI>().FirstOrDefault(x => x.name == "DisplayName").gameObject;
            Objects.MenuManager = GameObject.Find("/Cohtml").gameObject.GetComponent<ABI_RC.Core.InteractionSystem.CVR_MenuManager>();
            new Setup();
            CancelInvoke("WaitForObjects");
        }
    }
}
