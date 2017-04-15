using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using UnityEngine;
using TheForest.Buildings.Creation;

namespace BluePrint_Remove
{
    class BluePrintRemove : MonoBehaviour
    {

        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            new GameObject("__BluePrint__").AddComponent<BluePrintRemove>();
        }       

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("BluePrint_Remove"))
            {
                Craft_Structure[] array = UnityEngine.Object.FindObjectsOfType<Craft_Structure>();
                if (array != null && array.Length > 0)
                {
                    Craft_Structure[] array2 = array;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        Craft_Structure craft_Structure = array2[i];
                        craft_Structure.CancelBlueprint();
                    }
                }
            }
        }
    }
}
