using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private PlayerFactory _playerFactory;

    public override void InstallBindings()
    {
        Container.Bind<SceneManager>().AsSingle().NonLazy();
        Container.Bind<PlayerAttackService>().AsSingle();
    }

    [Inject]
    private void Construct(PlayerFactory playerFactory)
    {
        _playerFactory = playerFactory;
    }

    public override void Start()
    {
        base.Start();
        _playerFactory.Create(Vector3.zero);
    }
}