using System;
using HollowKnight.Param;
using UnityEngine;
using Zenject;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private Transform _model;
    [SerializeField] private Rigidbody2D _rb;
    private PlayerParam _param;
    private CharacterData _data;
    private ICollisionDetector _collisionDetector;
    
    [Header("Jump Variables")]
    private Vector2 _frameVelocity;
    private float _frameLeftGrounded = float.MinValue;
    private bool _grounded;
    
    private float _time;
    
    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;
    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _data.JumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _data.CoyoteTime;
    
    private bool _cachedQueryStartInColliders;
    

    [Inject]
    public void Construct(PlayerParam param, CharacterData data, ICollisionDetector collisionDetector)
    {
        _param = param;
        _data = data;
        _collisionDetector = collisionDetector;
    }

    private void Awake()
    {
        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
        CheckCollisions();

        HandleJump();
        HandleDirection();
        HandleGravity();
        
    }

    private void Move()
    {
        if(_param.inputDir.x != 0)
        {
            Flip();
            Vector2 move = new Vector2(-_param.faceDir / 2, 0) * (_data.moveSpeed * Time.deltaTime);
            transform.position += (Vector3)move;
        }
    }
    private void Flip()
    {
        if (_model == null) return;

        _param.faceDir = (int)_model.localScale.x;

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
    
    
    #region Collisions
    
  

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = _collisionDetector.IsColliding(CollisionDirection.Ground);
        bool ceilingHit = _collisionDetector.IsColliding(CollisionDirection.Up);

        // Hit a Ceiling
        if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
            // GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
        }
        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = _time;
            // GroundedChanged?.Invoke(false, 0);
        }
        
        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        
    }

    #endregion
    
    
    #region Jumping
   
    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_param.jumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = _data.JumpPower;
        // Jumped?.Invoke();
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        if (_param.inputDir.x == 0)
        {
            var deceleration = _grounded ? _data.GroundDeceleration : _data.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _param.inputDir.x * _data.MaxSpeed, _data.Acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0f)
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

    #endregion
    
    
    
    
}
