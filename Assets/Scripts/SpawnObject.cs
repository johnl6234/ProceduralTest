using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject spawnObject;
    public List<GameObject> objectList = new List<GameObject>();
    [SerializeField] private float _distanceApart;
    [SerializeField] private int _amountToSpawn;
    private Vector3 _positionVector;
    private float _width;
    private float _length;
    private float _height = 1;

    public void Start()
    {
        _width = GetComponent<Renderer>().bounds.size.x / 2;
        _length = GetComponent<Renderer>().bounds.size.z / 2;

        while (objectList.Count < _amountToSpawn)
        {
            _positionVector =  GetNewPosition();
            if (CheckPosition(_positionVector))
            {
                GameObject prefab = Instantiate(spawnObject, _positionVector, Quaternion.identity);
                objectList.Add(prefab);
            }
        }
    }

    private bool CheckPosition(Vector3 positionVector)
    {
        foreach (var vectorPosition in objectList)
        {
            if (Vector3.Distance(vectorPosition.transform.position, positionVector) < _distanceApart) return false;
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
