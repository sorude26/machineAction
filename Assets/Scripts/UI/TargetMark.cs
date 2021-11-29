using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetMark : MonoBehaviour
{
    [SerializeField]
    GameObject _lockOnMark;
    [SerializeField]
    RectTransform _rect;
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
    }
    public void SetTarget(DamageControl target)
    {
        Target = target;
        if (Target != null)
        {
            _rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Target.Center.position);
        }
        gameObject.SetActive(true);
    }
}
