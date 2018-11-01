using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamepadManager : BaseManager
{
    public bool useGamepad = false;
    public int playerNumber = 1;


    private float _triggerDeadZone = 0.5f;
    private float _stickDeadZone = 0.05f;

    private PlayerIndex[] _playerIndex = null;
    private GamePadState[] _states = null;
    private GamePadState[] _prevStates = null;

    private void Start()
    {
        _playerIndex = new PlayerIndex[playerNumber];
        _states = new GamePadState[playerNumber];
        _prevStates = new GamePadState[playerNumber];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            useGamepad = !useGamepad;
        }

        for(int i = 0; i < playerNumber; i++)
        {
            _playerIndex[i] = (PlayerIndex)i;
            _prevStates[i] = _states[i];
            _states[i] = GamePad.GetState(_playerIndex[i]);
        }
    }

    public void Vibration(int index)
    {
        GamePad.SetVibration((PlayerIndex)index-1, 1.0f, 1.0f);
    }

    public float GetStickPosX(int indexPlayer)
    {
        return _states[indexPlayer].ThumbSticks.Left.X;
    }

    public float GetStickPosY(int indexPlayer)
    {
        return _states[indexPlayer].ThumbSticks.Left.Y;
    }

    public bool RightTriggerPressed(int indexPlayer)
    {
        return _states[indexPlayer].Triggers.Right >= _triggerDeadZone ? true : false;
    }

    public bool LeftTriggerPressed(int indexPlayer)
    {
        return _states[indexPlayer].Triggers.Left >= _triggerDeadZone ? true : false;
    }

    public bool BButtonPressed(int indexPlayer)
    {
        if (_prevStates[indexPlayer].Buttons.B == ButtonState.Released && _states[indexPlayer].Buttons.B == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public bool AButtonPressed(int indexPlayer)
    {
        if (_prevStates[indexPlayer].Buttons.A == ButtonState.Released && _states[indexPlayer].Buttons.A == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public bool XButtonPressed(int indexPlayer)
    {
        if (_prevStates[indexPlayer].Buttons.X == ButtonState.Released && _states[indexPlayer].Buttons.X == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public bool YButtonPressed(int indexPlayer)
    {
        if (_prevStates[indexPlayer].Buttons.Y == ButtonState.Released && _states[indexPlayer].Buttons.Y == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public float GetStickDeadZone()
    {
        return _stickDeadZone;
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static GamepadManager _instance;

    public static GamepadManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
