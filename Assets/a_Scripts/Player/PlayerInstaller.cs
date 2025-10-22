using a_Scripts.Player;
using a_Scripts.Player.States;
using HollowKnight.Anim;
using HollowKnight.Input;
using HollowKnight.Param;
using HollowKnight.Tools.FSM;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
  
    [SerializeField] private CharacterData characterData;
    [SerializeField] private PlayerController _playerController;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(_playerController).AsSingle();
        Container.Bind<PlayerParam>().AsSingle();
        Container.Bind<CharacterData>().FromInstance(characterData).AsSingle();
    }
}