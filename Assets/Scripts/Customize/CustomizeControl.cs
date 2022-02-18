using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeControl : MonoBehaviour
{
    [SerializeField]
    UnitBuildData _currentBuildData = default;
    [SerializeField]
    MachineBuildControl _buildControl = default;
    [SerializeField]
    Transform[] _modelTransform = new Transform[4];
    [SerializeField]
    Slider[] _sliders = default;
    [SerializeField]
    Slider[] _colorSliders = new Slider[3];
    [SerializeField]
    Button _button = default;
    [SerializeField]
    GameObject _mark = default;
    [SerializeField]
    Transform _cameraTarget = default;
    [SerializeField]
    float _followSpeed = 5f;
    [SerializeField]
    float _lockSpeed = 20f;
    PartsManager _partsManager = default;
    UnitBuildData _buildData = default;
    Quaternion _cameraRot = default;
    Color _color = default;
    int _targetMaxNumber = default;
    int _targetNumber = 0;
    float _changetime = 1f;
    bool _setColor = false;
    bool _inputStop = false;
    bool _buttonOn = false;
    private void Awake()
    {
        _partsManager = new PartsManager();
    }
    private void Start()
    {
        _buildData = GameManager.Instance.CurrentBuildData;
        _currentBuildData = _buildData;
        _color = GameManager.Instance.PlayerColor;
        _sliders[0].value = _currentBuildData.HeadID;
        _sliders[1].value = _currentBuildData.BodyID;
        _sliders[2].value = _currentBuildData.LArmID;
        _sliders[3].value = _currentBuildData.RArmID;
        _sliders[4].value = _currentBuildData.LegID;
        _sliders[5].value = _currentBuildData.BoosterID;
        _sliders[6].value = _currentBuildData.WeaponLArmID;
        _sliders[7].value = _currentBuildData.WeaponRArmID;
        _sliders[8].value = _currentBuildData.ShoulderWeaponID;
        _sliders[9].value = _currentBuildData.BodyWeaponID;
        _colorSliders[0].value = _color.r;
        _colorSliders[1].value = _color.g;
        _colorSliders[2].value = _color.b;
        _targetMaxNumber = _sliders.Length + _colorSliders.Length + 1;
        _setColor = true;
        GameScene.InputManager.Instance.OnInputAxisRaw += MoveCursor;
        GameScene.InputManager.Instance.OnFirstInputJump += ChangeOn;
        GameScene.InputManager.Instance.OnFirstInputShot2 += ChangeOn;
        GameScene.InputManager.Instance.OnInputCameraRawExit += ResetLock;
        GameScene.InputManager.Instance.OnInputCameraRaw += FreeLock;
        Build();
        TargetControl(_targetNumber, 0);
        FadeController.StartFadeIn();
    }
    private void Update()
    {
        _cameraRot = _cameraTarget.rotation;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, _cameraRot, _followSpeed * Time.deltaTime);
    }
    void DefaultLock()
    {
        _cameraRot = _cameraTarget.rotation;
    }
    void FreeLock(Vector2 dir)
    {
        _cameraRot = _cameraTarget.localRotation;
        if (Mathf.Abs(dir.x) > 0.1f)
        {
            _cameraRot *= Quaternion.Euler(0, dir.x * _lockSpeed, 0);
        }
        _cameraTarget.rotation = _cameraRot;
    }
    void ResetLock()
    {
        _cameraRot = _cameraTarget.localRotation;
    }
    public void Build()
    {
        var rot = transform.localRotation;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        foreach (var transform in _modelTransform)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        _buildControl.Purge(transform);
        _partsManager.ResetAllParts();
        _buildControl.SetData(_currentBuildData);
        _buildControl.StartSet(_partsManager);
        _modelTransform[0].localRotation = Quaternion.Euler(0, 0, -6);
        _modelTransform[1].localRotation = Quaternion.Euler(0, 0, 6);
        _modelTransform[2].localRotation = Quaternion.Euler(-70, 0, -1);
        _modelTransform[3].localRotation = Quaternion.Euler(-70, 0, 1);
        SetColor();
        transform.localRotation = rot;
    }
    void SetColor()
    {
        _partsManager.ChangeColor(_color);
    }
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
        _changetime = 0.1f;
        if (target < 0)
        {
            _targetNumber++;
            if (_targetNumber >= _targetMaxNumber)
            {
                _targetNumber = 0;
            }
            value = 0;
            InputStop();
        }
        else if (target > 0)
        {
            _targetNumber--; 
            if (_targetNumber < 0)
            {
                _targetNumber = _targetMaxNumber;
            }
            value = 0;
            InputStop();
        }
        TargetControl(_targetNumber, value);
    }
    void TargetControl(int target,int value)
    {
        if (target < _sliders.Length)
        {
            _buttonOn = false;
            _mark.transform.position = _sliders[target].transform.position;
            PartsChange(target, value);
        }
        else if (target < _targetMaxNumber - 1)
        {
            _buttonOn = false;
            _mark.transform.position = _colorSliders[target - _sliders.Length].transform.position;
            ColorChange(target - _sliders.Length, value);
        }
        else
        {
            _buttonOn = true;
            _mark.transform.position = _button.transform.position;
        }
    }
    void PartsChange(int target,int count)
    {
        if (_inputStop)
        {
            return;
        }
        _changetime = 0.1f;
        if (count > 0)
        {
            if (_sliders[target].maxValue > _sliders[target].value)
            {
                _sliders[target].value++;
                InputStop();
            }
        }
        else if (count < 0)
        {
            if (_sliders[target].value > 0)
            {
                _sliders[target].value--;
                InputStop();
            }
        }
    }
    void ColorChange(int target,int count)
    {
        if (_inputStop)
        {
            return;
        }
        _changetime = 0.02f;
        if (count > 0)
        {
            if (_colorSliders[target].maxValue > _colorSliders[target].value)
            {
                _colorSliders[target].value += 0.01f;
            }
            InputStop();
        }
        else if (count < 0)
        {
            if (_colorSliders[target].value > 0)
            {
                _colorSliders[target].value -= 0.01f;
            }
            InputStop();
        }
    }
    void ChangeOn()
    {
        if (_inputStop || !_buttonOn)
        {
            return;
        }
        GameScene.InputManager.Instance.InputActionsOut();
        _button.onClick?.Invoke();
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
    public void SetDataBattlleStart()
    {
        GameManager.Instance.SetData(_currentBuildData, _color);
        SceneChange.LoadGame();
    }
    public void ChangeHead(int number)
    {
        _currentBuildData.HeadID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeBody(int number)
    {
        _currentBuildData.BodyID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeLArm(int number)
    {
        _currentBuildData.LArmID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeRArm(int number)
    {
        _currentBuildData.RArmID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeLeg(int number)
    {
        _currentBuildData.LegID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeLWeapon(int number)
    {
        _currentBuildData.WeaponLArmID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeRWeapon(int number)
    {
        _currentBuildData.WeaponRArmID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeShoulder(int number)
    {
        _currentBuildData.ShoulderWeaponID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeBWeapon(int number)
    {
        _currentBuildData.BodyWeaponID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeBooster(int number)
    {
        _currentBuildData.BoosterID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeColor()
    {
        if (!_setColor)
        {
            return;
        }
        _color.r = _colorSliders[0].value;
        _color.g = _colorSliders[1].value;
        _color.b = _colorSliders[2].value;
        SetColor();
    }
}
