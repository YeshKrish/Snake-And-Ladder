using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _currentPosition = 0;
    private bool _hasMovedOnce = false;
    private float _camHeightOffset = 4.5f;
    private float _nextCamPos = 0f;
    private float _prevCamPos = 0f;
    private Vector3 _intialPos;

    private bool _movedBack = false;

    public static event Action IncreaseCameraHeight;
    public static event Action DecreaseCameraHeight;

    private void OnEnable()
    {
        Dice.PlayerMove += MoveSteps;
        Ladder.ClimbLadder += MoveForward;
        Snake.FallSnake += MoveBackWard;
    }

    private void Start()
    {
        _hasMovedOnce = false;
        _intialPos = transform.position;
        _nextCamPos = _intialPos.y + _camHeightOffset;
    }

    private void Update()
    {
        Debug.Log("trans" + transform.position.y + "next" + _nextCamPos);
        if (transform.position.y > _nextCamPos)
        {
            _nextCamPos += _camHeightOffset;
            IncreaseCameraHeight?.Invoke();
        }

        if (_movedBack)
        {
            _prevCamPos = transform.position.y - _camHeightOffset;
            _nextCamPos += _prevCamPos;
            DecreaseCameraHeight?.Invoke();
            _movedBack = false;
        }
    }

    private void MoveSteps(int moves)
    {

        if (!_hasMovedOnce)
        {
            _currentPosition = 0;
        }
        else
        {
            moves = moves + 1;
        }
        _currentPosition += moves;

        Debug.Log("Curr" + _currentPosition + "move" + moves);
        transform.position = new Vector3(GameManager.Instance.Lands[_currentPosition].transform.position.x, GameManager.Instance.Lands[_currentPosition].transform.position.y + 2, GameManager.Instance.Lands[_currentPosition].transform.position.z);
        _hasMovedOnce = true;
    }
    private void MoveForward(int moves)
    {
        _currentPosition = moves;
        transform.position = new Vector3(GameManager.Instance.Lands[moves].transform.position.x, GameManager.Instance.Lands[moves].transform.position.y + 2, GameManager.Instance.Lands[moves].transform.position.z);
    }

    private void MoveBackWard(int moves)
    {
        _movedBack = true;
        _currentPosition = moves;
        transform.position = new Vector3(GameManager.Instance.Lands[moves].transform.position.x, GameManager.Instance.Lands[moves].transform.position.y + 2, GameManager.Instance.Lands[moves].transform.position.z);
    }

    private void OnDisable()
    {
        Dice.PlayerMove -= MoveSteps;
        Ladder.ClimbLadder -= MoveSteps;
    }
}
