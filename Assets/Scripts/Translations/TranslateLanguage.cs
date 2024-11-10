using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslateLanguage : MonoBehaviour
{
    public GameObject[] _Txts;
    public TMP_Text displayText;  // Ссылка на UI элемент Text
    public Saves _saves;

    private Dictionary<string, string> translations;

    void Start()
    {

        _saves = GetComponent<Saves>();
        // Инициализация словаря с переводами
        translations = new Dictionary<string, string>
        {
            // Пример перевода на английском
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

            // Пример перевода на русском
            {"Menu_label_ru", "Меню"},
            {"Settings_Txt_ru", "Настройки"},
            {"SensLabel_Txt_ru", "Чувствительность"},
            {"SoundLabel_Txt_ru", "Звуки"},
            {"FovLabel_Txt_ru", "Угол обзора"},
            {"Resume_But_Txt_ru", "Играть"},
            {"Powers_Txt_ru", "Усиления"},
            {"Fly_Txt_ru", "Полёт"},
            {"DoubleJump_Txt_ru", "Прыжок 2х"},
            {"Coins_Txt_ru", "Монеты:"},
            {"Height_Txt_ru", "Высота:"},
            {"Lang_Txt_ru", "Язык"},
            {"Die_Txt_ru", "Упал"},
            {"Die_But_Txt_ru", "Заново"},
            {"Win_Txt_ru", "Победа"},
            {"Win_But_Txt_ru", "Следующий уровень"}
        };
        // Установите начальный язык
        UpdateAllText();  // Установите текст "hello" как пример
    }
    public void SetLanguage()
    {
        if (_saves._lang == "ru")
            _saves._lang = "en";
        else
            _saves._lang = "ru";
        UpdateAllText();  // Обновите текст при смене языка
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
