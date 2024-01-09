using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public event System.Action<int, int> UpdateWaveInfo;

    public static SceneManager Instance;

    [SerializeField]
    private Player _playerPrefab;
    [SerializeField]
    private Vector3 _playersSpawnPos;

    public Player Player { get; private set; }
    [Space]
    public List<Enemie> Enemies;
    public GameObject Lose;
    public GameObject Win;

    private int currWave = 0;
    [SerializeField] private LevelConfig Config;

    private void Awake()
    {
        Instance = this;
        Player player = Instantiate(_playerPrefab);
        player.transform.position = _playersSpawnPos;
        Player = player;
    }

    private void Start()
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
        Lose.SetActive(true);
    }

    private void SpawnWave()
    {
        if (currWave >= Config.Waves.Length)
        {
            Win.SetActive(true);
            return;
        }

        var wave = Config.Waves[currWave];
        foreach (var character in wave.Characters)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(character, pos, Quaternion.identity);
        }
        currWave++;
        UpdateWaveInfo?.Invoke(Config.Waves.Length, currWave);
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
