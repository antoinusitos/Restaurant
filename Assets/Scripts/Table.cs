using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour 
{
    #region Public Fields
    public Client[] allClient = new Client[2];
    public Chair[] allChair = null;
    public bool used = false;
    #endregion

    #region Private Fields
    private int _currentClientNumber = 0;
    #endregion

    #region Unity Methods
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!used) return;

        int nbReadyToBeServed = 0;
        for (int i = 0; i < allClient.Length; i++)
        {
            if (allClient[i] != null && allClient[i].GetReadyToBeServed())
            {
                nbReadyToBeServed++;
                break;
            }
        }

        if (nbReadyToBeServed == 0) return;

        Plate p = other.GetComponent<Plate>();
        if(p != null)
        {
            int plateFound = -1;
            if(CompatePlates(p, out plateFound))
            {
                allClient[plateFound].Served();
                bool allServed = true;
                for (int i = 0; i < _currentClientNumber; i++)
                {
                    if (!allClient[i].desiredPlate.served)
                    {
                        allServed = false;
                        break;
                    }
                }
                if(allServed)
                {
                    ClientsLeave();
                }
            }
            Destroy(p.gameObject);
        }
    }
    #endregion
 
	#region Public Methods
    public void ClientArrive(Client c)
    {
        allClient[_currentClientNumber] = c;
        _currentClientNumber++;
    }

    public void ClientsLeave()
    {
        used = false;
        int tempClientNumber = _currentClientNumber;
        for (int i = 0; i < tempClientNumber; i++)
        {
            allClient[i].Leave();
            _currentClientNumber--;
        }
        ClientManager.GetInstance().TableLeave();
    }
    #endregion

    #region Private Methods
    private bool CompatePlates(Plate enteringPlate, out int plateFound)
    {
        for (int i = 0; i < _currentClientNumber; i++)
        {
            if(allClient[i].desiredPlate.ingredientNumber == enteringPlate.plateIngredients.ingredientNumber)
            {
                if (allClient[i].GetReadyToBeServed() && !allClient[i].GetServed())
                {
                    //TODO : check ingredients
                    for(int ing = 0; ing < allClient[i].desiredPlate.ingredientNumber; ing++)
                    {
                        if (!enteringPlate.ContainsIngredient(allClient[i].desiredPlate.ingredients[ing]))
                        {
                            plateFound = -1;
                            return false;
                        }
                    }
                    plateFound = i;
                    return true;
                }
            }
        }

        for (int i = 0; i < _currentClientNumber; i++)
        {
            allClient[i].WrongPlate();
        }

        plateFound = -1;
        return false;
    }
    #endregion
}
