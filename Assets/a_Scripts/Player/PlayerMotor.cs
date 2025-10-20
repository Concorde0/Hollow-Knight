using HollowKnight.Param;
using UnityEngine;
using Zenject;

public class PlayerMotor : MonoBehaviour
{
    private Transform _model;
    private PlayerParam _param;

    [Inject]
    public void Construct(PlayerParam param, [Inject(Id = "PlayerModel")] Transform model)
    {
        _param = param;
        _model = model;
    }

    private void Update()
    {
        Flip();
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