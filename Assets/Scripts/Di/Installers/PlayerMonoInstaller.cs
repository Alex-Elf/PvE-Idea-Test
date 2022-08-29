using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMonoInstaller : MonoInstaller
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovementController playerMovementController;
    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
        Container.Bind<PlayerMovementController>().FromInstance(playerMovementController).AsSingle();
    }
}
