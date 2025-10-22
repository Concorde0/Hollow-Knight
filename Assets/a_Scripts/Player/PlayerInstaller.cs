using a_Scripts.Player;
using a_Scripts.Player.States;
using HollowKnight.Input;
using HollowKnight.Param;
using HollowKnight.Tools.FSM;
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
        // 基础绑定
        Container.BindInterfacesAndSelfTo<PlayerMotor>().FromInstance(playerMotor).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(playerController).AsSingle();
        Container.Bind<PlayerInputController>().FromInstance(inputController).AsSingle();
        Container.Bind<PlayerParam>().AsSingle();
        Container.Bind<CharacterData>().FromInstance(playerData).AsSingle();
        
        // FSM绑定
        Container.Bind<BaseFSM<PlayerController>>().AsSingle().WithArguments(playerController);

        // PlayerAI绑定（假设挂在同一个GameObject上）
        Container.Bind<PlayerAI>().FromComponentInHierarchy().AsSingle();

       


    }
}