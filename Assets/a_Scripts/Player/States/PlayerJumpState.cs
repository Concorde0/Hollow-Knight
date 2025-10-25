using System.Collections;
using HollowKnight.Tools;
using HollowKnight.Tools.FSM;
using RPG.Timer;
using UnityEngine;

namespace a_Scripts.Player.States
{
    public class PlayerJumpState : FSMState<PlayerAI>
    {
        private bool canTrans;
        private float jumpUpLength;
        private float jumpSoftTime;
        private bool jumpSoftIsStart;
        public override void OnEnter(PlayerAI owner)
        {
            base.OnEnter(owner);
            owner.Motor.OnAnimEvent += OnMotorAnimEvent;
            owner.Param.canJump = false;
            canTrans = false;
            jumpSoftIsStart = false;
            jumpUpLength = owner.Motor.GetAnimLength(StringConstants.AnimName.JumpUp);
            jumpSoftTime = owner.Motor.GetAnimLength(StringConstants.AnimName.JumpSoft);
            TimerManager.Instance.StartTimer(StringConstants.TimerName.JumpLoadTime,0.02f);
            TimerManager.Instance.StartTimer(StringConstants.TimerName.JumpUpTime,jumpUpLength);
        }

        private void OnMotorAnimEvent(string obj)
        {
            
        }


        public override void OnUpdate(PlayerAI owner)
        {
            base.OnUpdate(owner);
           
            if (TimerManager.Instance.IsFinished(StringConstants.TimerName.JumpUpTime) && !owner.Motor.CheckCollider(CollisionDirection.Ground))
            {
                owner.Motor.TriggerAnimEvent(StringConstants.AnimName.JumpLoop);
            }
            if (TimerManager.Instance.IsFinished(StringConstants.TimerName.JumpLoadTime) && owner.Motor.CheckCollider(CollisionDirection.Ground))
            {
                owner.Motor.TriggerAnimEvent(StringConstants.AnimName.JumpSoft);
                if (!jumpSoftIsStart)
                {
                    TimerManager.Instance.StartTimer(StringConstants.TimerName.JumpSoftTime,jumpSoftTime);
                    jumpSoftIsStart = true;
                }
                
                if(TimerManager.Instance.IsFinished(StringConstants.TimerName.JumpSoftTime))
                {
                    canTrans = true;
                }
                
            }
        }

        public override void OnExit(PlayerAI owner)
        {
            base.OnExit(owner);
            owner.Motor.OnAnimEvent -= OnMotorAnimEvent;
            canTrans = false;
            jumpSoftIsStart = false;
            TimerManager.Instance.Exists(StringConstants.TimerName.JumpUpTime);
            TimerManager.Instance.Exists(StringConstants.TimerName.JumpLoadTime);
            TimerManager.Instance.CleanupFinished();
        }   

        public override void RegisterTransitions(BaseFSM<PlayerAI> fsm)
        {
            base.RegisterTransitions(fsm);
            var idleAnim = new FSMCondition<PlayerAI>(m=>m.Param.inputDir.sqrMagnitude < 0.1f && m.Motor.CheckCollider(CollisionDirection.Ground) && canTrans);
            var moveAnim = new FSMCondition<PlayerAI>(m=>m.Param.inputDir.sqrMagnitude >= 0.1f && m.Motor.CheckCollider(CollisionDirection.Ground) && canTrans);
            AddCondition(idleAnim,StringConstants.AnimName.Idle);
            AddCondition(moveAnim, StringConstants.AnimName.Move);
            
            
           
        }
    }
}