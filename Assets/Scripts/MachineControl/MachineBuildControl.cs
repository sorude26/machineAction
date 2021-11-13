using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBuildControl : MonoBehaviour
{
    [SerializeField]
    UnitBuildData m_test = default;
    [SerializeField]
    Transform m_camera = default;
    [SerializeField]
    Transform[] m_bodyBase = new Transform[2];
    [SerializeField]
    Transform[] m_rightArm = new Transform[4];
    [SerializeField]
    Transform[] m_leftArm = new Transform[4];
    [SerializeField]
    Transform[] m_legBase = new Transform[5];
    [SerializeField]
    Transform[] m_rightLeg = new Transform[3];
    [SerializeField]
    Transform[] m_leftLeg = new Transform[3];

    PartsHead m_head = default;
    PartsBody m_body = default;
    PartsArm m_rArm = default;
    PartsArm m_lArm = default;
    PartsLeg m_leg = default;
    WeaponMaster m_rAWeapon = default;
    WeaponMaster m_lAWeapon = default;
    WeaponMaster m_bodyWeapon = default;
    WeaponMaster m_shoulderWeapon = default;
    private void Start()
    {
        Build(m_test);
    }
    public void Build(UnitBuildData data)
    {
        m_leg = Instantiate(GameManager.Instanse.PartsList.GetLeg(data.LegID));
        m_leg.transform.position = m_legBase[0].position;
        m_legBase[3].position = m_leg.LegTop.position;
        m_leg.transform.SetParent(m_legBase[3]);
        m_rightLeg[0].position = m_leg.RLeg1.position;
        m_rightLeg[1].position = m_leg.RLeg2.position;
        m_rightLeg[2].position = m_leg.RLeg3.position;
        m_leftLeg[0].position = m_leg.LLeg1.position;
        m_leftLeg[1].position = m_leg.LLeg2.position;
        m_leftLeg[2].position = m_leg.LLeg3.position;
        m_leg.RLeg3.SetParent(m_rightLeg[2]);
        m_leg.RLeg2.SetParent(m_rightLeg[1]);
        m_leg.RLeg1.SetParent(m_rightLeg[0]);
        m_leg.LLeg3.SetParent(m_leftLeg[2]);
        m_leg.LLeg2.SetParent(m_leftLeg[1]);
        m_leg.LLeg1.SetParent(m_leftLeg[0]);
        m_body = Instantiate(GameManager.Instanse.PartsList.GetBody(data.BodyID));
        m_body.transform.position = m_bodyBase[0].position;
        m_bodyBase[1].position = m_body.HeadPos.position;
        m_camera.transform.position = m_body.HeadPos.position;
        m_body.transform.SetParent(m_bodyBase[0]);
        m_rightArm[0].position = m_body.RArmPos.position;
        m_leftArm[0].position = m_body.LArmPos.position;
        m_rArm = Instantiate(GameManager.Instanse.PartsList.GetRArm(data.RArmID));
        m_rArm.transform.position = m_rightArm[0].position;
        m_rArm.transform.SetParent(m_rightArm[0]);
        m_rightArm[1].position = m_rArm.ArmTop.position;
        m_rightArm[2].position = m_rArm.ArmBottom.position;
        m_rightArm[3].position = m_rArm.Grip.position;
        m_rArm.Grip.SetParent(m_rightArm[3]);
        m_rArm.ArmBottom.SetParent(m_rightArm[2]);
        m_rArm.ArmTop.SetParent(m_rightArm[1]);
        m_lArm = Instantiate(GameManager.Instanse.PartsList.GetLArm(data.LArmID));
        m_lArm.transform.position = m_leftArm[0].position;
        m_lArm.transform.SetParent(m_leftArm[0]);
        m_leftArm[1].position = m_lArm.ArmTop.position;
        m_leftArm[2].position = m_lArm.ArmBottom.position;
        m_leftArm[3].position = m_lArm.Grip.position;
        m_lArm.Grip.SetParent(m_leftArm[3]);
        m_lArm.ArmBottom.SetParent(m_leftArm[2]);
        m_lArm.ArmTop.SetParent(m_leftArm[1]);
        m_head = Instantiate(GameManager.Instanse.PartsList.GetHead(data.HeadID));
        m_head.transform.position = m_bodyBase[1].position;
        m_head.transform.SetParent(m_bodyBase[1]);
    }
}
