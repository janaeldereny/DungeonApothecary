using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]


public class  RecipeSO  : ScriptableObject
{
    
    public ItemScriptableObject ingredientA;
    public ItemScriptableObject ingredientB;
    public ItemScriptableObject resultCure;

    public bool Matches(ItemScriptableObject slot1, ItemScriptableObject slot2)
    {
        if (slot1 == null || slot2 == null) return false;

        bool orderOne = slot1 == ingredientA && slot2 == ingredientB;
        bool orderTwo = slot1 == ingredientB && slot2 == ingredientA;

        return orderOne || orderTwo;

    }
   
}
