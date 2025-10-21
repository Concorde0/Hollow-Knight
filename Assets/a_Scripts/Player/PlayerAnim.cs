using System;
using System.Collections;
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
        [FormerlySerializedAs("animancer")] 
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private Animator _anim;
        [SerializeField] private AnimationClip _Move;
        [SerializeField] private AnimationClip _Idle;
        [SerializeField] private AnimationClip _JumpUp;
        [SerializeField] private AnimationClip _JumpLoop;
        [SerializeField] private AnimationClip _JumpSoft;
        [SerializeField] private AnimationClip _JumpHard;
        
        // [Header("Particles")] 
        // [SerializeField] private ParticleSystem _jumpParticles;
        // [SerializeField] private ParticleSystem _launchParticles;
        // [SerializeField] private ParticleSystem _moveParticles;
        // [SerializeField] private ParticleSystem _landParticles;

        [Header("Idle Settings")]
        [SerializeField] private float _maxIdleSpeed = 1.2f;
        
        [Header("Audio Clips")] 
        [SerializeField] private AudioClip[] _footsteps;
        private AudioSource _source;
        
        
        private ParticleSystem.MinMaxGradient _currentGradient;
        private PlayerParam _param;
        private IPlayerMotor _playerMotor;
        
        private bool _grounded;
        private AnimancerState _currentState;
        
        private bool _isJumping;


        [Inject]
        public void Construct(PlayerParam param,IPlayerMotor playerMotor)
        {
            _param = param;
            _playerMotor = playerMotor;
        }

        private void Awake()
        {
            if (_animancer != null && _Idle != null)
            {
                _currentState = _animancer.Play(_Idle);
            }
        }


        private void OnEnable()
        {
            _playerMotor.Jumped += OnJumped;
            _playerMotor.GroundedChanged += OnGroundedChanged;
        }

       

        private void OnDisable()
        {
            _playerMotor.Jumped -= OnJumped;
            _playerMotor.GroundedChanged -= OnGroundedChanged;
        }

        private void Update()
        {
            if (_animancer == null) return;
            MoveAnim();
            
            HandleIdleSpeed();
            
            // HandleCharacterTilt();
        }

        private void MoveAnim()
        {
            if (_isJumping || (_currentState != null && 
                               (_currentState.Clip == _JumpUp || _currentState.Clip == _JumpLoop || 
                                _currentState.Clip == _JumpSoft || _currentState.Clip == _JumpHard))) return;

            _currentState = _animancer.Play(_param.inputDir.x != 0 ? _Move : _Idle);
        }
        
        private void HandleIdleSpeed()
        {
            var inputStrength = Mathf.Abs(_param.inputDir.x);
            if ( _currentState.Clip == _Idle)
            {
                _currentState.Speed = Mathf.Lerp(1f, _maxIdleSpeed, inputStrength);
            }
          
        }
       
        
        
        private void OnJumped()
        {
            if (_animancer == null || _JumpUp == null) return;

            _isJumping = true;
            _currentState = _animancer.Play(_JumpUp, 0.1f);
            StartCoroutine(PlayJumpLoopWhenDone());
            
            // if (_grounded)
            // {
            //     _jumpParticles?.Play();
            //     _launchParticles?.Play();
            // }
        }

        private IEnumerator PlayJumpLoopWhenDone()
        {
            if (_currentState == null) yield break;
            
            while (_currentState.NormalizedTime < 1f)
                yield return null;
            
            if (_isJumping && !_grounded && _JumpLoop != null)
            {
                _currentState = _animancer.Play(_JumpLoop, 0.1f);
            }
        }


        private void OnGroundedChanged(bool grounded, float impact)
        {
            _grounded = grounded;

            if (grounded)
            {
                _isJumping = false; 
                // 根据冲击强度选择软/硬着地动画
                AnimationClip landClip = impact > 20f ? _JumpHard : _JumpSoft;
                if (_animancer != null && landClip != null)
                {
                    // 停掉可能正在播放的跳跃动画
                    _currentState?.Stop();
                    _currentState = _animancer.Play(landClip, 0.1f);
                    StartCoroutine(WaitForLandAnimationThenIdle());
                }

                // 音效
                if (_footsteps != null && _footsteps.Length > 0 && _source != null)
                    _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);

                // // 移动粒子
                // if (_moveParticles != null && !_moveParticles.isPlaying)
                //     _moveParticles.Play();
                //
                // // 着地粒子缩放和播放
                // if (_landParticles != null)
                // {
                //     _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, 40, impact);
                //     _landParticles.Play();
                // }
            }
            else
            {
                // 离地：停止移动粒子
                // _moveParticles?.Stop();

                // 离地时停止当前播放状态
                _currentState?.Stop();
            }
        }
        
        private IEnumerator WaitForLandAnimationThenIdle()
        {
            if (_currentState == null) yield break;

            while (_currentState.NormalizedTime < 1f)
                yield return null;
            
            _currentState = _animancer.Play(_param.inputDir.x != 0 ? _Move : _Idle);
        }



       
        
        

        
    }
}