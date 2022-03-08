using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateBase<TStateController>
{
    void OnEnter(TStateController owner);
    void OnUpdate(TStateController owner);
    bool OnLeave(TStateController owner, IStateBase<TStateController> next);
}
