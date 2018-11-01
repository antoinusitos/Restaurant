using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObject : MonoBehaviour 
{
    #region Public Fields
    public Ingredient ingredient;
    public GameObject representation = null;
    #endregion

    #region Private Fields
    private PortableObject _portableObject = null;
    #endregion
 
    #region Unity Methods
    private void Start()
    {
        Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        Plate p = other.GetComponent<Plate>();
        if(p != null && _portableObject.GetTaken())
        {
            p.AddIngredient(this);
            Destroy(gameObject);
        }
    }
    #endregion

    #region Public Methods
    public void Init()
    {
        _portableObject = GetComponent<PortableObject>();
    }
    #endregion

    #region Private Methods
    #endregion
}
