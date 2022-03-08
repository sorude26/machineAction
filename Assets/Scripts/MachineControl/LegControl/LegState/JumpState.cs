using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class LegControl
{
    public class JumpState : IStateBase<LegControl>
    {
        public void OnEnter(LegControl owner)
        {
            owner._jump = true;
            owner._jumpEnd = false;
        }

        public bool OnLeave(LegControl owner, IStateBase<LegControl> next)
        {
            if (owner._float)
            {
                return false;
            }
            return true;
        }

        public void OnUpdate(LegControl owner)
        {
            if (owner._knockDown ||owner._float ||!owner._jumpEnd)
            {
                return;
            }
            if(owner._machine.IsGrounded())
            {
                owner._actionControl.ChengeState(owner._stateWait);
            }
        }
    }
}