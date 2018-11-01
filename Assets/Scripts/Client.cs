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
    public Animator animator = null;
    public Transform[] ingredientsShowingPlaces = null;
    public GameObject ingredientShowing = null;
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

    private GameObject _saladRepresentation = null;
    private GameObject _tomatoRepresentation = null;
    private GameObject _beanRepresentation = null;
    private GameObject _hamRepresentation = null;
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
                animator.SetBool("Sit", true);
                _agent.enabled = false;
                transform.position = _target.clientPlace.position;
                transform.rotation = _target.clientPlace.rotation;
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
                ingredientShowing.SetActive(true);
                choiceUI.SetActive(false);
                _readyToBeServed = true;
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
            switch(ingredients[i])
            {
                case Ingredient.BEANS:
                    {
                        Transform go = Instantiate(_beanRepresentation, transform.position, Quaternion.identity).transform;
                        go.SetParent(ingredientsShowingPlaces[i]);
                        go.localPosition = Vector3.zero;
                        break;
                    }
                case Ingredient.HAM:
                    {
                        Transform go = Instantiate(_hamRepresentation, transform.position, Quaternion.identity).transform;
                        go.SetParent(ingredientsShowingPlaces[i]);
                        go.localPosition = Vector3.zero;
                        break;
                    }
                case Ingredient.SALAD:
                    {
                        Transform go = Instantiate(_saladRepresentation, transform.position, Quaternion.identity).transform;
                        go.SetParent(ingredientsShowingPlaces[i]);
                        go.localPosition = Vector3.zero;
                        break;
                    }
                case Ingredient.TOMATO:
                    {
                        Transform go = Instantiate(_tomatoRepresentation, transform.position, Quaternion.identity).transform;
                        go.SetParent(ingredientsShowingPlaces[i]);
                        go.localPosition = Vector3.zero;
                        break;
                    }
            }
        }
        desiredPlate = new PlateIngredients(nbIngredients, ingredients);
    }
    
    public void Init()
    {
        _agent = GetComponent<NavMeshAgent>();
        _saladRepresentation = Resources.Load<GameObject>("SaladRepresentation");
        _tomatoRepresentation = Resources.Load<GameObject>("TomatoRepresentation");
        _beanRepresentation = Resources.Load<GameObject>("BeanRepresentation");
        _hamRepresentation = Resources.Load<GameObject>("HamRepresentation");
        CreatePlate();
    }

    public void SetTarget(Chair c)
    {
        _target = c;
        _agent.SetDestination(c.enterExitPlace.position);
    }

    public void Leave()
    {
        ingredientShowing.SetActive(false);
        animator.SetBool("Sit", false);
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
        ingredientShowing.SetActive(false);
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
