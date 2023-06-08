using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private float _increaseOffsetY = 4f;
    private float _increaseOffsetZ = 2f;
    private Vector3 _intialPos;

    private void OnEnable()
    {
        Player.IncreaseCameraHeight += Increase;
        Player.DecreaseCameraHeight += Decrease;
    }

    private void Start()
    {
        _intialPos = transform.position;
    }

    private void Increase()
    {
        float yVal = transform.position.y;
        float zVal = transform.position.z;
        yVal += _increaseOffsetY;
        zVal += _increaseOffsetZ;
        transform.position = new Vector3(_intialPos.x, yVal, zVal);
    }

    private void Decrease()
    {
        float yVal = transform.position.y;
        float zVal = transform.position.z;
        yVal -= _increaseOffsetY;
        zVal -= _increaseOffsetZ;
        transform.position = new Vector3(_intialPos.x, yVal, zVal);
    }

    private void OnDisable()
    {
        Player.IncreaseCameraHeight -= Increase;
        Player.DecreaseCameraHeight -= Decrease;
    }
}
