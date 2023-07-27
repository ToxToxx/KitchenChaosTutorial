using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    [SerializeField] private Transform _counterTopPoint;

    public void Interact()
    {
        Debug.Log("Interact");
        Transform kitchenObjectTransform = Instantiate(KitchenObjectSO.prefab, _counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

       Debug.Log( kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}
