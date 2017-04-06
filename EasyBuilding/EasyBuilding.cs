using TheForest.Buildings.Creation;
using TheForest.Items.Craft;
using UnityEngine;

namespace EasyBuilding
{
    internal class EasyBuilding : Craft_Structure
    {
        private bool _initialized;        
        private ReceipeIngredient[] _presentIngredients;

        public new void Initialize()
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
                    BuildIngredients ingredients = _requiredIngredients[i];
                    if (_presentIngredients[i] == null)
                    {
                        _presentIngredients[i] = new ReceipeIngredient { _itemID = ingredients._itemID };
                    }
                    ReceipeIngredient ingredient = _presentIngredients[i];
                    if (ingredients._amount != 1)
                    {
                        ingredients._amount /= 2;
                    }
                    if (ingredient._amount != 1)
                    {
                        ingredient._amount /= 2;
                    }
                    int amount = ingredients._amount - ingredient._amount;
                    BuildMission.AddNeededToBuildMission(ingredients._itemID, amount);
                    for (int j = 0; (j < ingredient._amount) && (j < ingredients._renderers.Length); j++)
                    {
                        ingredients._renderers[j].SetActive(true);
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
                }
            }
        }

        private void SetUpIcons()
        {
            //throw new NotImplementedException();
            if (Application.isPlaying)
            {
                if (this._requiredIngredients != null)
                {
                    float num = 1f / (Screen.width / 70f);
                    Vector3 vector = new Vector3(0.5f - (num * (this._requiredIngredients.Count - 0.5f)), 0.15f, 0f);
                    for (int i = 0; i < this._requiredIngredients.Count; i++)
                    {
                        BuildIngredients ingredients = this._requiredIngredients[i];
                        if (ingredients._icon.transform.parent != base.transform)
                        {
                            ingredients._icon = Instantiate(ingredients._icon);
                            ingredients._icon.transform.parent = base.transform;
                            ingredients._icon.transform.position = vector;
                            ingredients._text = Instantiate(ingredients._text);
                            ingredients._text.transform.parent = base.transform;
                            vector.z++;
                            ingredients._text.transform.position = vector;
                            vector.z--;
                            vector.x += num;
                        }
                        ingredients._icon.SetActive(false);
                        ingredients._text.SetActive(false);
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
