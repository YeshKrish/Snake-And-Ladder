using System;
using System.Threading.Tasks;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField]
    private int _endPlace;

    public static event Action<int> FallSnake;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning(other.gameObject.transform.name);
        if (other.gameObject.CompareTag("Player"))
        {
            DelayFall();

        }
    }

    async void DelayFall()
    {
        await Task.Delay(500);
        FallSnake?.Invoke(_endPlace);
    }
}
