using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetMark : MonoBehaviour
{
    [SerializeField]
    RectTransform _center;
    [SerializeField]
    GameObject _lockOnMark;
    [SerializeField]
    RectTransform _rect;
    [SerializeField]
    float _range = 0.4f;
    public DamageControl Target { get; private set; }
    private void Update()
    {
        if (Target == null || Target.CurrentHP <= 0)
        {
            Target = null;
            gameObject.SetActive(false);
            return;
        }
        _rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Target.Center.position);
        if((_center.position - _rect.position).magnitude > _range)
        {
            Target = null;
            gameObject.SetActive(false);
        }
    }
    public void SetTarget(DamageControl target)
    {
        Target = target;
        if (Target != null)
        {
            _rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Target.Center.position);
            gameObject.SetActive(true);
        }
    }
  
}
