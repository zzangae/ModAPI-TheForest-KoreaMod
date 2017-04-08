using TheForest.Buildings.Creation;
using TheForest.Items.Craft;
using UnityEngine;

namespace EasyBuilding
{
    internal class EasyBuilding : Craft_Structure
    {   
        public override void Initialize()
        {
            if (!_initialized)
            {
                if (_presentIngredients == null)
                {
                    _presentIngredients = new ReceipeIngredient[base._requiredIngredients.Count];
                }
                this.SetUpIcons();
                if (_presentIngredients.Length != base._requiredIngredients.Count)
                {
                    _presentIngredients = new ReceipeIngredient[base._requiredIngredients.Count];
                }
                for (int i = 0; i < base._requiredIngredients.Count; i++)
                {
                    BuildIngredients buildIngredients = _requiredIngredients[i];
                    if (_presentIngredients[i] == null)
                    {
                        _presentIngredients[i] = new ReceipeIngredient { _itemID = buildIngredients._itemID };
                    }
                    ReceipeIngredient receipeIngredient = _presentIngredients[i];

                    if (buildIngredients._amount != 1)
                    {
                        buildIngredients._amount /= 2;
                    }
                    if (receipeIngredient._amount != 1)
                    {
                        receipeIngredient._amount /= 2;
                    }

                    int amount = buildIngredients._amount - receipeIngredient._amount;
                    BuildMission.AddNeededToBuildMission(buildIngredients._itemID, amount);                    
                    for (int j = 0; (j < receipeIngredient._amount) && (j >= buildIngredients._renderers.Length); j++)
                    {
                        buildIngredients._renderers[j].SetActive(true);
                    }
                    
                }
                _initialized = true;
                if (BoltNetwork.isRunning)
                {
                    base.gameObject.AddComponent<CoopConstruction>();
                    if (BoltNetwork.isServer && base.entity.isAttached)
                    {
                        this.UpdateNetworkIngredients();
                    }                    
                    if (!BoltNetwork.isClient)
                    {
                        this.CheckNeeded();
                    }
                }
            }
        }

        protected override void SetUpIcons()
        {   
            if (Application.isPlaying)
            {
                if (this._requiredIngredients != null)
                {
                    float num = 1f / (Screen.width / 70f);                    
                    Vector3 localPosition = new Vector3(0.5f - num * (float)(this._requiredIngredients.Count / 2), 0.06f, 0f);
                    for (int i = 0; i < this._requiredIngredients.Count; i++)
                    {
                        BuildIngredients buildIngredients = this._requiredIngredients[i];                        
                        HudGui.BuildingIngredient icons = this.GetIcons(buildIngredients._itemID);
                        icons._icon.transform.parent = null;
                        icons._text.transform.parent = null;
                        icons._icon.transform.localPosition = localPosition;
                        localPosition.z += 1f;
                        icons._text.transform.localPosition = localPosition;
                        localPosition.z -= 1f;
                        localPosition.x += num;
                    }
                }
                else
                {
                    Destroy(base.gameObject);
                }
            }

        }
    }
}
