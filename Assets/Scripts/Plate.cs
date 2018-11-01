using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour 
{
    #region Public Fields
    public PlateIngredients plateIngredients;
    public MealUI mealUI = null;
    public Transform[] ingredientsShowingPlaces = null;
    #endregion

    #region Private Fields
    #endregion

    #region Unity Methods
    public void Start()
    {
        plateIngredients = new PlateIngredients(0, null);
    }
    #endregion

    #region Public Methods
    public bool ContainsIngredient(Ingredient ing)
    {
        for(int i = 0; i < plateIngredients.ingredientNumber; i++)
        {
            if (plateIngredients.ingredients[i] == ing)
                return true;
        }
        return false;
    }
    
    public void AddIngredient(IngredientObject io)
    {
        plateIngredients.ingredients[plateIngredients.ingredientNumber] = io.ingredient;
        plateIngredients.ingredientNumber++;
        mealUI.gameObject.SetActive(true);
        mealUI.ShowIngredients(plateIngredients.ingredients);
        Transform go = Instantiate(io.representation, transform.position, Quaternion.identity).transform;
        go.SetParent(ingredientsShowingPlaces[plateIngredients.ingredientNumber - 1]);
        go.localPosition = Vector3.zero;
    }
    #endregion

    #region Private Methods
    #endregion
}
