using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslateLanguage : MonoBehaviour
{
    public GameObject[] _Txts;
    public TMP_Text displayText;  // ������ �� UI ������� Text
    public Saves _saves;

    private Dictionary<string, string> translations;

    void Start()
    {

        _saves = GetComponent<Saves>();
        // ������������� ������� � ����������
        translations = new Dictionary<string, string>
        {
            // ������ �������� �� ����������
            {"Menu_label", "Menu"},
            {"Settings_Txt", "Settings"},
            {"SensLabel_Txt", "Sensitivity"},
            {"SoundLabel_Txt", "Sound"},
            {"FovLabel_Txt", "Fov"},
            {"Resume_But_Txt", "Play"},
            {"Powers_Txt", "Powers"},
            {"Fly_Txt", "Fly"},
            {"DoubleJump_Txt", "Jump 2x"},
            {"Coins_Txt", "Coins:"},
            {"Height_Txt", "Height:"},
            {"Lang_Txt", "Language"},
            {"Die_Txt", "Failed"},
            {"Die_But_Txt", "Again"},
            {"Win_Txt", "Win"},
            {"Win_But_Txt", "Next level"},

            // ������ �������� �� �������
            {"Menu_label_ru", "����"},
            {"Settings_Txt_ru", "���������"},
            {"SensLabel_Txt_ru", "����������������"},
            {"SoundLabel_Txt_ru", "�����"},
            {"FovLabel_Txt_ru", "���� ������"},
            {"Resume_But_Txt_ru", "������"},
            {"Powers_Txt_ru", "��������"},
            {"Fly_Txt_ru", "����"},
            {"DoubleJump_Txt_ru", "������ 2�"},
            {"Coins_Txt_ru", "������:"},
            {"Height_Txt_ru", "������:"},
            {"Lang_Txt_ru", "����"},
            {"Die_Txt_ru", "����"},
            {"Die_But_Txt_ru", "������"},
            {"Win_Txt_ru", "������"},
            {"Win_But_Txt_ru", "��������� �������"}
        };
        // ���������� ��������� ����
        UpdateAllText();  // ���������� ����� "hello" ��� ������
    }
    public void SetLanguage()
    {
        if (_saves._lang == "ru")
            _saves._lang = "en";
        else
            _saves._lang = "ru";
        UpdateAllText();  // �������� ����� ��� ����� �����
        _saves.Save();

    }

    void UpdateAllText()
    {
        for (int i = 0; i < _Txts.Length; i++)
        {
            if (translations.ContainsKey(_Txts[i].name))
            {
                if (_saves._lang == "ru")
                {
                    _Txts[i].GetComponent<TMP_Text>().text = translations[_Txts[i].name + "_ru"];
                }
                else
                {
                    _Txts[i].GetComponent<TMP_Text>().text = translations[_Txts[i].name];
                }
            }
            else
            {
                displayText.GetComponent<TMP_Text>().text = "Translation not found";
            }
        }
        GameObject[] gmLang = GameObject.FindGameObjectsWithTag("gmLang");
        foreach (var item in gmLang)
        {
            item.GetComponent<TranslateObj>().UpdateLang();
        }
    }

    public string UpdateCurText(GameObject gm)
    {
        if (translations.ContainsKey(gm.name))
        {
            if (_saves._lang == "ru")
            {
                return translations[gm.name + "_ru"];
            }
            else
            {
                return translations[gm.name];
            }
        }
        else
            return "Not found key";
    }
}
