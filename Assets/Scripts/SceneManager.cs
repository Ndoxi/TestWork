using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager
{
    public event System.Action<bool> OnGameEnd;
    public event System.Action OnPlayerAdded;
    public event System.Action<int, int> UpdateWaveInfo;

    public Player Player { get; private set; }
    public readonly List<Enemie> Enemies;

    private int currWave = 0;
    private readonly LevelConfig _config;
    private readonly EnemyFactory _enemyFactory;

    public SceneManager(LevelConfig levelConfig, EnemyFactory enemyFactory)
    {
        _config = levelConfig;
        _enemyFactory = enemyFactory;
        Enemies = new List<Enemie>();
    }

    public void SetPlayer(Player player)
    {
        Player = player;
        OnPlayerAdded?.Invoke();
    }

    public void StartGame()
    {
        SpawnWave();
    }

    public void AddEnemie(Enemie enemie)
    {
        Enemies.Add(enemie);
    }

    public void RemoveEnemie(Enemie enemie)
    {
        Enemies.Remove(enemie);
        if(Enemies.Count == 0)
        {
            SpawnWave();
        }
    }

    public void GameOver()
    {
        OnGameEnd?.Invoke(false);
    }

    private void SpawnWave()
    {
        if (currWave >= _config.Waves.Length)
        {
            OnGameEnd?.Invoke(true);
            return;
        }

        var wave = _config.Waves[currWave];
        foreach (var enemyType in wave.Enemies)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            _enemyFactory.Create(enemyType, pos);
        }
        currWave++;
        UpdateWaveInfo?.Invoke(_config.Waves.Length, currWave);
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public Enemie GetClosestEnemy(Vector3 center)
    {
        var enemies = Enemies;
        Enemie closestEnemie = null;

        foreach (var enemie in enemies)
        {
            if (enemie == null)
            {
                continue;
            }

            if (closestEnemie == null)
            {
                closestEnemie = enemie;
                continue;
            }

            var distance = Vector3.Distance(center, enemie.transform.position);
            var closestDistance = Vector3.Distance(center, closestEnemie.transform.position);

            if (distance < closestDistance)
            {
                closestEnemie = enemie;
            }
        }

        return closestEnemie;
    }

    public List<Enemie> GetInCircle(float radius, Vector3 center)
    {
        var enemies = Enemies;
        List<Enemie> result = new List<Enemie>();

        foreach (var enemie in enemies)
        {
            if (Vector3.Distance(center, enemie.transform.position) > radius)
                continue;

            result.Add(enemie);
        }

        return result;
    }
}
