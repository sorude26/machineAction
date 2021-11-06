using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolBase<T,key> : MonoBehaviour where T : MonoBehaviour where key : Enum
{
    private static ObjectPoolBase<T,key> instance = default;

    [SerializeField]
    protected T[] m_objectPrefab = default;
    [SerializeField]
    protected int[] m_createCount = default;

    Dictionary<key, T[]> m_objectDic = default;
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
        if (m_objectPrefab.Length > m_createCount.Length)
        {
            return;
        }
        m_objectDic = new Dictionary<key, T[]>();
        for (int i = 0; i < m_objectPrefab.Length; i++)
        {
            var bulletData = new T[m_createCount[i]];
            for (int b = 0; b < m_createCount[i]; b++)
            {
                var bullet = Instantiate(m_objectPrefab[i], transform);
                bullet.gameObject.SetActive(false);
                bulletData[b] = bullet;
            }
            m_objectDic.Add((key)Enum.ToObject(typeof(key), i), bulletData);
        }
    }

    public static T Get(key type, Vector3 pos)
    {
        foreach (var bullet in instance.m_objectDic[type])
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
