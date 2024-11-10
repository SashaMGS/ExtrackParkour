using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    // Количество платформ для генерации
    public int _countPlatforms = 5;
    // Величина случайного поворота платформы
    public int _randomRotation = 90;
    // Минимальное расстояние между платформами
    public float _distanceForPlatforms = 2f;
    // Вектор для случайного смещения платформ при генерации
    public Vector3 _plusVector = new Vector3(5f, 0.5f, 5f);
    // Массив префабов платформ
    public GameObject[] _platformsPrefabs;
    // Префаб объекта финиша
    public GameObject _finishObj;
    // Родительский объект для всех платформ
    public GameObject _mainPack;
    // Начальная позиция для генерации платформ
    public Vector3 _startPosPlatform = new Vector3(0f, 0f, 0f);

    // Текущая позиция для генерации платформ
    private Vector3 _curPosPl;
    // Текущий случайный угол поворота платформы
    private int _curRotation;
    // Текущее расстояние между двумя платформами
    private float _distance;

    public GameObject _pickUpPrefab;
    public int _countMaxSupers = 2;
    private int _countSupers;

    private void Awake()
    {
        try
        {
            _curPosPl = _startPosPlatform;
            // Если флаг случайного изменения установлен, изменяем границы случайного выбора блока либо куб либо шар

            // Загрузка сохраненных данных
            GetComponent<Saves>().LoadSave();

            // Установка количества платформ в зависимости от уровня сложности
            if (GetComponent<Saves>()._difficult <= 1)
                _countPlatforms = 20;
            else if (GetComponent<Saves>()._difficult <= 10)
                _countPlatforms = 20 * GetComponent<Saves>()._difficult;
            else
                _countPlatforms = 200;

            // Массив для хранения сгенерированных объектов платформ
            GameObject[] _platformsObjs = new GameObject[_countPlatforms];

            // Генерация платформ
            for (int i = 0; i < _countPlatforms; i++)
            {
                // Создаем новую платформу из префаба
                _platformsObjs[i] = Instantiate(_platformsPrefabs[Random.Range(0, _platformsPrefabs.Length)]);

                // Если это не первая платформа, проверяем расстояние до предыдущей
                if (i > 0)
                {
                    // Рассчитываем новую позицию для платформы с учетом случайного смещения
                    Vector3 _newPos = new Vector3(_curPosPl.x + Random.Range(-_plusVector.x, _plusVector.x),
                                              _curPosPl.y + _plusVector.y,
                                              _curPosPl.z + Random.Range(-_plusVector.z, _plusVector.z));

                    _distance = Vector3.Distance(_newPos, _platformsObjs[i - 1].transform.position);

                    // Пока расстояние меньше минимально допустимого, генерируем новую позицию
                    while (_distance < _distanceForPlatforms)
                    {
                        _newPos = new Vector3(_curPosPl.x + Random.Range(-_plusVector.x, _plusVector.x),
                                              _curPosPl.y + _plusVector.y,
                                              _curPosPl.z + Random.Range(-_plusVector.z, _plusVector.z));

                        _distance = Vector3.Distance(_platformsObjs[i].transform.position, _platformsObjs[i - 1].transform.position);
                    }

                    // Обновляем текущую позицию платформы
                    _curPosPl = _newPos;
                }

                // Устанавливаем позицию платформы
                _platformsObjs[i].transform.position = _curPosPl;

                // Применяем случайный поворот к платформе
                _curRotation = Random.Range(-_randomRotation, _randomRotation);
                _platformsObjs[i].transform.Rotate(0f, _curRotation, 0f);

                // Устанавливаем случайный цвет платформы
                _platformsObjs[i].GetComponent<MeshRenderer>().material.color = Random.ColorHSV();

                // Если это последняя платформа, устанавливаем красный цвет и создаем объект финиша
                if (i == _countPlatforms - 1)
                {
                    _platformsObjs[i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
                    Instantiate(_finishObj, _platformsObjs[i].transform.position, transform.rotation);
                    GameObject _pickUpObj = Instantiate(_pickUpPrefab, _platformsObjs[i].transform.position, transform.rotation);
                    _pickUpObj.transform.GetChild(0).GetComponent<PickUpObjScript>().SpawnItem(4);
                    // Устанавливаем родительский объект для предмета
                    _pickUpObj.transform.SetParent(_mainPack.transform);
                }
                else if (i < _countPlatforms - 1)
                {
                    int _change = Random.Range(0, 100);
                    if (_change < 30 || _change == 70 || _change == 80)
                    {
                        GameObject _pickUpObj = Instantiate(_pickUpPrefab, _platformsObjs[i].transform.position, transform.rotation);
                        if (_change < 30)
                            _pickUpObj.transform.GetChild(0).GetComponent<PickUpObjScript>().SpawnItem(1);
                        else if (_change == 70 && i > 15 && _countSupers <= _countMaxSupers)
                        {
                            _pickUpObj.transform.GetChild(0).GetComponent<PickUpObjScript>().SpawnItem(2);
                            _countSupers++;
                        }
                        else if (_change == 80 && i > 15 && _countSupers <= _countMaxSupers)
                        {
                            _pickUpObj.transform.GetChild(0).GetComponent<PickUpObjScript>().SpawnItem(3);
                            _countSupers++;
                        }
                        // Устанавливаем родительский объект для предмета
                        _pickUpObj.transform.SetParent(_mainPack.transform);
                    }
                }

                // Устанавливаем родительский объект для платформы
                _platformsObjs[i].transform.SetParent(_mainPack.transform);
            }

            // Деактивируем родительский объект, чтобы отключить все платформы
            _mainPack.SetActive(false);
        }
        catch (System.Exception)
        {
            Debug.Log("Что-то пошло не так");
            throw;
        }

    }
}

