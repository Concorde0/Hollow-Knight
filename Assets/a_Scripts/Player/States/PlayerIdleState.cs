using HollowKnight.Param;
using HollowKnight.Tools.FSM;
using UnityEngine;

namespace a_Scripts.Player.States
{
    public class PlayerIdleState : FSMState<PlayerController>
    {
        public override void OnEnter(PlayerController owner)
        {
            base.OnEnter(owner);
            
        }

        public override void OnUpdate(PlayerController owner)
        {
            base.OnUpdate(owner);
        }

        public override void OnExit(PlayerController owner)
        {
            base.OnExit(owner);
        }

        public override void RegisterTransitions(BaseFSM<PlayerController> fsm)
        {
            base.RegisterTransitions(fsm);
        }
    }
}