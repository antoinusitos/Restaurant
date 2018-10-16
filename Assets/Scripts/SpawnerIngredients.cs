using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerIngredients : MonoBehaviour 
{
    #region Public Fields
    public GameObject ingredientPrefab = null;
    #endregion

    #region Private Fields
    #endregion

    #region Unity Methods
    #endregion
 
	#region Public Methods
    public void SpawnIngredientFor(CarryObject co)
    {
        GameObject go = Instantiate(ingredientPrefab, transform.position, transform.rotation);
        co.TryTakeObject(go);
    }
    #endregion

    #region Private Methods
    #endregion
}
