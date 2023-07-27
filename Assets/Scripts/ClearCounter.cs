using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private Transform _tomatoPrefab;
    [SerializeField] private Transform _counterTopPoint;

    public void Interact()
    {
        Debug.Log("Interact");
        Transform tomatoTransform = Instantiate(_tomatoPrefab, _counterTopPoint);
        tomatoTransform.localPosition = Vector3.zero;
    }
}
