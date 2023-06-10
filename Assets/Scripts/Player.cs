using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private GameObject _WinScreen;

    private int _nextPosition = 0;
    private bool _hasMovedOnce = false;
    private float _camHeightOffset = 4.5f;
    private float _nextCamPos = 0f;
    private float _prevCamPos = 0f;
    private Vector3 _intialPos;


    private float _intialXDistanceBetweenPlayerAndCamera = 0f;
    private float _intialYDistanceBetweenPlayerAndCamera = 0f;
    private float _intialZDistanceBetweenPlayerAndCamera = 0f;

    private bool _movedBack = false;

    public static event Action<Vector3> IncreaseCameraHeight;
    public static event Action<Vector3> DecreaseCameraHeight;

    private void OnEnable()
    {
        Dice.PlayerMove += MoveSteps;
        Ladder.ClimbLadder += MoveForward;
        Snake.FallSnake += MoveBackWard;
    }

    private void Start()
    {
        //finding intial y distance
        _intialXDistanceBetweenPlayerAndCamera = MathF.Abs(transform.position.x - _camera.transform.position.x);
        _intialYDistanceBetweenPlayerAndCamera = MathF.Abs(transform.position.y - _camera.transform.position.y);
        _intialZDistanceBetweenPlayerAndCamera = MathF.Abs(transform.position.z - _camera.transform.position.z);

        _hasMovedOnce = false;
        _intialPos = transform.position;
        _nextCamPos = _intialPos.y + _camHeightOffset;
    }

    private void Update()
    {
        //Debug.Log("trans" + transform.position.y + "next" + _nextCamPos);
        if (transform.position.y > _nextCamPos)
        {
            _nextCamPos += _camHeightOffset;
            //IncreaseCameraHeight?.Invoke();
        }

        if (_movedBack)
        {
            _prevCamPos = transform.position.y - _camHeightOffset;
            _nextCamPos += _prevCamPos;
            //DecreaseCameraHeight?.Invoke();
            _movedBack = false;
            //MovedBackFalse();
        }
    }

    //async void MovedBackFalse()
    //{
    //    await System.Threading.Tasks.Task.Delay(100);

    //}

    private void MoveSteps(int moves)
    {
        try
        {
            int previousPosition = _nextPosition;

            if (!_hasMovedOnce)
            {
                _nextPosition = 0;
            }
            else
            {
                moves = moves + 1;
            }

            _nextPosition += moves;

            Debug.Log("Curr" + _nextPosition + "move" + moves);

            if(_nextPosition + 1 == 100)
            {
                transform.position = new Vector3(GameManager.Instance.Lands[_nextPosition].transform.position.x, GameManager.Instance.Lands[_nextPosition].transform.position.y + 2, GameManager.Instance.Lands[_nextPosition].transform.position.z);
                CalculateYDistance(transform.position);
                _hasMovedOnce = true;
                DelayedWin();
            }

            if (_nextPosition >= 96 && _nextPosition <= 99 && moves >= 1 && moves <= 5)
            {
                transform.position = new Vector3(GameManager.Instance.Lands[_nextPosition].transform.position.x, GameManager.Instance.Lands[_nextPosition].transform.position.y + 2, GameManager.Instance.Lands[_nextPosition].transform.position.z);
                CalculateYDistance(transform.position);
                _hasMovedOnce = true;
            }
            else if (_nextPosition <= 95)
            {
                transform.position = new Vector3(GameManager.Instance.Lands[_nextPosition].transform.position.x, GameManager.Instance.Lands[_nextPosition].transform.position.y + 2, GameManager.Instance.Lands[_nextPosition].transform.position.z);
                CalculateYDistance(transform.position);
                _hasMovedOnce = true;
            }
            else
            {
                _nextPosition = previousPosition; // Reset the current position to the previous position
                throw new Exception("Invalid move");
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Roll the dice again: " + ex.Message);
            // Display your message or take any other appropriate action
        }
    }


    private void MoveForward(int moves)
    {
        _nextPosition = moves;
        transform.position = new Vector3(GameManager.Instance.Lands[moves].transform.position.x, GameManager.Instance.Lands[moves].transform.position.y + 2, GameManager.Instance.Lands[moves].transform.position.z);
        LateCalculateForward();
    }

    async void LateCalculateForward()
    {
        await System.Threading.Tasks.Task.Delay(50);
        CalculateYDistance(transform.position);
    }

    private void MoveBackWard(int moves)
    {
        _movedBack = true;
        _nextPosition = moves;
        transform.position = new Vector3(GameManager.Instance.Lands[moves].transform.position.x, GameManager.Instance.Lands[moves].transform.position.y + 2, GameManager.Instance.Lands[moves].transform.position.z);
        LateCalculateBackward();
    }
    async void LateCalculateBackward()
    {
        await System.Threading.Tasks.Task.Delay(50);
        CalculateYDistance(transform.position);
    }

    async void DelayedWin()
    {
        await System.Threading.Tasks.Task.Delay(1000);
        _WinScreen.SetActive(true);
    }


    private void CalculateYDistance(Vector3 currentPos)
    {
        float newCameraAndPlayerDistanceX = Mathf.Abs(currentPos.x - _camera.transform.position.y);
        float newCameraAndPlayerDistanceY = Mathf.Abs(currentPos.y - _camera.transform.position.y);
        float newCameraAndPlayerDistanceZ = Mathf.Abs(currentPos.z - _camera.transform.position.z);
        Debug.Log("newCamera" + newCameraAndPlayerDistanceY + "intial" + _intialYDistanceBetweenPlayerAndCamera);
        if(newCameraAndPlayerDistanceY < _intialYDistanceBetweenPlayerAndCamera)
        {
            float totalDiffX = Mathf.Abs(newCameraAndPlayerDistanceX - _intialXDistanceBetweenPlayerAndCamera);
            float totalDiffY = Mathf.Abs(newCameraAndPlayerDistanceY - _intialYDistanceBetweenPlayerAndCamera);
            float totalDiffZ = Mathf.Abs(newCameraAndPlayerDistanceZ - _intialZDistanceBetweenPlayerAndCamera);
            Vector3 total = new Vector3(totalDiffX, totalDiffY, totalDiffZ);
            IncreaseCameraHeight?.Invoke(total);
        }
        if(newCameraAndPlayerDistanceY > _intialYDistanceBetweenPlayerAndCamera)
        {
            float totalDiffX = Mathf.Abs(newCameraAndPlayerDistanceX - _intialXDistanceBetweenPlayerAndCamera);
            float totalDiffY = Mathf.Abs(newCameraAndPlayerDistanceY - _intialYDistanceBetweenPlayerAndCamera);
            float totalDiffZ = Mathf.Abs(newCameraAndPlayerDistanceZ - _intialZDistanceBetweenPlayerAndCamera);
            Vector3 total = new Vector3(totalDiffX, totalDiffY, totalDiffZ);
            DecreaseCameraHeight?.Invoke(total);
        }
    }
    private void OnDisable()
    {
        Dice.PlayerMove -= MoveSteps;
        Ladder.ClimbLadder -= MoveForward;
        Snake.FallSnake -= MoveBackWard;
    }
}
