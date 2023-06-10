using System.Collections;
using System;
using UnityEngine;
using System.Threading.Tasks;

public class Dice : MonoBehaviour
{

    private Sprite[] diceSides;

    private SpriteRenderer rend;

    public static event Action<int> PlayerMove;

    private int _previousMove = 0;

    private bool _isDiceRolling = false;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }


    private void OnMouseDown()
    {
        if (!_isDiceRolling)
        {
            StartCoroutine(RollTheDice());
        }
    }
    private IEnumerator RollTheDice()
    {
        _isDiceRolling = true;

        int randomDiceSide = 0;

        int finalSide = 0;

        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = UnityEngine.Random.Range(0, 5);

            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);


        }

        _previousMove += randomDiceSide;

        PlayerMove?.Invoke(randomDiceSide);
        finalSide = randomDiceSide + 1;

        _isDiceRolling = false;
    }
}
