using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamepadManager : BaseManager
{

    private float _triggerDeadZone = 0.5f;
    private float _stickDeadZone = 0.05f;

    private PlayerIndex _playerIndex;
    private GamePadState _state;
    private GamePadState _prevState;

    private PlayerIndex _playerIndex2;
    private GamePadState _state2;
    private GamePadState _prevState2;

    private PlayerIndex _playerIndex3;
    private GamePadState _state3;
    private GamePadState _prevState3;

    private PlayerIndex _playerIndex4;
    private GamePadState _state4;
    private GamePadState _prevState4;

    void Update()
    {
        // player 1

        _playerIndex = (PlayerIndex)0;
        _prevState = _state;
        _state = GamePad.GetState(_playerIndex);

        // player 2

        _playerIndex2 = (PlayerIndex)1;
        _prevState2 = _state2;
        _state2 = GamePad.GetState(_playerIndex2);

        // player 3

        _playerIndex3 = (PlayerIndex)2;
        _prevState3 = _state3;
        _state3 = GamePad.GetState(_playerIndex3);

        // player 4

        _playerIndex4 = (PlayerIndex)3;
        _prevState4 = _state4;
        _state4 = GamePad.GetState(_playerIndex4);
    }

    public void Vibration(int index)
    {
        GamePad.SetVibration((PlayerIndex)index-1, 1.0f, 1.0f);
    }

    public float GetStickPosX(int indexPlayer)
    {
        if(indexPlayer == 1)
        {
            return _state.ThumbSticks.Left.X;
        }
        else if (indexPlayer == 2)
        {
            return _state2.ThumbSticks.Left.X;
        }
        else if (indexPlayer == 3)
        {
            return _state3.ThumbSticks.Left.X;
        }
        else
        {
            return _state4.ThumbSticks.Left.X;
        }
    }

    public float GetStickPosY(int indexPlayer)
    {
        if (indexPlayer == 1)
        {
            return _state.ThumbSticks.Left.Y;
        }
        else if (indexPlayer == 2)
        {
            return _state2.ThumbSticks.Left.Y;
        }
        else if (indexPlayer == 3)
        {
            return _state3.ThumbSticks.Left.Y;
        }
        else
        {
            return _state4.ThumbSticks.Left.Y;
        }
    }

    public bool RightTriggerPressed(int indexPlayer)
    {
        if (indexPlayer == 1)
        {
            return _state.Triggers.Right >= _triggerDeadZone ? true : false;
        }
        else if (indexPlayer == 2)
        {
            return _state2.Triggers.Right >= _triggerDeadZone ? true : false;
        }
        else if (indexPlayer == 3)
        {
            return _state3.Triggers.Right >= _triggerDeadZone ? true : false;
        }
        else
        {
            return _state4.Triggers.Right >= _triggerDeadZone ? true : false;
        }
    }

    public bool LeftTriggerPressed(int indexPlayer)
    {
        if (indexPlayer == 1)
        {
            return _state.Triggers.Left >= _triggerDeadZone ? true : false;
        }
        else if (indexPlayer == 2)
        {
            return _state2.Triggers.Left >= _triggerDeadZone ? true : false;
        }
        else if (indexPlayer == 3)
        {
            return _state3.Triggers.Left >= _triggerDeadZone ? true : false;
        }
        else
        {
            return _state4.Triggers.Left >= _triggerDeadZone ? true : false;
        }
    }

    public bool BButtonPressed(int indexPlayer)
    {
        if (indexPlayer == 1)
        {
            if (_prevState.Buttons.B == ButtonState.Released && _state.Buttons.B == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else if (indexPlayer == 2)
        {
            if (_prevState2.Buttons.B == ButtonState.Released && _state2.Buttons.B == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else if (indexPlayer == 3)
        {
            if (_prevState3.Buttons.B == ButtonState.Released && _state3.Buttons.B == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else
        {
            if (_prevState4.Buttons.B == ButtonState.Released && _state4.Buttons.B == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
    }

    public bool AButtonPressed(int indexPlayer)
    {
        if (indexPlayer == 1)
        {
            if (_prevState.Buttons.A == ButtonState.Released && _state.Buttons.A == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else if (indexPlayer == 2)
        {
            if (_prevState2.Buttons.A == ButtonState.Released && _state2.Buttons.A == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else if (indexPlayer == 3)
        {
            if (_prevState3.Buttons.A == ButtonState.Released && _state3.Buttons.A == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else
        {
            if (_prevState4.Buttons.A == ButtonState.Released && _state4.Buttons.A == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
    }

    public bool XButtonPressed(int indexPlayer)
    {
        if (indexPlayer == 1)
        {
            if (_prevState.Buttons.X == ButtonState.Released && _state.Buttons.X == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else if (indexPlayer == 2)
        {
            if (_prevState2.Buttons.X == ButtonState.Released && _state2.Buttons.X == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else if (indexPlayer == 3)
        {
            if (_prevState3.Buttons.X == ButtonState.Released && _state3.Buttons.X == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else
        {
            if (_prevState4.Buttons.X == ButtonState.Released && _state4.Buttons.X == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
    }

    public bool YButtonPressed(int indexPlayer)
    {
        if (indexPlayer == 1)
        {
            if (_prevState.Buttons.Y == ButtonState.Released && _state.Buttons.Y == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else if (indexPlayer == 2)
        {
            if (_prevState2.Buttons.Y == ButtonState.Released && _state2.Buttons.Y == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else if (indexPlayer == 3)
        {
            if (_prevState3.Buttons.Y == ButtonState.Released && _state3.Buttons.Y == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        else
        {
            if (_prevState4.Buttons.Y == ButtonState.Released && _state4.Buttons.Y == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
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
