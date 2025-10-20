using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HollowKnight.Input
{
    
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerInput _input;
        public Vector2 inputDir;
        
        
        private void Awake()
        {
            _input = new PlayerInput();
            _input.GamePlay.Move.performed += ctx => inputDir = ctx.ReadValue<Vector2>().normalized;
            _input.GamePlay.Move.canceled += _ => inputDir  = Vector2.zero;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }
    }
}

