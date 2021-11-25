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
        }
        if (_objectPrefab.Length > _createCount.Length)
        {
            return;
        }
        _objectDic = new Dictionary<key, T[]>();
        for (int i = 0; i < _objectPrefab.Length; i++)
        {
            var bulletData = new T[_createCount[i]];
            for (int b = 0; b < _createCount[i]; b++)
            {
                var bullet = Instantiate(_objectPrefab[i], transform);
                bullet.gameObject.SetActive(false);
                bulletData[b] = bullet;
            }
            _objectDic.Add((key)Enum.ToObject(typeof(key), i), bulletData);
        }
    }

    public static T Get(key type, Vector3 pos)
    {
        foreach (var bullet in instance._objectDic[type])
        {
            if (bullet.gameObject.activeInHierarchy)
            {
                continue;
            }
            bullet.transform.position = pos;
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        return null;
    }
}
