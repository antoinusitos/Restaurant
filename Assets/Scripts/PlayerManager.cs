using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager
{
    private PlayerMovement[] _playersMovement = null;

    private void Start()
    {
        _playersMovement = FindObjectsOfType<PlayerMovement>();
    }

    public void BlockAllPlayers(bool newState)
    {
        for(int i = 0; i < _playersMovement.Length; i++)
        {
            _playersMovement[i].SetCanMove(!newState);
        }
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static PlayerManager _instance;

    public static PlayerManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
