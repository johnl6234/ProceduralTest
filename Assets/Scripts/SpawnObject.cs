using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject spawnObject;
    public List<GameObject> objectList = new List<GameObject>();
    private List<GameObject> _orangeObjectsList = new List<GameObject>();
    [SerializeField] private float _distanceApart;
    [SerializeField] private int _amountToSpawn;
    [SerializeField] private Material _orangeMaterial;
    [SerializeField] private int _percentageOfOrange;
    private Vector3 _positionVector;
    private float _width;
    private float _length;
    private float _height = 1;

    private int _numberOfOrange;

    public void Start()
    {
        _width = GetComponent<Renderer>().bounds.size.x / 2;
        _length = GetComponent<Renderer>().bounds.size.z / 2;
        while (objectList.Count < _amountToSpawn)
        {
            _positionVector =  GetNewPosition();
            if (CheckPosition(_positionVector))
            {
                var prefab = Instantiate(spawnObject, _positionVector, Quaternion.identity);
                objectList.Add(prefab);
            }
        }

        AdjustMaterial();
    }

    private void AdjustMaterial()
    {
        var percentage = _percentageOfOrange / 100f;

        for (var i = 0; i < Mathf.FloorToInt(objectList.Count * percentage); i++)
        {
            Debug.Log(i);
            var j = Random.Range(0, objectList.Count);
            if (!objectList[j].GetComponent<Prefab>().isOrange)
            {
                objectList[j].GetComponent<MeshRenderer>().material = _orangeMaterial;
                objectList[j].GetComponent<Prefab>().isOrange = true;
            }
            else
            {
                i--;
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
