using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    //singleton
    public static DeliveryManager Instance { get; private set; }


    private List<RecipeSO> _waitingRecipeSOList;
    [SerializeField] private RecipeListSO _recipeListSO;


    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipesMax = 4;


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

            if(_waitingRecipeSOList.Count < _waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = _recipeListSO.recipeSOList[Random.Range(0, _recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                _waitingRecipeSOList.Add(waitingRecipeSO);
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
                    Debug.Log("Player delivered recipe!");
                    _waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        //no matches found!
        Debug.Log("Player did not deliver correct recipe");
    }

}
