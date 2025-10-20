using System;
using HollowKnight.Param;
using UnityEngine;
using Zenject;

public class PlayerMotor : MonoBehaviour
{
    private Transform _model;
    private PlayerParam _param;
    private CharacterData _data;

    [Inject]
    public void Construct(PlayerParam param, [Inject(Id = "PlayerModel")] Transform model, CharacterData data)
    {
        _param = param;
        _model = model;
        _data = data;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
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
}