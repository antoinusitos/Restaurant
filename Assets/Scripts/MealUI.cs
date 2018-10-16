using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MealUI : MonoBehaviour 
{
    #region Public Fields
    public Image[] _ingredientsImage = null;
    #endregion

	#region Private Fields
    #endregion
 
    #region Unity Methods
    #endregion
 
	#region Public Methods
    public void ShowIngredients(Ingredient[] ingredients)
    {
        Color transparent = new Color(0, 0, 0, 0);
        for (int i = 0; i < _ingredientsImage.Length; i++)
        {
            _ingredientsImage[i].color = transparent;
        }
        for(int i = 0; i < ingredients.Length; i++)
        {
            _ingredientsImage[i].color = Color.white;
            _ingredientsImage[i].sprite = Data.GetInstance().GetIngredientSprite(ingredients[i]);
        }
    }
    #endregion

    #region Private Methods
    #endregion
}
