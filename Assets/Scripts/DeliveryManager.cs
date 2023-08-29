using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    //singleton
    public static DeliveryManager Instance { get; private set; }


    private List<RecipeSO> _waitingRecipeSOList;
    [SerializeField] private RecipeListSO _recipeListSO;


    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipesMax = 4;
    private int _successfulRecipesAmount = 0;


    private void Awake()
    {
        Instance = this;
        _waitingRecipeSOList = new List<RecipeSO>();
    }


    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;

        if(_spawnRecipeTimer <= 0f)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if(KitchenGameManager.Instance.IsGamePlaying() && _waitingRecipeSOList.Count < _waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = _recipeListSO.recipeSOList[UnityEngine.Random.Range(0, _recipeListSO.recipeSOList.Count)];

                _waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count ==  plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has the same number of ingredients
                bool plateContentsMuchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    //Going through all ingredients for equal ingredients
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling through all ingredient on a plate
                        if(plateKitchenObjectSO ==  recipeKitchenObjectSO)
                        {
                            //Ingredients matches
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound)
                    {
                        //this recipe ingredient was not on a plate
                        plateContentsMuchesRecipe = false;
                    }
                }
                if(plateContentsMuchesRecipe)
                {
                    //Player delivered correct recipe
                    _successfulRecipesAmount++;
                    _waitingRecipeSOList.RemoveAt(i);
                    
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //no matches found!
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return _waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return _successfulRecipesAmount;
    }

}
