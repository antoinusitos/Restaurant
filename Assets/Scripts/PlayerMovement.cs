using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    private bool _canMove = true;

    private float _stickDeadZone;
    private int _indexPlayer;

    private Rigidbody _rigidBody;

    private float _speed = 10.0f;

    // Accessor
    private GamepadManager _gamepadManager;
    private RenderingRotation _renderingRotation;
    private CarryObject _carryObject;

    public Animator animator = null;

    private void Start()
    {
        _gamepadManager = GamepadManager.GetInstance();
        _indexPlayer = GetComponent<IndexPlayerController>().playerIndex;
        _stickDeadZone = _gamepadManager.GetStickDeadZone();
        _rigidBody = GetComponent<Rigidbody>();
        if (!_rigidBody)
        {
            Debug.LogError("NO RIGIDBODY ON THE OBJECT !");
        }

        _renderingRotation = GetComponentInChildren<RenderingRotation>(); // Because it's in the child
        if(!_renderingRotation)
        {
            Debug.LogError("NO RENDERING ROTATION IN THE CHILDREN !");
        }

        _carryObject = GetComponentInChildren<CarryObject>(); // Because it's in the child
        if (!_carryObject)
        {
            Debug.LogError("NO CARRY OBJECT IN THE CHILDREN !");
        }
    }

    private void Update()
    {
        if (_canMove)
        {
            Vector3 direction = Vector3.zero;

            //Go Right
            if (_gamepadManager.GetStickPosX(_indexPlayer) >= _stickDeadZone || Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right;
            }
            // Go Left
            else if (_gamepadManager.GetStickPosX(_indexPlayer) <= -_stickDeadZone || Input.GetKey(KeyCode.Q))
            {
                direction = Vector3.left;
            }

            //Go Top
            if (_gamepadManager.GetStickPosY(_indexPlayer) >= _stickDeadZone || Input.GetKey(KeyCode.Z))
            {
                direction += Vector3.forward;
            }
            // Go Down
            else if (_gamepadManager.GetStickPosY(_indexPlayer) <= -_stickDeadZone || Input.GetKey(KeyCode.S))
            {
                direction += Vector3.back;
            }

            if(direction != Vector3.zero)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Running", false);
            }

            // Apply velocity
            _rigidBody.velocity = direction.normalized * _speed;
            // Set the rotation of the renderer
            _renderingRotation.SetRotation(direction.normalized);

            if (_gamepadManager.AButtonPressed(_indexPlayer) || Input.GetKeyDown(KeyCode.E))
            {
                _carryObject.TryTakeObject();
            }
            if (_gamepadManager.BButtonPressed(_indexPlayer))
            {
            }
            if (_gamepadManager.YButtonPressed(_indexPlayer))
            {
            }
            if (_gamepadManager.XButtonPressed(_indexPlayer))
            {
                _gamepadManager.Vibration(_indexPlayer);
            }
        }
    }

    public void SetCanMove(bool newState)
    {
        _canMove = newState;
    }
}
