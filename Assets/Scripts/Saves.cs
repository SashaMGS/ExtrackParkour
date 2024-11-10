using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saves : MonoBehaviour
{
    public int _difficult;
    public int _coins;
    public float _sensivity;
    public float _audio;
    public int _fov;
    public string _lang;
    public int _maxHeight;

    Transform _playerTransform;
    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        LoadSave();

        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CamController>().sensivity = _sensivity;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().fieldOfView = _fov;
    }

    private void Update()
    {
        if ((int)(_playerTransform.transform.position.y - 1.58) > _maxHeight)
        {
            _maxHeight = (int)(_playerTransform.transform.position.y - 1.58);
            PlayerPrefs.SetInt("maxHeight", _maxHeight);
            PlayerPrefs.Save();
        }
    }

    public void SaveEmpty()
    {
        PlayerPrefs.SetString("canFly", "False");
        PlayerPrefs.SetString("canDoubleJump", "False");
        PlayerPrefs.Save();
    }

    public void Save()
    {
        PlayerPrefs.SetString("lang", _lang);
        PlayerPrefs.SetInt("difficult", _difficult);
        PlayerPrefs.SetInt("coins", _coins);
        PlayerPrefs.SetInt("fov", _fov);
        PlayerPrefs.SetString("canFly", GameObject.FindGameObjectWithTag("Player").GetComponent<PlController>().canFly.ToString());
        PlayerPrefs.SetString("canDoubleJump", GameObject.FindGameObjectWithTag("Player").GetComponent<PlController>().canDoubleJump.ToString());
        PlayerPrefs.SetFloat("sensivity", _sensivity);
        PlayerPrefs.SetFloat("audio", _audio);
        PlayerPrefs.SetInt("maxHeight", _maxHeight);
        PlayerPrefs.Save();
        Debug.Log("Save successfully");
    }

    public void LoadSave()
    {
        _lang = PlayerPrefs.GetString("lang", "ru");
        _difficult = PlayerPrefs.GetInt("difficult");
        _coins = PlayerPrefs.GetInt("coins", 0);
        _sensivity = PlayerPrefs.GetFloat("sensivity", 1f);
        _audio = PlayerPrefs.GetFloat("audio", 1f);
        _fov = PlayerPrefs.GetInt("fov", 90);
        _maxHeight = PlayerPrefs.GetInt("maxHeight", 0);
        PlController _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlController>();
        if (PlayerPrefs.GetString("canFly", "False") == "True")
            _player.canFly = true;
        if (PlayerPrefs.GetString("canDoubleJump", "False") == "True")
            _player.canDoubleJump = true;
    }
}
