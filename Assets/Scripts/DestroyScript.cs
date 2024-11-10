using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    public GameObject _gameObject;
    private void OnTriggerEnter(Collider other)
    {
        GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<PlatformScript>()._mainPack.SetActive(true);
        Destroy(_gameObject);
    }
}
