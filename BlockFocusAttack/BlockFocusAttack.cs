using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BlockFocusAttack
{
    public class BlockFocusAttack : MonoBehaviour
    {
        public const string ModName = "BlockFocusAttack";

        public static int LastFocus
        {
            get;
            set;
        }

        public static bool ShouldAttack
        {
            get
            {
                return Environment.TickCount - BlockFocusAttack.LastFocus > 200;
            }
            set
            {
                if (value)
                {
                    BlockFocusAttack.LastFocus = 0;
                }
            }
        }

        [ExecuteOnGameStart]
        private static void Init()
        {
            new GameObject(string.Format("__{0}__", "BlockFocusAttack")).AddComponent<BlockFocusAttack>();
        }

        private void OnApplicationFocus(bool focusStatus)
        {
            if (focusStatus)
            {
                BlockFocusAttack.LastFocus = Environment.TickCount;
            }
        }
    }
}
