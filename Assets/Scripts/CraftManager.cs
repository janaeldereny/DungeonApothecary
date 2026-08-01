using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using NUnit.Framework;

public class CraftManager : MonoBehaviour
{
    public List<RecipeSO> recipes;
    [SerializeField] private Inventory inventory;
    //private RecipeSO recipesSO;
   
    [SerializeField] public  bool isCure = false;
    [SerializeField] private RecipeSO matchedRecipe ;


    public void startCrafting()
    {
         Debug.Log("Slot0: " + inventory.items[0]?.name + " | Slot1: " + inventory.items[1]?.name);

        foreach (RecipeSO recipe in recipes)
        {
            if (recipe.Matches(inventory.items[0], inventory.items[1]))
            {
            isCure=true;
            matchedRecipe = recipe;
             inventory.ConsumeAndCraft(matchedRecipe.resultCure);
             break;  
            
            }
            
        }
       
        if (!isCure)
        {
              Debug.Log("No matching recipe");
        }
       
    }
    
}
