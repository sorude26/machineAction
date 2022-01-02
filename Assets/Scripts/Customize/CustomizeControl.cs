using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeControl : MonoBehaviour
{
    [SerializeField]
    UnitBuildData _unitBuildData = default;
    [SerializeField]
    MachineBuildControl _buildControl = default;
    [SerializeField]
    Transform[] _modelTransform = new Transform[4];
    [SerializeField]
    Slider[] _sliders = default;
    [SerializeField]
    Slider[] _colorSliders = new Slider[3];
    Color _color = default;
    bool _setColor = false;
    PartsManager _partsManager = default;
    private void Awake()
    {
        _partsManager = new PartsManager();
    }
    private void Start()
    {
        _unitBuildData = GameManager.Instance.CurrentBuildData;
        _color = GameManager.Instance.PlayerColor;
        _sliders[0].value = _unitBuildData.HeadID;
        _sliders[1].value = _unitBuildData.BodyID;
        _sliders[2].value = _unitBuildData.LArmID;
        _sliders[3].value = _unitBuildData.RArmID;
        _sliders[4].value = _unitBuildData.LegID;
        _sliders[5].value = _unitBuildData.WeaponLArmID;
        _sliders[6].value = _unitBuildData.WeaponRArmID;
        _sliders[7].value = _unitBuildData.ShoulderWeaponID;
        _sliders[8].value = _unitBuildData.BodyWeaponID;
        _colorSliders[0].value = _color.r;
        _colorSliders[1].value = _color.g;
        _colorSliders[2].value = _color.b;
        _setColor = true;
        Build();
        FadeController.StartFadeIn();
    }
    public void Build()
    {
        foreach (var transform in _modelTransform)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        _partsManager.ResetAllParts();
        _buildControl.SetData(_unitBuildData);
        _buildControl.StartSet(_partsManager);
        _modelTransform[0].localRotation = Quaternion.Euler(0, 0, -6);
        _modelTransform[1].localRotation = Quaternion.Euler(0, 0, 6);
        _modelTransform[2].localRotation = Quaternion.Euler(-70, 0, -1);
        _modelTransform[3].localRotation = Quaternion.Euler(-70, 0, 1);
        SetColor();
    }
    void SetColor()
    {
        _partsManager.ChangeColor(_color);
    }
    public void SetDataBattlleStart()
    {
        GameManager.Instance.SetData(_unitBuildData, _color);
        SceneChange.RoadGame();
    }
    public void ChangeHead(int number)
    {
        _unitBuildData.HeadID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeBody(int number)
    {
        _unitBuildData.BodyID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeLArm(int number)
    {
        _unitBuildData.LArmID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeRArm(int number)
    {
        _unitBuildData.RArmID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeLeg(int number)
    {
        _unitBuildData.LegID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeLWeapon(int number)
    {
        _unitBuildData.WeaponLArmID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeRWeapon(int number)
    {
        _unitBuildData.WeaponRArmID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeShoulder(int number)
    {
        _unitBuildData.ShoulderWeaponID = (int)_sliders[number].value;
        Build();
    }
    public void ChangeBWeapon(int number)
    {
        _unitBuildData.BodyWeaponID = (int)_sliders[number].value;
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
