using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using HollowKnight.Input;
using UnityEngine;

namespace HollowKnight.Anim
{
    public class PlayAnimationOnEnable : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent animancer;
        [SerializeField] private AnimationClip _Move;
        [SerializeField] private AnimationClip _Idle;
        private PlayerInputController _playerInputController;

        private void Awake()
        {
            _playerInputController = GetComponent<PlayerInputController>();
        }

        private void OnEnable()
        {
            
        }

        private void Update()
        {
            animancer.Play(_playerInputController.inputDir.x != 0 ? _Move : _Idle);
        }
    }
}

