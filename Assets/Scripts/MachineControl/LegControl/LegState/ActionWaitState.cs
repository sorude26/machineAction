using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class LegControl
{
    public class ActionWaitState : IStateBase<LegControl>
    {
        bool _waitEnd = false;
        public void OnEnter(LegControl owner)
        {
            owner._move = MoveState.Stop;
            owner._landingTimer = 0; 
            _waitEnd = false;
            owner._animator.Play("JunpEnd");
            CameraEffectManager.Shake(owner.transform.position);
        }

        public bool OnLeave(LegControl owner, IStateBase<LegControl> next)
        {
            return _waitEnd;
        }

        public void OnUpdate(LegControl owner)
        {
            if (!owner._landing)
            {
                return;
            }
            owner._landingTimer += Time.deltaTime;
            if (owner._landingTimer >= owner._landingTime)
            {
                _waitEnd = true;
                owner._landing = false;
                owner._jump = false;
                owner._jumpEnd = false;
                owner._actionControl.ChengeState(owner._stateIdle);
                if (!owner._knockDown)
                {
                    owner._animator.Play("LandingEnd");
                }
            }
        }
    }
}