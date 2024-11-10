using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public Transform _playerTransform;
    public TMP_Text _heightTxt;
    public TMP_Text _maxHeightTxt;
    public TMP_Text _coinsTxt;
    public GameObject _doubleJumpObj;
    public GameObject _flyObj;

    private Saves _saves;

    public TMP_Text _sensTxt;
    public Slider _sensSlider;

    public TMP_Text _audioTxt;
    public Slider _audioSlider;

    public TMP_Text _fovTxt;
    public Slider _fovSlider;

    void Start()
    {
        if (_playerTransform != null)
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _saves = GetComponent<Saves>();

        _sensSlider.value = _saves._sensivity;
        _sensTxt.text = _saves._sensivity.ToString("0.0");

        _audioSlider.value = _saves._audio;
        _audioTxt.text = _saves._audio.ToString("0.0");

        _fovSlider.value = _saves._fov;
        _fovTxt.text = _saves._fov.ToString();

        GameUI();
        MenuUI();
    }

    void Update()
    {
        GameUI();
        MenuUI();
    }

    public void GameUI()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CamController>().sensivity = _saves._sensivity;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().fieldOfView = _saves._fov;

        _heightTxt.text = ((int)(_playerTransform.transform.position.y - 1.58)).ToString();

        _coinsTxt.text = _saves._coins.ToString();

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlController>().canDoubleJump)
            _doubleJumpObj.SetActive(true);
        else
            _doubleJumpObj.SetActive(false);

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlController>().canFly)
            _flyObj.SetActive(true);
        else
            _flyObj.SetActive(false);
    }

    public void MenuUI()
    {
        _saves._sensivity = _sensSlider.value;
        _sensTxt.text = _saves._sensivity.ToString("0.00");

        _saves._audio = _audioSlider.value;
        _audioTxt.text = _saves._audio.ToString("0.0");

        _saves._fov = (int)_fovSlider.value;
        _fovTxt.text = _saves._fov.ToString();

        _maxHeightTxt.text = _saves._maxHeight.ToString();
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
