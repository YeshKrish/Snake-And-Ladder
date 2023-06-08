using System.Collections;
using System;
using UnityEngine;

public class Dice : MonoBehaviour {

    private Sprite[] diceSides;

    private SpriteRenderer rend;

    public static event Action<int> PlayerMove;

    private int _previousMove = 0;

    private float _increaseOffsetY = 4f;
    private float _increaseOffsetZ = 2f;
    private Vector3 _intialPos;

    private void Start () {

        _intialPos = transform.position;
        rend = GetComponent<SpriteRenderer>();

        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
	}

    private void OnEnable()
    {
        Player.IncreaseCameraHeight += Increase;
        Player.DecreaseCameraHeight += Decrease;
    }

    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
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

        Debug.Log(finalSide);
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
