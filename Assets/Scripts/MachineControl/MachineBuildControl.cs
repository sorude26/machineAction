using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBuildControl : MonoBehaviour
{
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
}
