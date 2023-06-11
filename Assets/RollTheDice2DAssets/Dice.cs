using System.Collections;
using System;
using UnityEngine;
using System.Threading.Tasks;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;

    private SpriteRenderer rend;

    public static event Action<int> PlayerMove;

    private bool _isMouseClicked = false;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    private void Update()
    {
        if (_isMouseClicked || Player._isMoving)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }


    private void OnMouseDown()
    {
        if (!_isMouseClicked && !Player._isMoving)
        {
            _isMouseClicked = true;
            StartCoroutine(RollTheDice());
        }
    }
    private IEnumerator RollTheDice()
    {

        int randomDiceSide = 0;

        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = UnityEngine.Random.Range(0, 5);

            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);


        }

        PlayerMove?.Invoke(randomDiceSide);

        _isMouseClicked = false;
    }
}
