using UnityEngine;
using TMPro;

public class TranslateObj : MonoBehaviour
{
    private void Start()
    {
        UpdateLang();
    }
    public void UpdateLang()
    {
        GetComponent<TMP_Text>().text = GameObject.FindGameObjectWithTag("ScriptObj").GetComponent<TranslateLanguage>().UpdateCurText(gameObject);
    }
}
