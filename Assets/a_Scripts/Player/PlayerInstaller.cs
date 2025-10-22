using a_Scripts;
using a_Scripts.Player;
using a_Scripts.Player.States;
using HollowKnight.Anim;
using HollowKnight.Input;
using HollowKnight.Param;
using HollowKnight.Tools.FSM;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
  
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private AnimancerSetting _animancerSetting;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerMotor _playerMotor;
    [SerializeField] private PlayerAnim _playerAnim;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(_playerController).AsSingle();
        Container.Bind<CharacterData>().FromInstance(_characterData).AsSingle();
        Container.Bind<PlayerMotor>().FromInstance(_playerMotor).AsSingle();
        Container.Bind<PlayerAnim>().FromInstance(_playerAnim).AsSingle();
        Container.Bind<AnimancerSetting>().FromInstance(_animancerSetting).AsSingle();
        Container.Bind<PlayerParam>().AsSingle();
    }
}