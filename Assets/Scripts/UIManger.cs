using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    public GameObject WinScreen;
    [SerializeField]
    private GameObject _rollTheDiceText;

    public static UIManger Instance;

    private float _lerpDuration = 0.5f;
    private float _scaleTimer = 0f;
    private bool _isScalingUp = true;
    private Vector3 _intialScale;
    private Vector3 _targetScale;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _intialScale = _rollTheDiceText.transform.localScale;
        _targetScale = new Vector3(0.8f, 0.8f, 0.8f);
        _rollTheDiceText.SetActive(true);
    }

    private void Update()
    {
        if (Player._isMoving)
        {
            _rollTheDiceText.SetActive(false);
        }
        else
        {
            _scaleTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_scaleTimer / _lerpDuration);

            if (_isScalingUp)
            {
                _rollTheDiceText.transform.localScale = Vector3.Lerp(_intialScale, _targetScale, t);
                _rollTheDiceText.GetComponent<TMPro.TMP_Text>().color = Color.Lerp(Color.red, Color.yellow, t);
            }
            else
            {
                _rollTheDiceText.transform.localScale = Vector3.Lerp(_targetScale, _intialScale, t);
                _rollTheDiceText.GetComponent<TMPro.TMP_Text>().color = Color.yellow;
            }

            if (_scaleTimer >= _lerpDuration)
            {
                _scaleTimer = 0f;
                _isScalingUp = !_isScalingUp;
            }

            _rollTheDiceText.SetActive(true);
        }
    }

    public void Retry()
    {
        if (WinScreen.activeSelf)
        {
            WinScreen.SetActive(false);
        }
        SceneManager.LoadScene(0);
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
