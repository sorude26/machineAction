using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNamePanel : MonoBehaviour
{
    [SerializeField]
    GameObject _stagePanel = default;
    [SerializeField]
    Text[] _stageName = default;
    public void SetPanel(string name, Quaternion angle, int number)
    {
        foreach (var item in _stageName)
        {
            item.text = name;
        }
        _stagePanel.transform.position = Vector3.back * number;
        transform.rotation = angle;
    }
}
