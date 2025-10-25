using HollowKnight.Param;
using HollowKnight.Tools;
using HollowKnight.Tools.FSM;
using UnityEngine;

namespace a_Scripts.Player.States
{
    public class PlayerIdleState : FSMState<PlayerAI>
    {
        public override void OnEnter(PlayerAI owner)
        {
            base.OnEnter(owner);
            owner.Motor.OnAnimEvent += OnMotorAnimEvent;
            owner.Motor.TriggerAnimEvent(StringConstants.AnimName.Idle);
        }

        public override void OnUpdate(PlayerAI owner)
        {
            base.OnUpdate(owner);
            Debug.Log("Idle");
        }

        private void OnMotorAnimEvent(string obj)
        {
            
        }

        public override void OnExit(PlayerAI owner)
        {
            base.OnExit(owner);
            owner.Motor.OnAnimEvent -= OnMotorAnimEvent;
        }


        public override void RegisterTransitions(BaseFSM<PlayerAI> fsm)
        {
            base.RegisterTransitions(fsm);
            
            var moveAnim = new FSMCondition<PlayerAI>(m => m.Param.inputDir.sqrMagnitude >= 0.1f);
            var jumpState = new FSMCondition<PlayerAI>(m => m.Param.canJump);
            AddCondition(jumpState,StringConstants.AnimName.Jump);
            AddCondition(moveAnim, StringConstants.AnimName.Move);
        }
    }
}