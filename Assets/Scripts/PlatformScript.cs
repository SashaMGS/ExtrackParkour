using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    // ���������� �������� ��� ���������
    public int _countPlatforms = 5;
    // �������� ���������� �������� ���������
    public int _randomRotation = 90;
    // ����������� ���������� ����� �����������
    public float _distanceForPlatforms = 2f;
    // ������ ��� ���������� �������� �������� ��� ���������
    public Vector3 _plusVector = new Vector3(5f, 0.5f, 5f);
    // ������ �������� ��������
    public GameObject[] _platformsPrefabs;
    // ������ ������� ������
    public GameObject _finishObj;
    // ������������ ������ ��� ���� ��������
    public GameObject _mainPack;
    // ��������� ������� ��� ��������� ��������
    public Vector3 _startPosPlatform = new Vector3(0f, 0f, 0f);

    // ������� ������� ��� ��������� ��������
    private Vector3 _curPosPl;
    // ������� ��������� ���� �������� ���������
    private int _curRotation;
    // ������� ���������� ����� ����� �����������
    private float _distance;

    public GameObject _pickUpPrefab;
    public int _countMaxSupers = 2;
    private int _countSupers;

    private void Awake()
    {
        try
        {
            _curPosPl = _startPosPlatform;
            // ���� ���� ���������� ��������� ����������, �������� ������� ���������� ������ ����� ���� ��� ���� ���

            // �������� ����������� ������
            GetComponent<Saves>().LoadSave();

            // ��������� ���������� �������� � ����������� �� ������ ���������
            if (GetComponent<Saves>()._difficult <= 1)
                _countPlatforms = 20;
            else if (GetComponent<Saves>()._difficult <= 10)
                _countPlatforms = 20 * GetComponent<Saves>()._difficult;
            else
                _countPlatforms = 200;

            // ������ ��� �������� ��������������� �������� ��������
            GameObject[] _platformsObjs = new GameObject[_countPlatforms];

            // ��������� ��������
            for (int i = 0; i < _countPlatforms; i++)
            {
                // ������� ����� ��������� �� �������
                _platformsObjs[i] = Instantiate(_platformsPrefabs[Random.Range(0, _platformsPrefabs.Length)]);

                // ���� ��� �� ������ ���������, ��������� ���������� �� ����������
                if (i > 0)
                {
                    // ������������ ����� ������� ��� ��������� � ������ ���������� ��������
                    Vector3 _newPos = new Vector3(_curPosPl.x + Random.Range(-_plusVector.x, _plusVector.x),
                                              _curPosPl.y + _plusVector.y,
                                              _curPosPl.z + Random.Range(-_plusVector.z, _plusVector.z));

                    _distance = Vector3.Distance(_newPos, _platformsObjs[i - 1].transform.position);

                    // ���� ���������� ������ ���������� �����������, ���������� ����� �������
                    while (_distance < _distanceForPlatforms)
                    {
                        _newPos = new Vector3(_curPosPl.x + Random.Range(-_plusVector.x, _plusVector.x),
                                              _curPosPl.y + _plusVector.y,
                                              _curPosPl.z + Random.Range(-_plusVector.z, _plusVector.z));

                        _distance = Vector3.Distance(_platformsObjs[i].transform.position, _platformsObjs[i - 1].transform.position);
                    }

                    // ��������� ������� ������� ���������
                    _curPosPl = _newPos;
                }

                // ������������� ������� ���������
                _platformsObjs[i].transform.position = _curPosPl;

                // ��������� ��������� ������� � ���������
                _curRotation = Random.Range(-_randomRotation, _randomRotation);
                _platformsObjs[i].transform.Rotate(0f, _curRotation, 0f);

                // ������������� ��������� ���� ���������
                _platformsObjs[i].GetComponent<MeshRenderer>().material.color = Random.ColorHSV();

                // ���� ��� ��������� ���������, ������������� ������� ���� � ������� ������ ������
                if (i == _countPlatforms - 1)
                {
                    _platformsObjs[i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
                    Instantiate(_finishObj, _platformsObjs[i].transform.position, transform.rotation);
                    GameObject _pickUpObj = Instantiate(_pickUpPrefab, _platformsObjs[i].transform.position, transform.rotation);
                    _pickUpObj.transform.GetChild(0).GetComponent<PickUpObjScript>().SpawnItem(4);
                    // ������������� ������������ ������ ��� ��������
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
                        // ������������� ������������ ������ ��� ��������
                        _pickUpObj.transform.SetParent(_mainPack.transform);
                    }
                }

                // ������������� ������������ ������ ��� ���������
                _platformsObjs[i].transform.SetParent(_mainPack.transform);
            }

            // ������������ ������������ ������, ����� ��������� ��� ���������
            _mainPack.SetActive(false);
        }
        catch (System.Exception)
        {
            Debug.Log("���-�� ����� �� ���");
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