using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Items;
using ModAPI.Attributes;
using UnityEngine;

[assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]
namespace maxarrows
{
    internal class Moar : MonoBehaviour
    {
        [ExecuteOnGameStart]
        private static void Iamacheater()
        {
            for (int i = 0; i < ItemDatabase.Items.Length; i++)
            {
                if (ItemDatabase.Items[i]._name.Equals("Arrows"))
                {
                    ItemDatabase.Items[i]._maxAmount = 500;
                    return;
                }
            }
        }
    }
}
