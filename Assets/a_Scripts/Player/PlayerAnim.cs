using System;
using System.Collections;
using a_Scripts;
using Animancer;
using HollowKnight.Param;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;


namespace HollowKnight.Anim
{
    
    public class PlayerAnim : MonoBehaviour
    {
        private AnimancerComponent _animancer;
        [Inject] private PlayerParam _param;
        [Inject] private AnimancerSetting _animSetting;

        private void Awake()
        {
            _animancer = GetComponent<AnimancerComponent>();
        }

        private void Update()
        {
           
        }


        //TODO:切换状态机中的状态
        public void TransitionTo(string animName, float fadeDuration)
        {
            var param = _animSetting.GetParam(animName);
            if (param == null) { return; }
            _animancer.Play(param.clip,fadeDuration);
        }
        
        


    }
}