using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Lands = new List<GameObject>();
    public float CubeSize { get; private set; }

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        // Calculate the cube size based on the scale of the first cube in the list
        if (Lands.Count > 0)
        {
            CubeSize = Lands[0].transform.localScale.x;
        }
        else
        {
            // Set a default cube size if no cubes are in the list
            CubeSize = 1f;
        }
    }

    public void GameWon()
    {
        UIManger.Instance.WinScreen.SetActive(true);
    }
}