/*
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public int _countPlatforms = 5;
    public int _randomRotation = 90;
    public float _distanceForPlatforms = 2f;
    public Vector3 _plusVector = new Vector3(5f, 0.5f, 5f);
    public GameObject[] _platformsPrefabs;
    public GameObject _finishObj;
    public GameObject _mainPack;
    public bool _randomChange = true;
    public int _b1_Change = 7;
    public int _b2_Change = 10;

    private Vector3 _curPosPl;
    private int _curRotation;
    private float _distance;

    private void Awake()
    {
        if (_randomChange)
        {
            _b1_Change = Random.Range(0, 10);
            _b2_Change = Random.Range(_b1_Change, 11);
        }

        GetComponent<Saves>().LoadSave();
        if (GetComponent<Saves>()._difficult <= 1)
            _countPlatforms = 20;
        else if(GetComponent<Saves>()._difficult <= 10)
            _countPlatforms = 20 * GetComponent<Saves>()._difficult;
        else
            _countPlatforms = 200;
        GameObject[] _platformsObjs = new GameObject[_countPlatforms];
        for (int i = 0; i < _countPlatforms; i++)
        {
            _platformsObjs[i] = Instantiate(_platformsPrefabs[ChangeBlock()]);
            Vector3 _newPos = new Vector3(_curPosPl.x + Random.Range(-_plusVector.x, _plusVector.x), _curPosPl.y + _plusVector.y, _curPosPl.z + Random.Range(-_plusVector.z, _plusVector.z));

            if (i > 0)
            {
                _distance = Vector3.Distance(_newPos, _platformsObjs[i - 1].transform.position);

                while (_distance < _distanceForPlatforms)
                {
                    _newPos = new Vector3(_curPosPl.x + Random.Range(-_plusVector.x, _plusVector.x), _curPosPl.y + _plusVector.y, _curPosPl.z + Random.Range(-_plusVector.z, _plusVector.z));

                    _distance = Vector3.Distance(_platformsObjs[i].transform.position, _platformsObjs[i - 1].transform.position);

                }
            }

            _curPosPl = _newPos;
            _platformsObjs[i].transform.position = _curPosPl;

            _curRotation = Random.Range(-_randomRotation, _randomRotation);
            _platformsObjs[i].transform.Rotate(0f, _curRotation, 0f);

            _platformsObjs[i].GetComponent<MeshRenderer>().material.color = Random.ColorHSV();

            if (i == _countPlatforms - 1)
            {
                _platformsObjs[i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
                Instantiate(_finishObj, _platformsObjs[i].transform.position, _platformsObjs[i].transform.rotation);
            }

            _platformsObjs[i].transform.SetParent(_mainPack.transform);
        }

        _mainPack.SetActive(false);
    }

    private int ChangeBlock()
    {
        int i = Random.Range(0, _b2_Change);
        if (i >= _b1_Change)
            return 1;
        else
            return 0;
    }
}
*/