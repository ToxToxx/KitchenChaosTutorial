using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{


    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);

    }
    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += _plateKitchenObject_OnIngredientAdded;
    }

    private void _plateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)//second method of adding and removing visual
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in _plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(_iconTemplate, transform); // transform import for become a child of object
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
