using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _nextPosition = 0;
    private bool _hasMovedOnce = false;

    int previousPosition;

    public float timeStartedLerping;
    public float lerpTime;

    private CapsuleCollider capsuleCollider;

    private float _lerpDuration = 1f; 
    public static bool _isMoving = false;

    private void OnEnable()
    {
        Dice.PlayerMove += MoveSteps;
        Ladder.ClimbLadder += MoveForward;
        Snake.FallSnake += MoveBackWard;
    }

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();

        _hasMovedOnce = false;
    }

    private void MoveSteps(int moves)
    {
        try
        {
            previousPosition = _nextPosition;

            if (!_hasMovedOnce)
            {
                _nextPosition = 0;
            }
            else
            {
                moves = moves + 1;
            }

            _nextPosition += moves;

            if (_nextPosition + 1 == 100)
            {
                StartCoroutine(MovePlayerSmoothly(GameManager.Instance.Lands[_nextPosition].transform.position));
            }
            else if (_nextPosition >= 96 && _nextPosition <= 99 && moves >= 1 && moves <= 5)
            {
                StartCoroutine(MovePlayerSmoothly(GameManager.Instance.Lands[_nextPosition].transform.position));
                _hasMovedOnce = true;
            }
            else if (_nextPosition <= 95)
            {
                StartCoroutine(MovePlayerSmoothly(GameManager.Instance.Lands[_nextPosition].transform.position));
                _hasMovedOnce = true;
            }
            else
            {
                _nextPosition = previousPosition;
                throw new Exception("Invalid move");
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Roll the dice again: " + ex.Message);
        }
    }

    private IEnumerator MovePlayerSmoothly(Vector3 targetPosition)
    {
        _isMoving = true;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        capsuleCollider.enabled = false;

        Vector3 newTargetPos = new Vector3(targetPosition.x, targetPosition.y + 2, targetPosition.z);

        
        if(startPosition.y != newTargetPos.y)
        {
            Vector3 endPosY = startPosition;
            endPosY.y = newTargetPos.y;

            while (elapsedTime < _lerpDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / _lerpDuration);

                transform.position = Vector3.Lerp(startPosition, endPosY, t);
                yield return null;
            }
            elapsedTime = 0f;
        }

        startPosition = transform.position;
        while (elapsedTime < _lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _lerpDuration);

            transform.position = Vector3.Lerp(startPosition, newTargetPos, t);
            yield return null;
        }


        transform.position = newTargetPos;
        _isMoving = false;

        if (!_isMoving)
        {
            capsuleCollider.enabled = true;
        }
    }

    private void MoveForward(int moves)
    {
        previousPosition = _nextPosition+1;
        int distanceBetweenLands = Mathf.Abs(previousPosition - (moves + 1));
        _nextPosition = moves;
        StartCoroutine(MovePlayerSmoothly(GameManager.Instance.Lands[_nextPosition].transform.position));
    }


    private void MoveBackWard(int moves)
    {
        previousPosition = _nextPosition + 1;
        _nextPosition = moves;
        transform.position = new Vector3(GameManager.Instance.Lands[moves].transform.position.x, GameManager.Instance.Lands[moves].transform.position.y + 2, GameManager.Instance.Lands[moves].transform.position.z);
        _isMoving = false;
    }

    private void OnDisable()
    {
        Dice.PlayerMove -= MoveSteps;
        Ladder.ClimbLadder -= MoveForward;
        Snake.FallSnake -= MoveBackWard;
    }
}
