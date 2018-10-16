using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : BaseManager
{
    #region Public Fields
    #endregion

    #region Private Fields
    private Ingredient[] _menu = null;
    private bool _readyToPlay = false;
    #endregion

    #region Unity Methods
    private void Start()
    {
        int ingredientNumber = (int)Ingredient.LENGHT - 1;
        _menu = new Ingredient[ingredientNumber];
        for (int i = 1; i <= ingredientNumber; i++)
        {
            _menu[i - 1] = (Ingredient)i;
        }
        _readyToPlay = true;
    }
    #endregion

    #region Public Methods
    public Ingredient[] GetMenu()
    {
        return _menu;
    }

    public bool GetReadyToPlay()
    {
        return _readyToPlay;
    }
    #endregion

    #region Private Methods
    #endregion


    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static MenuManager _instance;

    public static MenuManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
