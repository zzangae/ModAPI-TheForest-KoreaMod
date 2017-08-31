using ModAPI;
using ModAPI.Attributes;
using System;
using TheForest.Items;
using TheForest.Items.Craft;
using TheForest.Utils;
using TheForest.Items.Inventory;
using UnityEngine;

namespace FullInventory
{
    public class FillInv : MonoBehaviour
    {
        private static string GOName = "__FillInv__";

        [ExecuteOnGameStart]
        private static void PutThisOnScene()
        {
            if (!BoltNetwork.isClient)
            {
                new GameObject(FillInv.GOName).AddComponent<FillInv>();
            }
        }

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("FillInventory"))
            {
                for (int i = 0; i < ItemDatabase.Items.Length; i++)
                {
                    Item item = ItemDatabase.Items[i];
                    try
                    {
                        if (item._maxAmount >= 0)
                        {                            
                            LocalPlayer.Inventory.AddItem(item._id, 2000 - LocalPlayer.Inventory.AmountOf(item._id, true), true, false, null);

                        }
                    }
                    catch (System.Exception)
                    {
                    }
                }
            }
        }
    }
}
