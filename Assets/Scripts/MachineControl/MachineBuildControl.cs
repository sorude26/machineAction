using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBuildControl : MonoBehaviour
{
    [SerializeField]
    UnitBuildData _buildData = default;
    [SerializeField]
    Transform _camera = default;
    [SerializeField]
    Transform[] _bodyBase = new Transform[2];
    [SerializeField]
    Transform[] _rightArm = new Transform[4];
    [SerializeField]
    Transform[] _leftArm = new Transform[4];
    [SerializeField]
    Transform[] _legBase = new Transform[6];
    [SerializeField]
    Transform[] _rightLeg = new Transform[3];
    [SerializeField]
    Transform[] _leftLeg = new Transform[3];

    public Transform LegBase { get; set; }

    public void StartSet(PartsManager manager)
    {
        Build(_buildData, manager);
    }
    public void SetData(UnitBuildData data)
    {
        _buildData = data;
    }
    public void Build(UnitBuildData data, PartsManager manager)
    {
        manager.Leg = Instantiate(GameManager.Instance.PartsList.GetLeg(data.LegID));
        manager.Leg.transform.position = _legBase[0].position;
        if (manager.Leg.Type == LegType.Animation)
        {
            _legBase[3].position = manager.Leg.LegTop.position;
            _legBase[5].SetParent(manager.Leg.LegTop);
            manager.Leg.transform.SetParent(transform);
            LegBase = manager.Leg.transform;
        }
        else
        {
            _legBase[3].position = manager.Leg.LegTop.position;
            manager.Leg.transform.SetParent(_legBase[3]);
            LegBase = transform;
        }
        _rightLeg[0].position = manager.Leg.RLeg1.position;
        _rightLeg[1].position = manager.Leg.RLeg2.position;
        _rightLeg[2].position = manager.Leg.RLeg3.position;
        _rightLeg[3].position = manager.Leg.RLeg3.position;
        _leftLeg[0].position = manager.Leg.LLeg1.position;
        _leftLeg[1].position = manager.Leg.LLeg2.position;
        _leftLeg[2].position = manager.Leg.LLeg3.position;
        _leftLeg[3].position = manager.Leg.LLeg3.position;
        manager.Leg.RLeg3.SetParent(_rightLeg[2]);
        manager.Leg.RLeg2.SetParent(_rightLeg[1]);
        manager.Leg.RLeg1.SetParent(_rightLeg[0]);
        manager.Leg.LLeg3.SetParent(_leftLeg[2]);
        manager.Leg.LLeg2.SetParent(_leftLeg[1]);
        manager.Leg.LLeg1.SetParent(_leftLeg[0]);
        manager.Body = Instantiate(GameManager.Instance.PartsList.GetBody(data.BodyID));
        manager.Body.transform.position = _bodyBase[0].position;
        _bodyBase[1].position = manager.Body.HeadPos.position;
        _camera.transform.position = manager.Body.HeadPos.position;
        manager.Body.transform.SetParent(_bodyBase[0]);
        _rightArm[0].position = manager.Body.RArmPos.position;
        _leftArm[0].position = manager.Body.LArmPos.position;
        manager.RArm = Instantiate(GameManager.Instance.PartsList.GetRArm(data.RArmID));
        manager.RArm.transform.position = _rightArm[0].position;
        manager.RArm.transform.SetParent(_rightArm[0]);
        _rightArm[1].position = manager.RArm.ArmTop.position;
        _rightArm[2].position = manager.RArm.ArmBottom.position;
        _rightArm[3].position = manager.RArm.Grip.position;
        manager.RArm.Grip.SetParent(_rightArm[3]);
        manager.RArm.ArmBottom.SetParent(_rightArm[2]);
        manager.RArm.ArmTop.SetParent(_rightArm[1]);
        manager.LArm = Instantiate(GameManager.Instance.PartsList.GetLArm(data.LArmID));
        manager.LArm.transform.position = _leftArm[0].position;
        manager.LArm.transform.SetParent(_leftArm[0]);
        _leftArm[1].position = manager.LArm.ArmTop.position;
        _leftArm[2].position = manager.LArm.ArmBottom.position;
        _leftArm[3].position = manager.LArm.Grip.position;
        manager.LArm.Grip.SetParent(_leftArm[3]);
        manager.LArm.ArmBottom.SetParent(_leftArm[2]);
        manager.LArm.ArmTop.SetParent(_leftArm[1]);
        manager.Head = Instantiate(GameManager.Instance.PartsList.GetHead(data.HeadID));
        manager.Head.transform.position = _bodyBase[1].position;
        manager.Head.transform.SetParent(_bodyBase[1]);
        manager.Booster = Instantiate(GameManager.Instance.PartsList.GetBooster(data.BoosterID));
        manager.Booster.transform.position = manager.Body.BackPos.position;
        manager.Booster.transform.SetParent(manager.Body.BackPos);
        manager.RAWeapon = Instantiate(GameManager.Instance.PartsList.GetWeapon(data.WeaponRArmID));
        manager.RAWeapon.transform.position = _rightArm[3].position;
        manager.RAWeapon.transform.rotation = Quaternion.Euler(90, 0, 0);
        manager.RAWeapon.transform.SetParent(manager.RArm.Grip);
        manager.LAWeapon = Instantiate(GameManager.Instance.PartsList.GetWeapon(data.WeaponLArmID));
        manager.LAWeapon.transform.position = _leftArm[3].position;
        manager.LAWeapon.transform.rotation = Quaternion.Euler(90, 0, 0);
        manager.LAWeapon.transform.SetParent(manager.LArm.Grip);
        manager.BodyWeapon = Instantiate(GameManager.Instance.PartsList.GetBodyWeapon(data.BodyWeaponID));
        manager.BodyWeapon.transform.position = manager.Body.BackPos.position;
        manager.BodyWeapon.transform.SetParent(manager.Body.BackPos);
        manager.ShoulderWeapon = Instantiate(GameManager.Instance.PartsList.GetShoulderWeapon(data.ShoulderWeaponID));
        manager.ShoulderWeapon.RShoulder.position = manager.RArm.Shoulder.position;
        manager.ShoulderWeapon.LShoulder.position = manager.LArm.Shoulder.position;
        manager.ShoulderWeapon.RShoulder.SetParent(manager.RArm.Shoulder);
        manager.ShoulderWeapon.LShoulder.SetParent(manager.LArm.Shoulder);
        manager.ShoulderWeapon.transform.SetParent(_bodyBase[0]);
        manager.LArm.SetBody(manager.Body);
        manager.RArm.SetBody(manager.Body);
    }
    public void Purge(Transform parent)
    {
        transform.SetParent(parent);
        _legBase[5].SetParent(transform);
    }
}
