using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<Saves>()._difficult++;
        GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<Saves>().Save();
        GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<Menu_Script>().Win();
    }
}
