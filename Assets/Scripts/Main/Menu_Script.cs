using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    public bool _onPause;
    public GameObject _panMenu;
    public GameObject _panUI;
    public GameObject _panDie;
    public GameObject _panWin;
    private CamController _camController;
    void Start()
    {
        Time.timeScale = 1f;
        _camController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CamController>();
        _panDie.SetActive(false);
        _camController.canMoveMouse = true;
        _panMenu.SetActive(false);
        _panWin.SetActive(false);
        _panUI.SetActive(true);
        _onPause = false;
        GetComponent<Saves>().Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.P))
        {
            if (!_onPause)
                Pause();
            else
                UnPause();
        }
    }

    public void Pause()
    {
        _camController.canMoveMouse = false;
        _panMenu.SetActive(true);
        _panUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        _onPause = true;
    }

    public void UnPause()
    {
        _camController.canMoveMouse = true;
        _panMenu.SetActive(false);
        _panUI.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        _onPause = false;
        GetComponent<Saves>().Save();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
    public void Die()
    {
        Time.timeScale = 0f;
        _camController.canMoveMouse = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _panDie.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        _camController.canMoveMouse = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _panWin.SetActive(true);
    }
}
