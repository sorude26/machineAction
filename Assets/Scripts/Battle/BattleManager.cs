﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [SerializeField]
    GameObject _clearMassage = default;

    List<DamageControl> _allAttackTarget = default;

    public Transform PlayerPos { get; set; }
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
        if (_allAttackTarget.Count == 0)
        {
            BattleEnd();
        }
    }
    public DamageControl GetTarget(float range)
    {
        return _allAttackTarget
            .Where(target => Vector3.Distance(target.Center.position, Camera.main.transform.position) > 0.3f)
            .Where(target => Vector3.Dot((target.Center.position - Camera.main.transform.position).normalized, Camera.main.transform.forward) > 0.9f)
            .Where(target => Vector3.Distance(target.Center.position, Camera.main.transform.position) < range)
            .OrderByDescending(target => Vector3.Dot((target.Center.position - Camera.main.transform.position).normalized, Camera.main.transform.forward)).FirstOrDefault();
    }
    void BattleEnd()
    {
        _clearMassage.SetActive(true);
        StartCoroutine(BattleEnd(3f));
    }
    public void GameEnd()
    {
        StartCoroutine(BattleEnd(5f));
    }
    IEnumerator BattleEnd(float time)
    {
        GameScene.InputManager.Instance.InputActionsOut();
        yield return new WaitForSeconds(time);
        SceneChange.LoadCustomize();
    }
}
