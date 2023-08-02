using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList;
    private List<KitchenObjectSO> _kitchenObjectSOList;

    private void Awake()
    {
        _kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!_validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //not a valid kitchen object
            return false;
        }
        if (_kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Already has this kitchen object
            return false;
        }
        else
        {
            _kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
        
    }
}
