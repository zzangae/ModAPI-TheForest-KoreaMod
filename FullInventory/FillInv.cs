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
            var Fillinven1 = ModAPI.Input.GetButton("FillInventory");
            var Fillinven2 = ModAPI.Input.GetButton("FillUnlimit");

            if (Fillinven1)
            {                
                for (int i = 0; i < ItemDatabase.Items.Length; i++)
                {
                    Item item = ItemDatabase.Items[i];
                    try
                    {
                        if (item._maxAmount >= 0)
                        {
                            LocalPlayer.Inventory.AddItem(item._id, 100000, true, false, null);
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                }                
            }
            if (Fillinven2)
            {
                LocalPlayer.Inventory.ItemFilter = new InventoryItemFilter_Unlimited();
            }
        }
    }
}
