using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //private float _increaseOffsetY = 6f;
    //private float _increaseOffsetZ = 3f;    
    //private float _decreaseOffsetY = -6f;
    //private float _decreaseOffsetZ = -3f;

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

    private void Increase(Vector3 changed)
    {
        float yVal = transform.position.y;
        float zVal = transform.position.z;
        yVal += changed.y;
        zVal += changed.z;
        transform.position = new Vector3(_intialPos.x, yVal, zVal-2);
    }

    private void Decrease(Vector3 changed)
    {
        float yVal = transform.position.y;
        float zVal = transform.position.z;
        yVal -= changed.y;
        zVal -= changed.z;
        transform.position = new Vector3(10.25f, yVal, zVal);
    }

    private void OnDisable()
    {
        Player.IncreaseCameraHeight -= Increase;
        Player.DecreaseCameraHeight -= Decrease;
    }
}
