using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour,IKitchenObjectParent
{

    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private ClearCounter _secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject _kitchenObject;


    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if(_kitchenObject!= null) 
            {
                _kitchenObject.SetKitchenObjectParent(_secondClearCounter);
            }
        }
    }
    public void Interact(Player player)
    {
        if (_kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(KitchenObjectSO.prefab, _counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            //Give the object to the player
            //_kitchenObject.SetClearCounter(player);
            Debug.Log(_kitchenObject.GetKitchenObjectParent());
        }
    }

    public Transform GetKitchenObjectFollowTransfrom()
    {
        return _counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() 
    { 
        return _kitchenObject; 
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
