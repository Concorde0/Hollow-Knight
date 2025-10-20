using HollowKnight.Input;
using HollowKnight.Param;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private PlayerMotor playerMotor;
    [SerializeField] private Transform model; 

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<PlayerInputController>().FromInstance(inputController).AsSingle();
        Container.Bind<PlayerMotor>().FromInstance(playerMotor).AsSingle();
        Container.Bind<PlayerParam>().AsSingle();
        
        Container.Bind<Transform>().WithId("PlayerModel").FromInstance(model).AsSingle();
    }
}