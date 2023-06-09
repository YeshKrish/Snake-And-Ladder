using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    [SerializeField]
    private GameObject _winScreen;

    public static UIManger Instance;

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

    public void Retry()
    {
        if (_winScreen.activeSelf)
        {
            _winScreen.SetActive(false);
        }
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
