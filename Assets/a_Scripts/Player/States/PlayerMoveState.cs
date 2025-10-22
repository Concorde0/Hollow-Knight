using HollowKnight.Tools.FSM;
using UnityEngine;

namespace a_Scripts.Player.States
{
    public class PlayerMoveState : FSMState<PlayerAI>
    {
        public override void OnUpdate(PlayerAI owner)
        {
            base.OnUpdate(owner);
            Debug.Log("Player is Moving");
        }
    }
}