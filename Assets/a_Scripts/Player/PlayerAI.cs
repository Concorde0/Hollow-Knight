using System;
using a_Scripts.Player.States;
using HollowKnight.Param;
using HollowKnight.Tools;
using HollowKnight.Tools.FSM;
using UnityEngine;
using Zenject;

namespace a_Scripts.Player
{
    public class PlayerAI : MonoBehaviour
    {
        private BaseFSM<PlayerAI> _fsm;

        private void Start()
        {
            _fsm = new BaseFSM<PlayerAI>(this);
            
            _fsm.AddState(StringConstants.AnimName.Idle, new PlayerIdleState());
            _fsm.AddState(StringConstants.AnimName.Move, new PlayerMoveState());
            _fsm.AddState(StringConstants.AnimName.JumpUp,new PlayerJumpUpState());
            _fsm.AddState(StringConstants.AnimName.JumpLoop,new PlayerJumpLoopState());
            _fsm.AddState(StringConstants.AnimName.JumpSoft, new PlayerJumpSoftState());
            _fsm.AddState(StringConstants.AnimName.JumpHard, new PlayerJumpHardState());
            _fsm.SetDefault(StringConstants.AnimName.Idle);
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}