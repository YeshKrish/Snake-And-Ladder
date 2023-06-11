using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Winner();
    }

    async void Winner()
    {
        await Task.Delay(200);
        GameManager.Instance.GameWon();
    }
}
