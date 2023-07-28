using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    [SerializeField] private Transform _counterTopPoint;

    private KitchenObject _kitchenObject;

    public void Interact()
    {
        if (_kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(KitchenObjectSO.prefab, _counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;

            _kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        }


    }
}
