using HollowKnight.Input;
using HollowKnight.Param;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private PlayerMotor playerMotor;
    [SerializeField] private Transform model; 
    [SerializeField] private CharacterData playerData;


    public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromInstance(PlayerController).AsSingle();
        Container.Bind<PlayerInputController>().FromInstance(inputController).AsSingle();
        Container.Bind<PlayerMotor>().FromInstance(playerMotor).AsSingle();
        Container.Bind<PlayerParam>().AsSingle();
        
        Container.Bind<Transform>().WithId("PlayerModel").FromInstance(model).AsSingle();
        Container.Bind<CharacterData>().FromInstance(playerData).AsSingle();
    }
}