using HollowKnight.Param;
using HollowKnight.Tools.FSM;
using UnityEngine;

namespace a_Scripts.Player.States
{
    public class PlayerIdleState : FSMState<PlayerAI>
    {
        public override void OnEnter(PlayerAI owner)
        {
            base.OnEnter(owner);
            
        }

        public override void OnUpdate(PlayerAI owner)
        {
            base.OnUpdate(owner);
            Debug.Log("aaa");
        }

        public override void OnExit(PlayerAI owner)
        {
            base.OnExit(owner);
        }

        public override void RegisterTransitions(BaseFSM<PlayerAI> fsm)
        {
            base.RegisterTransitions(fsm);
        }
    }
}