using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using HollowKnight.Param;

namespace HollowKnight.Input
{
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerInput _input;
        private PlayerParam _param;

        [Inject]
        public void Construct(PlayerParam param)
        {
            _param = param;
        }

        private void Awake()
        {
            _input = new PlayerInput();
            _input.GamePlay.Move.performed += ctx => _param.inputDir = ctx.ReadValue<Vector2>().normalized;
            _input.GamePlay.Move.canceled += _ => _param.inputDir = Vector2.zero;
        }

        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();
    }
}