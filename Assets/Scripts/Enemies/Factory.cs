using UnityEngine;

public static class Factory
{
    private static readonly string PLAYER_PREFAB_KEY = "Characters/Player";
    private static readonly string GOBLIN_PREFAB_KEY = "Characters/Goblin";
    private static readonly string STRONG_GOBLIN_PREFAB_KEY = "Characters/StrongGoblin";

    private static Player _playerPrefab;
    private static Enemie _goblinPrefab;
    private static Enemie _strongGoblinPrefab;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        _playerPrefab = Resources.Load<Player>(PLAYER_PREFAB_KEY);
        _goblinPrefab = Resources.Load<Enemie>(GOBLIN_PREFAB_KEY);
        _strongGoblinPrefab = Resources.Load<Enemie>(STRONG_GOBLIN_PREFAB_KEY);
    }

    public static Player SpawnPlayer(Vector3 position)
    {
        return Object.Instantiate(_playerPrefab, position, Quaternion.identity);
    }

    public static Enemie SpawnEnemy(Enemie.EnemyType type, Vector3 position)
    {
        Enemie prefab = null;
        switch (type)
        {
            case Enemie.EnemyType.Goblin:
                prefab = _goblinPrefab;
                break;
            case Enemie.EnemyType.StrongGoblin:
                prefab = _strongGoblinPrefab;
                break;
        }
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }
}
