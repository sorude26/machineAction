using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBuildControl : MonoBehaviour
{
    [SerializeField]
    UnitBuildData m_test = default;
    [SerializeField]
    Transform _camera = default;
    [SerializeField]
    Transform[] _bodyBase = new Transform[2];
    [SerializeField]
    Transform[] _rightArm = new Transform[4];
    [SerializeField]
    Transform[] _leftArm = new Transform[4];
    [SerializeField]
    Transform[] _legBase = new Transform[5];
    [SerializeField]
    Transform[] _rightLeg = new Transform[3];
    [SerializeField]
    Transform[] _leftLeg = new Transform[3];

    PartsHead _head = default;
    PartsBody _body = default;
    PartsArm _rArm = default;
    PartsArm _lArm = default;
    PartsLeg _leg = default;
    public WeaponMaster RAWeapon { get; private set; }
    public WeaponMaster LAWeapon { get; private set; }
    public ShoulderWeapon ShoulderWeapon { get; private set; }
    WeaponMaster _bodyWeapon = default;
    public void StartSet()
    {
        Build(m_test);
    }
    public void Build(UnitBuildData data)
    {
        _leg = Instantiate(GameManager.Instanse.PartsList.GetLeg(data.LegID));
        _leg.transform.position = _legBase[0].position;
        _legBase[3].position = _leg.LegTop.position;
        _leg.transform.SetParent(_legBase[3]);
        _rightLeg[0].position = _leg.RLeg1.position;
        _rightLeg[1].position = _leg.RLeg2.position;
        _rightLeg[2].position = _leg.RLeg3.position;
        _leftLeg[0].position = _leg.LLeg1.position;
        _leftLeg[1].position = _leg.LLeg2.position;
        _leftLeg[2].position = _leg.LLeg3.position;
        _leg.RLeg3.SetParent(_rightLeg[2]);
        _leg.RLeg2.SetParent(_rightLeg[1]);
        _leg.RLeg1.SetParent(_rightLeg[0]);
        _leg.LLeg3.SetParent(_leftLeg[2]);
        _leg.LLeg2.SetParent(_leftLeg[1]);
        _leg.LLeg1.SetParent(_leftLeg[0]);
        _body = Instantiate(GameManager.Instanse.PartsList.GetBody(data.BodyID));
        _body.transform.position = _bodyBase[0].position;
        _bodyBase[1].position = _body.HeadPos.position;
        _camera.transform.position = _body.HeadPos.position;
        _body.transform.SetParent(_bodyBase[0]);
        _rightArm[0].position = _body.RArmPos.position;
        _leftArm[0].position = _body.LArmPos.position;
        _rArm = Instantiate(GameManager.Instanse.PartsList.GetRArm(data.RArmID));
        _rArm.transform.position = _rightArm[0].position;
        _rArm.transform.SetParent(_rightArm[0]);
        _rightArm[1].position = _rArm.ArmTop.position;
        _rightArm[2].position = _rArm.ArmBottom.position;
        _rightArm[3].position = _rArm.Grip.position;
        _rArm.Grip.SetParent(_rightArm[3]);
        _rArm.ArmBottom.SetParent(_rightArm[2]);
        _rArm.ArmTop.SetParent(_rightArm[1]);
        _lArm = Instantiate(GameManager.Instanse.PartsList.GetLArm(data.LArmID));
        _lArm.transform.position = _leftArm[0].position;
        _lArm.transform.SetParent(_leftArm[0]);
        _leftArm[1].position = _lArm.ArmTop.position;
        _leftArm[2].position = _lArm.ArmBottom.position;
        _leftArm[3].position = _lArm.Grip.position;
        _lArm.Grip.SetParent(_leftArm[3]);
        _lArm.ArmBottom.SetParent(_leftArm[2]);
        _lArm.ArmTop.SetParent(_leftArm[1]);
        _head = Instantiate(GameManager.Instanse.PartsList.GetHead(data.HeadID));
        _head.transform.position = _bodyBase[1].position;
        _head.transform.SetParent(_bodyBase[1]);
        RAWeapon = Instantiate(GameManager.Instanse.PartsList.GetWeapon(data.WeaponRArmID));
        RAWeapon.transform.position = _rightArm[3].position;
        RAWeapon.transform.rotation = Quaternion.Euler(90, 0, 0);
        RAWeapon.transform.SetParent(_rightArm[3]);
        LAWeapon = Instantiate(GameManager.Instanse.PartsList.GetWeapon(data.WeaponLArmID));
        LAWeapon.transform.position = _leftArm[3].position;
        LAWeapon.transform.rotation = Quaternion.Euler(90, 0, 0);
        LAWeapon.transform.SetParent(_leftArm[3]);
        ShoulderWeapon = Instantiate(GameManager.Instanse.PartsList.GetShoulderWeapon(data.ShoulderWeaponID));
        ShoulderWeapon.RShoulder.position = _rArm.Shoulder.position;
        ShoulderWeapon.LShoulder.position = _lArm.Shoulder.position;
        ShoulderWeapon.RShoulder.SetParent(_rArm.Shoulder);
        ShoulderWeapon.LShoulder.SetParent(_lArm.Shoulder);
        ShoulderWeapon.transform.SetParent(_bodyBase[0]);
    }
}
