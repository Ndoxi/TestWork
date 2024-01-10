using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory
{
    private readonly string GOBLIN_PREFAB_KEY = "Characters/Goblin";
    private readonly string STRONG_GOBLIN_PREFAB_KEY = "Characters/StrongGoblin";

    private readonly DiContainer _container;
    private readonly Enemie _goblinPrefab;
    private readonly Enemie _strongGoblinPrefab;

    public EnemyFactory(DiContainer container)
    {
        _container = container;
        _goblinPrefab = Resources.Load<Enemie>(GOBLIN_PREFAB_KEY);
        _strongGoblinPrefab = Resources.Load<Enemie>(STRONG_GOBLIN_PREFAB_KEY);
    }

    public Enemie Create(Enemie.EnemyType enemyType, Vector3 spawnPosition)
    {
        Enemie enemyPrefab = null;
        switch (enemyType)
        {
            case Enemie.EnemyType.Goblin:
                enemyPrefab = _goblinPrefab;
                break;
            case Enemie.EnemyType.StrongGoblin:
                enemyPrefab = _strongGoblinPrefab;
                break;
        }

        Enemie newEnemy = _container.InstantiatePrefabForComponent<Enemie>(enemyPrefab);
        newEnemy.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        return newEnemy;
    }
}
