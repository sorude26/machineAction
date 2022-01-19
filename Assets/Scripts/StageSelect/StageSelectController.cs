using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    [SerializeField]
    RectTransform[] _selectTargets = default;

    int _stageMaxNumber = default;
    int _stageNumber = 0;
    int _targetNumber = 0;
    float _changetime = 1f;
    bool _inputStop = false;
    bool _buttonOn = false;
    void MoveCursor(float v, float h)
    {
        if (_inputStop)
        {
            return;
        }
        if (h > 0.8f || h < -0.8f)
        {
            if (h > 0)
            {
                UIControl(1, 0);
            }
            else
            {
                UIControl(-1, 0);
            }
        }
        else if (v > 0.8f || v < -0.8f)
        {
            if (v > 0)
            {
                UIControl(0, 1);
            }
            else
            {
                UIControl(0, -1);
            }
        }
    }
    void UIControl(int target, int value)
    {
        _changetime = 0.2f;
        if (target < 0)
        {
            _stageNumber++;
            if (_stageNumber >= _stageMaxNumber)
            {
                _stageNumber = 0;
            }
            value = 0;
            InputStop();
        }
        else if (target > 0)
        {
            _stageNumber--;
            if (_stageNumber < 0)
            {
                _stageNumber = _stageMaxNumber;
            }
            value = 0;
            InputStop();
        }
        TargetControl(_stageNumber, value);
    }
    void TargetControl(int target, int value)
    {
        if (value == 0)
        {
            ChangeStage(target);
        }
        else
        {
            ChangeSelectTarget(value);
        }
    }
    void ChangeStage(int target)
    {

    }
    void ChangeSelectTarget(int value)
    {

    }
    void InputStop()
    {
        if (_inputStop)
        {
            return;
        }
        _inputStop = true;
        StartCoroutine(InputWait());
    }
    IEnumerator InputWait()
    {
        while (_changetime > 0)
        {
            _changetime -= Time.deltaTime;
            yield return null;
        }
        _inputStop = false;
    }
}
