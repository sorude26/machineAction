using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolBase<T,key> : MonoBehaviour where T : MonoBehaviour where key : Enum
{
    private static ObjectPoolBase<T,key> instance = default;

    [SerializeField]
    protected T[] _objectPrefab = default;
    [SerializeField]
    protected int[] _createCount = default;

    Dictionary<key, T[]> _objectDic = default;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        if (_objectPrefab.Length > _createCount.Length)
        {
            return;
        }
        _objectDic = new Dictionary<key, T[]>();
        for (int i = 0; i < _objectPrefab.Length; i++)
        {
            var objectData = new T[_createCount[i]];
            for (int b = 0; b < _createCount[i]; b++)
            {
                var instanceObject = Instantiate(_objectPrefab[i], transform);
                instanceObject.gameObject.SetActive(false);
                objectData[b] = instanceObject;
            }
            _objectDic.Add((key)Enum.ToObject(typeof(key), i), objectData);
        }
    }

    public static T Get(key type, Vector3 pos)
    {
        foreach (var objects in instance._objectDic[type])
        {
            if (objects.gameObject.activeInHierarchy)
            {
                continue;
            }
            objects.transform.position = pos;
            objects.gameObject.SetActive(true);
            return objects;
        }
        return null;
    }
    public static void FullReset()
    {
        for (int i = 0; i < instance._objectPrefab.Length; i++)
        {            
            foreach (var objects in instance._objectDic[(key)Enum.ToObject(typeof(key),i)])
            {
                objects.gameObject.SetActive(false);
            }
        }
    }
}
