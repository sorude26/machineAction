using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class LegControl
{
    public class GroundMoveState : IStateBase<LegControl>
    {
        public void OnEnter(LegControl owner)
        {

        }

        public bool OnLeave(LegControl owner, IStateBase<LegControl> next)
        {
            return true;
        }

        public void OnUpdate(LegControl owner)
        {
            if (owner._knockDown)
            {
                return;
            }
            if (!owner._machine.IsGrounded())
            {
                owner.ChangeAnimation("Junp");
                owner._actionControl.ChengeState(owner._stateJump);
            }
        }
    }
}