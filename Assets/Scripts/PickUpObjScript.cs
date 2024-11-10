using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjScript : MonoBehaviour
{
    public int _id;

    public void SpawnItem(int id)
    {
        _id = id;
        if (id == 1)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        else if (id == 2)
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        else if (id == 3)
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        else if (id == 4)
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject scriptObj = GameObject.FindGameObjectWithTag("ScriptObj");

            if (_id == 1)
            {
                scriptObj.GetComponent<Saves>()._coins++;
            }

            if (_id == 2 && !other.GetComponent<PlController>().canDoubleJump)
                other.GetComponent<PlController>().canDoubleJump = true;
            else if (_id == 2 && other.GetComponent<PlController>().canDoubleJump)
                scriptObj.GetComponent<Saves>()._coins += 10;

            if (_id == 3 && !other.GetComponent<PlController>().canFly)
            {
                other.GetComponent<PlController>().canFly = true;
                other.GetComponent<PlController>().gravity = other.GetComponent<PlController>().gravityFly;
            }
            else if (_id == 3 && other.GetComponent<PlController>().canFly)
                scriptObj.GetComponent<Saves>()._coins += 10;

            scriptObj.GetComponent<AudioScript>().DoAudio("pickup");
            Destroy(gameObject);
        }
    }
}
