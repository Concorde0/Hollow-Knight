using HollowKnight.Input;
using HollowKnight.Param;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private PlayerMotor playerMotor;
    [SerializeField] private Transform model; 
    [SerializeField] private CharacterData playerData;


    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerMotor>().FromInstance(playerMotor).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(playerController).AsSingle();
        Container.Bind<PlayerInputController>().FromInstance(inputController).AsSingle();
        Container.Bind<PlayerParam>().AsSingle();
        
        Container.Bind<CharacterData>().FromInstance(playerData).AsSingle();


    }
}