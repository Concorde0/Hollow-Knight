using System;
using System.Collections;
using System.Collections.Generic;
using a_Scripts;
using Animancer;
using HollowKnight.Param;
using HollowKnight.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;


namespace HollowKnight.Anim
{
    
    public class PlayerAnim : MonoBehaviour
    {
        private AnimancerComponent _animancer;
        private Dictionary<string, AnimancerState> _animancerStates = new();
        [Inject] private PlayerParam _param;
        [Inject] private AnimancerSetting _animSetting;

        private void Awake()
        {
            _animancer = GetComponent<AnimancerComponent>();
        }

        private void Start()
        {
            AddState(StringConstants.AnimName.Idle, _animSetting.GetParam(StringConstants.AnimName.Idle).clip);
            AddState(StringConstants.AnimName.Move, _animSetting.GetParam(StringConstants.AnimName.Move).clip);
            AddState(StringConstants.AnimName.JumpUp, _animSetting.GetParam(StringConstants.AnimName.JumpUp).clip);
        }
        

        //TODO:切换状态机中的状态
        public void TransitionTo(string name, float fadeDuration = 0.25f)
        {
            if (_animancerStates.TryGetValue(name, out var state))
            {
                _animancer.Play(state, fadeDuration);
            }
            else
            {
                Debug.LogWarning($"未找到动画状态: {name}");
            }
        }


        private void AddState(string name, ClipTransition transition)
        {
            if (transition == null)
            {
                Debug.LogWarning($"动画 {name} 的 ClipTransition 为 null");
                return;
            }

            var state = _animancer.States.GetOrCreate(transition);
            _animancerStates[name] = state;
        }
        
        


    }
}