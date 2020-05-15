using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private float _distanceApart;
    [SerializeField] private int _amountToSpawn;
    [SerializeField] private Material _orangeMaterial;
    [SerializeField] private int _percentageOfOrange;
    [SerializeField] private GameObject _spawnObject;

    private readonly List<GameObject> _objectList = new List<GameObject>();
    private Vector3 _positionVector;
    private float _width;
    private float _length;
    private readonly float _height = 1;

    public void Start()
    {
        _width = GetComponent<Renderer>().bounds.size.x / 2;
        _length = GetComponent<Renderer>().bounds.size.z / 2;

        var maxPossible = (int)(_width * 2 / _distanceApart) * (_length * 2 / _distanceApart);
        maxPossible -= (maxPossible * .10f);

        if (_amountToSpawn > maxPossible)
            _amountToSpawn = (int)maxPossible;

        var count = 0;
        while (_objectList.Count < _amountToSpawn)
        {
            _positionVector = GetNewPosition();
            if (CheckPosition(_positionVector))
            {
                var prefab = Instantiate(_spawnObject, _positionVector, Quaternion.identity);
                _objectList.Add(prefab);
            }
            else
            {
                count++;
                if (count > _amountToSpawn)
                    break;
            }
        }

        AdjustMaterial();
    }

    private void AdjustMaterial()
    {
        var percentage = _percentageOfOrange / 100f;

        for (var i = 0; i < Mathf.FloorToInt(_objectList.Count * percentage); i++)
        {
            var j = Random.Range(0, _objectList.Count);
            if (!_objectList[j].GetComponent<Prefab>().isOrange)
            {
                _objectList[j].GetComponent<MeshRenderer>().material = _orangeMaterial;
                _objectList[j].GetComponent<Prefab>().isOrange = true;
            }
            else
            {
                i--;
            }
        }
    }

    private bool CheckPosition(Vector3 positionVector)
    {
        foreach (var newGameObject in _objectList)
        {
            if (Vector3.Distance(newGameObject.transform.position, positionVector) < _distanceApart)
                return false;
        }
        return true;
    }

    private Vector3 GetNewPosition()
    {
        var x = Random.Range(-_width, _width);
        var z = Random.Range(-_length, _length);
        var y = _height;
        return new Vector3(x, y, z);
    }
}
