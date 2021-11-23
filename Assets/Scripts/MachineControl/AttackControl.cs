using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    string[] _lArmBlade = { "attackSwingLArm", "attackSwingLArm2", "attackSwingLArm3" };
    string[] _rArmBlade = { "attackSwingRArm", "attackSwingRArm2", "attackSwingRArm3" };
    string[] _dArmBlade = { "attackSwingDArm", "attackSwingDArm2", "attackSwingDArm3" };
    string[] _dArmKnuckle = { "attackKnuckleRArm", "attackKnuckleLArm", "attackKnuckleRArm" };
    string[] _lArmBladeRKnuckle = { "attackSwingLArm", "attackSwingLArm2", "attackKnuckleRArm" };
    string[] _rArmBladeLKnuckle = { "attackSwingRArm", "attackSwingRArm2", "attackKnuckleLArm" };
}
