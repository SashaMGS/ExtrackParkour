using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEndLevel : MonoBehaviour
{
    public float _speedUp = 1f;
    public Transform _playerTransform;

    private void Start()
    {
        if (_playerTransform == null)
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (_playerTransform.position.y <= transform.position.y)
        {
            GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<Saves>().SaveEmpty();
            GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<Menu_Script>().Die();
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f * _speedUp * Time.deltaTime, transform.position.z);
    }
}
