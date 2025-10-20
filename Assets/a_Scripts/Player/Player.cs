using System;
using HollowKnight.Input;
using UnityEngine;
using Zenject;

public class Player : Character
{
    [Inject] private PlayerInputController _input;
    [Inject] private PlayerMotor _motor;
    
    private void Start()
    {
        Debug.Log(_input != null ? "Input注入成功" : "Input注入失败");
    }


    protected override void Awake()
    {
        
    }

    protected override void Attack()
    {
        
    }

    protected override void Die()
    {
        
        
    }
}
