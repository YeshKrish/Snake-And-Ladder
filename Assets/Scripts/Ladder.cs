using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    private int _endPlace;

    public static event Action<int> ClimbLadder;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning(other.gameObject.transform.name);
        if (other.gameObject.CompareTag("Player"))
        {
            DelayClimb();
            
        }
    }

    async void DelayClimb()
    {
        await Task.Delay(500);
        ClimbLadder?.Invoke(_endPlace);
    }
}
