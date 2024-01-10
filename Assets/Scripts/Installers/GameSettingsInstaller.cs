using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SctiptableObjectInstaller", menuName = "Installers/GameSettings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public LevelConfig LevelConfig;
    public AttacksData AttacksData;

    public override void InstallBindings()
    {
        Container.BindInstances(LevelConfig, AttacksData);
    }
}