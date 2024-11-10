using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputScript : MonoBehaviour
{
    public bool _canUseKeys = true;
    void Update()
    {
        if (_canUseKeys)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game");
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("Game");
            }
        }
    }
}
