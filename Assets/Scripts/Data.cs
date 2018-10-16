using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ingredient
{
    NONE,
    TOMATO,
    SALAD,
    HAM,
    BEANS,

    LENGHT,
}

[System.Serializable]
public struct PlateIngredients
{
    public int ingredientNumber;
    public Ingredient[] ingredients;
    public bool served;

    public PlateIngredients(int theNumber = 0, Ingredient[] theIngredients = null)
    {
        ingredientNumber = theNumber;
        if (theIngredients == null)
            ingredients = new Ingredient[4];
        else
            ingredients = theIngredients;
        served = false;
    }
}

[System.Serializable]
public struct IngredientSprites
{
    public Ingredient ingredient;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "Data", menuName = "Restaurant", order = 1)]
public class ScriptableData : ScriptableObject
{
    public IngredientSprites[] ingredientSprites = null;
}

public class Data : MonoBehaviour
{
    #region Public Fields
    public ScriptableData scriptableData;
    #endregion

    #region Private Fields
    #endregion

    #region Unity Methods
    #endregion

    #region Public Methods
    public static T[] ShuffleArray<T>(ref T[] theArray)
    {
        for(int i = 0; i < theArray.Length; i++)
        {
            int rand = Random.Range(0, theArray.Length);
            T temp = theArray[rand];
            theArray[rand] = theArray[i];
            theArray[i] = temp;
        }
        return theArray;
    }
    
    public Sprite GetIngredientSprite(Ingredient ing)
    {
        for(int i = 0; i < scriptableData.ingredientSprites.Length; i++)
        {
            if (scriptableData.ingredientSprites[i].ingredient == ing)
                return scriptableData.ingredientSprites[i].sprite;
        }
        return null;
    }
    #endregion

    #region Private Methods
    #endregion

    //
    // Singleton Stuff
    // 

    private static Data _instance;

    public static Data GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
