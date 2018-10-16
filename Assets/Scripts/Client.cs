using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Client : MonoBehaviour 
{
    #region Public Fields
    public PlateIngredients desiredPlate;
    public float timeMinWait = 30;
    public float timeMaxWait = 60;
    public float timeMinChoice = 0;
    public float timeMaxChoice = 10;
    public GameObject choiceUI = null;
    public GameObject servedUI = null;
    public int scoreMultiplier = 100;
    public float malus = 0.1f;
    #endregion

    #region Private Fields
    private bool _readyToBeServed = false;
    private bool _leaving = false;
    private bool _sit = false;
    private Chair _target = null;
    private NavMeshAgent _agent = null;
    private float _distToSit = 1.5f;
    private float _choiceTime = 0;
    private float _currentChoiceTime = 0;
    private float _waitingTime = 0;
    private float _timeMaxToWait = 0;
    #endregion

    #region Unity Methods
    private void Start()
    {
    }
 
    private void Update()
    {
        if (_leaving)
        {
            float dist = Vector3.Distance(ClientManager.GetInstance().entry.position, transform.position);
            if (dist <= _distToSit)
            {
                ClientManager.GetInstance().ClientLeave(this);
                Destroy(gameObject);
            }
            return;
        }

        if(_target != null && !_sit)
        {
            float dist = Vector3.Distance(_target.enterExitPlace.position, transform.position);
            if(dist <= _distToSit)
            {
                _target.table.ClientArrive(this);
                choiceUI.SetActive(true);
                _sit = true;
                _agent.enabled = false;
                transform.position = _target.clientPlace.position;
                transform.rotation = Quaternion.Euler(_target.clientPlace.forward);
                _choiceTime = Random.Range(timeMinChoice, timeMaxChoice);
            }
        }
        if(_sit && !_readyToBeServed)
        {
            choiceUI.SetActive(true);
            _timeMaxToWait = Random.Range(timeMinWait, timeMaxWait);
            _currentChoiceTime += Time.deltaTime;
            if (_currentChoiceTime >= _choiceTime)
            {
                choiceUI.SetActive(false);
                _readyToBeServed = true;
                _target.ActivateClientMeal(true);
                _target.clientMealUI.GetComponent<MealUI>().ShowIngredients(desiredPlate.ingredients);
            }
        }

        if (_readyToBeServed)
        {
            _waitingTime += Time.deltaTime;
            _target.ShowTimeWait(_waitingTime / _timeMaxToWait);
            if (_waitingTime >= _timeMaxToWait)
            {
                Leave();
            }
        }
    }
    #endregion
 
	#region Public Methods
    public void CreatePlate()
    {
        int nbIngredients = Random.Range(1, 5);
        Ingredient[] ingredients = new Ingredient[nbIngredients];
        Ingredient[] menu = MenuManager.GetInstance().GetMenu();
        menu = Data.ShuffleArray(ref menu);
        for (int i = 0; i < nbIngredients; i++)
        {
            ingredients[i] = menu[i];
        }
        desiredPlate = new PlateIngredients(nbIngredients, ingredients);
    }
    
    public void Init()
    {
        _agent = GetComponent<NavMeshAgent>();
        CreatePlate();
    }

    public void SetTarget(Chair c)
    {
        _target = c;
        _agent.SetDestination(c.enterExitPlace.position);
    }

    public void Leave()
    {
        _target.ActivateClientMeal(false);
        servedUI.SetActive(false);
        _leaving = true;
        _agent.enabled = true;
        transform.position = _target.enterExitPlace.position;
        _agent.SetDestination(ClientManager.GetInstance().entry.position);
        int score = (int)(desiredPlate.ingredientNumber * scoreMultiplier * ( 1 - (_waitingTime / _timeMaxToWait)));
        ScoreManager.GetInstance().AddScore(score);
    }

    public void Served()
    {
        _target.ActivateClientMeal(false);
        choiceUI.SetActive(false);
        servedUI.SetActive(true);
        desiredPlate.served = true;
    }

    public void WrongPlate()
    {
        _waitingTime += _timeMaxToWait * malus;
    }

    public bool GetReadyToBeServed()
    {
        return _readyToBeServed;
    }

    public bool GetServed()
    {
        return desiredPlate.served;
    }
    #endregion

    #region Private Methods
    #endregion
}
