using System;
using HollowKnight.Anim;
using HollowKnight.Param;
using HollowKnight.Tools;
using UnityEngine;
using Zenject;

public class PlayerMotor : MonoBehaviour
{
    [Inject] private PlayerParam _param;
    [Inject] private CharacterData _data;
    [Inject] private ICollisionDetector _collisionDetector;
    [Inject] private PlayerAnim _anim;
    [SerializeField] private Transform _model;
    [SerializeField] private Rigidbody2D _rb;


    private Vector2 _frameVelocity;
    
    [Header("跳跃控制参数")] 
    private bool _jumpToConsume;
    private bool _grounded; //这是一个缓存值，存入的是“是否处于着地状态”
    private bool _endedJumpEarly;
    private bool _bufferedJumpUsable;
    private bool _coyoteUsable;
    private bool _cachedQueryStartInColliders;
    private float _timeJumpWasPressed;
    private float _frameLeftGrounded = float.MinValue;
    
    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _data.JumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _data.CoyoteTime;

    private float _time;


    private void Update()
    {
        GatherInput();
        _time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
        CheckCollisions(); 
        HandleJump();
        HandleDirection();
        HandleGravity();

        ApplyMovement();
    }

    private void Move()
    {
        if (_param.inputDir.x != 0)
        {
            _frameVelocity = new Vector3(_param.inputDir.x * _data.moveSpeed, _rb.velocity.y, 0);
            Flip();
        }
    }
    
    private void Flip()
    {
        _param.faceDir = (int)_model.localScale.x ;
        if (_param.inputDir.x > 0)
        {
            _param.faceDir = -2;
            _model.localScale = new Vector3(_param.faceDir, 2, 2);
        }
        else if (_param.inputDir.x < 0)
        {
            _param.faceDir = 2;
            _model.localScale = new Vector3(_param.faceDir, 2, 2);
        }
    }
    
    private void GatherInput()
    {
        if (_param.jumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;

        }

        if (_data.SnapInput)
        {
            var dir = _param.inputDir;
            dir.x = Mathf.Abs(dir.x) < _data.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(dir.x);
            dir.y = Mathf.Abs(dir.y) < _data.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(dir.y);
            _param.inputDir = dir;
        }
        
    }
    
    private void ApplyMovement() => _rb.velocity = _frameVelocity;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;
        bool groundHit = _collisionDetector.IsColliding(CollisionDirection.Ground);
        bool ceilingHit = _collisionDetector.IsColliding(CollisionDirection.Ceiling);
        
        if(ceilingHit)
        {
            _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);
        }

        if (!_grounded && groundHit)
        {
            // 不在地面但是地面检测成功，代表要落地了
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
        }
        else if(_grounded && !groundHit)
        {
            // 地面状态但是地面检测失败，代表要离开地面了
            _grounded = false;
            _frameLeftGrounded = _time;
        }
        
        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;

    }
    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_param.jumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;
        
        if(!_jumpToConsume && !HasBufferedJump) return;
        
        if(_grounded || CanUseCoyote) ExecuteJump();
        
        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        //触发跳跃
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = _data.JumpPower;
        _anim.TransitionTo(StringConstants.AnimName.JumpUp,0.1f);
    }

    private void HandleDirection()
    {
        if (_param.inputDir.x == 0)
        {
            var deceleration = _grounded ? _data.GroundDeceleration : _data.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x,_param.inputDir.x * _data.MaxSpeed, _data.Acceleration * Time.fixedDeltaTime);
        }
    }

    private void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0)
        {
            _frameVelocity.y = _data.GroundingForce;
        }
        else
        {
            var inAirGravity = _data.FallAcceleration;
            if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _data.JumpEndEarlyGravityModifier;
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_data.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

   
    
    
}