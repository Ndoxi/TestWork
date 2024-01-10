using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerFactory>().AsSingle();
        Container.Bind<EnemyFactory>().AsSingle();
    }
}