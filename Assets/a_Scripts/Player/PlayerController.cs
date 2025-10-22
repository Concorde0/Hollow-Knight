using System.Collections;
using System.Collections.Generic;
using HollowKnight.Input;
using HollowKnight.Param;
using UnityEngine;
using Zenject;

public class PlayerController : Character
{
     private PlayerInputController _input;
     private PlayerMotor _motor;
     private PlayerParam _param;

    [Inject]
    public void Construct(PlayerParam param,PlayerMotor playerMotor,PlayerInputController playerInput)
    {
        _param = param;
        _motor = playerMotor;
        _input = playerInput;
    }


    private void Start()
    {
        Debug.Log(_input && _motor && _param != null ? "注入成功" : "注入失败");
    }
    

    protected override void Attack()
    {
        
    }

    protected override void Die()
    {
        
        
    }
}
