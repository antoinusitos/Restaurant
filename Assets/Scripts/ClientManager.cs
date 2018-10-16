using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : BaseManager 
{
    #region Public Fields
    public float timeToSpawnClient = 5.0f;
    public GameObject clientPrefab = null;
    public Transform entry = null;
    public int totalClientNumber = 10;
    #endregion

    #region Private Fields
    private int _currentBusyTableNumber = 0;
    private bool _canSpawnClient = false;
    private float _currentTimeToSpawnClient = 0.0f;
    private Table[] _places = null;
    private List<Client> _allClients = new List<Client>();
    #endregion

    #region Unity Methods
    private void Start()
    {
        _places = FindObjectsOfType<Table>();
        _places = Data.ShuffleArray(ref _places);

        UIManager.GetInstance().ChangeClientsText(totalClientNumber);
    }
 
    private void Update()
    {
        if (!_canSpawnClient || _currentBusyTableNumber >= _places.Length)
        {
            _canSpawnClient = MenuManager.GetInstance().GetReadyToPlay();
            return;
        }

        _currentTimeToSpawnClient += Time.deltaTime;
        if(totalClientNumber > 0 && _currentTimeToSpawnClient >= timeToSpawnClient)
        {
            _currentTimeToSpawnClient = 0;
            SpawnClient();
        }

        if(totalClientNumber == 0 && _allClients.Count == 0)
        {
            //END
            GameManager.GetInstance().End();
        }
    }
    #endregion

    #region Public Methods
    public void SetCanSpawnClient(bool newState)
    {
        _canSpawnClient = newState;
    }

    public void TableLeave()
    {
        _currentBusyTableNumber--;
    }

    public void ClientLeave(Client c)
    {
        _allClients.Remove(c);
    }
    #endregion

    #region Private Methods
    private void SpawnClient()
    {
        Table t = FindTable();

        if (t != null)
        {
            _currentBusyTableNumber++;

            int rand = Random.Range(1, 3);

            if (rand > totalClientNumber) rand = totalClientNumber;

            for (int i = 0; i < rand; i++)
            {
                GameObject go = Instantiate(clientPrefab, entry.position, Quaternion.identity);
                Client c = go.GetComponent<Client>();
                _allClients.Add(c);
                c.Init();
                c.SetTarget(t.allChair[i]);
                totalClientNumber--;
            }
            UIManager.GetInstance().ChangeClientsText(totalClientNumber);

        }
    }

    private Table FindTable()
    {
        for (int i = 0; i < _places.Length; i++)
        {
            if (!_places[i].used)
            {
                _places[i].used = true;
                return _places[i];
            }
        }

        return null;
    }
    #endregion

        // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static ClientManager _instance;

    public static ClientManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
