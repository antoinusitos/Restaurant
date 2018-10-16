using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chair : MonoBehaviour 
{
    #region Public Fields
    public Transform enterExitPlace = null;
    public Transform clientPlace = null;
    public GameObject clientMealUI = null;
    public Table table = null;
    #endregion

    #region Private Fields
    private Image _image = null;
    #endregion

    #region Unity Methods
    private void Start()
    {
        _image = clientMealUI.GetComponent<Image>();
    }
    #endregion

    #region Public Methods
    public void ActivateClientMeal(bool newState)
    {
        clientMealUI.SetActive(newState);
    }

    public void ShowTimeWait(float ratio)
    {
        _image.color = Color.Lerp(Color.white, Color.red, ratio);
    }
    #endregion

    #region Private Methods
    #endregion
}
