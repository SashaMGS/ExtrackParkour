using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    private Saves _saves;
    public short _id; // 0 - double jump 1 - Fly
    public int _price;

    private void Start()
    {
        _saves = GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<Saves>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _saves._coins >= _price)
        {
            PlController _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlController>();
            if (_id == 0 && !_player.canDoubleJump)
            {
                _saves._coins -= _price;
                _player.canDoubleJump = true;
                GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<AudioScript>().DoAudio("click");
                Destroy(gameObject);
            }
            else if (_id == 1 && !_player.canFly)
            {
                _saves._coins -= _price;
                _player.canFly = true;
                _player.gravity = _player.gravityFly;
                GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<AudioScript>().DoAudio("click");
                Destroy(gameObject);
            }
            _saves.Save();
        }
        else if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<AudioScript>().DoAudio("not");
        }
    }
}
