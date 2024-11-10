using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSwordScript : MonoBehaviour
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _anim.SetInteger("curAnim", 2);
            _anim.SetBool("goAnim", true);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            _anim.SetInteger("curAnim", 0);
            _anim.SetBool("goAnim", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _anim.SetInteger("curAnim", 3);
            _anim.SetBool("goAnim", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _anim.SetInteger("curAnim", 0);
            _anim.SetBool("goAnim", false);
        }
    }
}
