using HollowKnight.Tools;
using HollowKnight.Tools.FSM;
using UnityEngine;

namespace a_Scripts.Player.States
{
    public class PlayerMoveState : FSMState<PlayerAI>
    {
        
        public override void OnEnter(PlayerAI owner)
        {
            base.OnEnter(owner);
            owner.Motor.OnAnimEvent += OnMotorAnimEvent;
            owner.Motor.TriggerAnimEvent(StringConstants.AnimName.Move);
        }

        private void OnMotorAnimEvent(string obj)
        {
            
        }

        public override void OnUpdate(PlayerAI owner)
        {
            base.OnUpdate(owner);
        }

        public override void OnExit(PlayerAI owner)
        {
            base.OnExit(owner);
            owner.Motor.OnAnimEvent -= OnMotorAnimEvent;
            
        }

        public override void RegisterTransitions(BaseFSM<PlayerAI> fsm)
        {
            
            var idleAnim = new FSMCondition<PlayerAI>(m => m.Param.inputDir.sqrMagnitude < 0.1f);
            var jumpAnim = new FSMCondition<PlayerAI>(m => m.Param.canJump);
            AddCondition(jumpAnim,StringConstants.AnimName.Jump);
            AddCondition(idleAnim, StringConstants.AnimName.Idle);
        }
    }
}