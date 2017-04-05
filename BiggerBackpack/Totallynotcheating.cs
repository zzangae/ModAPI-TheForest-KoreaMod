using ModAPI;
using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TheForest.Items;
using UnityEngine;

namespace BiggerBackpack
{
    internal class Totallynotcheating
    {
        [ExecuteOnGameStart]
        private static void Load()
        {
            string arg = "1.1";
            ModAPI.Console.Write(string.Format("BiggerBackpack v{0} initializing.", arg), "BiggerBackpack");
            if (!System.IO.File.Exists(Application.dataPath + "/BiggerBackpack.ini"))
            {
                ModAPI.Console.Write(string.Format("ini file not found, creating a new one!", arg), "BiggerBackpack");
                try
                {
                    using (System.IO.StreamWriter streamWriter = System.IO.File.AppendText(Application.dataPath + "/BiggerBackpack.ini"))
                    {
                        streamWriter.Write("Stick = 100\r\nRock = 100\r\nBooze = 20\r\nBone = 25\r\ndynamite = 20\r\nBattery = 50\r\nChocolateBar = 50\r\nEnergyMix = 50\r\nSoda = 25\r\nSmallGenericMeat = 25\r\nSkull = 25\r\nRope = 25\r\nRabbitSkin = 15\r\nRabbit Dead = 15\r\nLizard = 15\r\nLizardSkin = 15\r\nCBoard = 15\r\nOrangePaint = 15\r\nBluePaint = 15\r\nFlare = 25\r\nHeadBomb = 5\r\nMolotov = 15\r\nAirCanister = 15\r\nMeds = 15\r\nCod = 15");
                        streamWriter.Close();
                    }
                }
                catch (System.Exception ex)
                {
                    ModAPI.Console.Write("{0}\n", ex.Message);
                }
            }
            try
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(Application.dataPath + "/BiggerBackpack.ini", System.Text.Encoding.Default);
                using (streamReader)
                {
                    string text;
                    do
                    {
                        text = streamReader.ReadLine();
                        if (text != null)
                        {
                            string[] array = text.Split(new char[]
                            {
                                '='
                            });
                            if (array.Length != 0)
                            {
                                Totallynotcheating.ProcessItem(array[0].Trim(), System.Convert.ToInt32(Regex.Replace(array[1], "\\s+", "")));
                            }
                        }
                    }
                    while (text != null);
                    streamReader.Close();
                }
            }
            catch (System.Exception ex2)
            {
                ModAPI.Console.Write("{0}\n", ex2.Message);
            }
            ModAPI.Console.Write(string.Format("BiggerBackpack runtime complete.", arg), "BiggerBackpack");
        }

        private static bool ProcessItem(string itemname, int amt)
        {
            Item[] items = ItemDatabase.Items;
            int i = 0;
            while (i < items.Length)
            {
                Item item = items[i];
                if (item._name == itemname)
                {
                    if (amt < 1 || amt > 100)
                    {
                        ModAPI.Console.Write(string.Format("{0} item capacity is not in allowed range (1 - 100), ignoring item.", itemname), "BiggerBackpack");
                        return false;
                    }
                    item._maxAmount = amt;
                    ModAPI.Console.Write(string.Format("Setting {0} to {1}.", itemname, amt), "BiggerBackpack");
                    return true;
                }
                else
                {
                    i++;
                }
            }
            items = ItemDatabase.Items;
            i = 0;
            while (i < items.Length)
            {
                Item item2 = items[i];
                if (item2._name.ToLower() == itemname.ToLower())
                {
                    ModAPI.Console.Write(string.Format("Warning: {0} is spelled incorrectly but we've found the item case-insensitive.", itemname), "BiggerBackpack");
                    if (amt < 1 || amt > 100)
                    {
                        ModAPI.Console.Write(string.Format("{0} item capacity is not in allowed range (1 - 100), ignoring item.", itemname), "BiggerBackpack");
                        return false;
                    }
                    item2._maxAmount = amt;
                    ModAPI.Console.Write(string.Format("Setting {0} to {1}.", itemname, amt), "BiggerBackpack");
                    return true;
                }
                else
                {
                    i++;
                }
            }
            ModAPI.Console.Write(string.Format("{0} not found in item database, ignoring item.\nHint: you can use the Inventory Mod to see item names.", itemname), "BiggerBackpack");
            return false;
        }
    }
}
