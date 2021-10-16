using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shot : MonoBehaviour
{
    [SerializeField]
    GameObject m_effect = default;
    [SerializeField]
    Rigidbody m_rb = default;
    public Rigidbody ShotRb { get => m_rb; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            Instantiate(m_effect).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
