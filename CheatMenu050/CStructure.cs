using Bolt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Buildings.Creation;
using TheForest.Items.Craft;
using TheForest.Utils;
using UnityEngine;

namespace CheatMenu050
{
    internal class CStructure : TheForest.Buildings.Creation.Craft_Structure
    {
        [ModAPI.Attributes.Priority(1000)]
        protected override void AddIngredient(int ingredientNum)
        {
            if (!CheatMenuComponent.InstantBuild)
            {
                base.AddIngredient(ingredientNum);
                return;
            }
            Craft_Structure.BuildIngredients buildIngredients = this._requiredIngredients[ingredientNum];
            LocalPlayer.Sfx.PlayHammer();                
                if (BoltNetwork.isRunning)
                {
                    AddIngredient addIngredient = global::AddIngredient.Create(GlobalTargets.OnlyServer);
                    addIngredient.IngredientNum = ingredientNum;
                    addIngredient.ItemId = buildIngredients._itemID;
                    addIngredient.Construction = base.GetComponentInParent<BoltEntity>();
                    addIngredient.Send();
                return;
                }
           base.AddIngrendient_Actual(ingredientNum, true, null);
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void Update()
        {
            if (CheatMenuComponent.InstantBuild && ModAPI.Input.GetButtonDown("InstantBuild"))
            {
                TheForest.Utils.Scene.HudGui.CantPlaceIcon.SetActive(false);                
                for (int j = 0; j < this._requiredIngredients.Count; j++)
                {                    
                    int num = this._requiredIngredients[j]._amount - this._presentIngredients[j]._amount;
                    for (int k = 0; k < num; k++)
                    {
                        this.AddIngredient(j);
                    }                   
                }
                return;               
            }
            base.Update();
        }

        protected override void Start()
        {
            if (CheatMenuComponent.AutoBuild)
            {
                base.Invoke("Build", 2f);
            }
            base.Start();
        }
    }
}
