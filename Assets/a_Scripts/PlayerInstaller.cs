using HollowKnight.Input;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerInputController inputController;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<PlayerInputController>().FromInstance(inputController).AsSingle();
    }
}