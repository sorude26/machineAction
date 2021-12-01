using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    List<DamageControl> _allAttackTarget = default;
    private void Awake()
    {
        Instance = this;
        _allAttackTarget = new List<DamageControl>();
    }

    public void AddTarget(DamageControl target)
    {
        _allAttackTarget.Add(target);
    }
    public void ReMoveTarget(DamageControl target)
    {
        _allAttackTarget.Remove(target);
    }
    public DamageControl GetTarget(Transform attacker)
    {
        return _allAttackTarget
            .Where(target => Vector3.Distance(target.Center.position, attacker.position) > 1f)
            .Where(target => Vector3.Dot((target.Center.position - attacker.position).normalized, attacker.forward.normalized) > 0.4f)
            .OrderBy(target => Vector3.Distance(target.Center.position, attacker.position)).FirstOrDefault();
    }
    public DamageControl GetTarget()
    {
        return _allAttackTarget
            .Where(target => Vector3.Distance(target.Center.position, Camera.main.transform.position) > 1f)
            .Where(target => Vector3.Dot((target.Center.position - Camera.main.transform.position).normalized, Camera.main.transform.forward.normalized) > 0.9f)
            .OrderBy(target => Vector3.Distance(target.Center.position, Camera.main.transform.position)).FirstOrDefault();
    }
}
