using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Object_Spawners
{
  public abstract class ObjectSpawner : MonoBehaviour
  {
    public GameObject Prefab;
    public float SpawnRate = 0.4f;
    public float Randomness;
    public float minSpawnRate = 0.2f;
    public float difficultyIncreaseStep = 0.005f; 
    public Rect spawnArea;
    public float spawnDelay = 0;

    private Coroutine spawnCoroutine;
    private float playerDifficulty;

    public delegate IEnumerator SpawnObjectDelegate();
    public SpawnObjectDelegate SpawnObjectTest;

    protected bool shouldBeSpawning = false;

    void Start()
    {
      GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void Awake()
    {
      SpawnObjectTest = SpawnObject;

      playerDifficulty = PlayerPrefs.GetInt("playerDifficulty", 1);
      playerDifficulty = Mathf.Abs(playerDifficulty - 2);

      SpawnRate += playerDifficulty * 0.2f;
    }

    public void IncreaseDifficulty(int steps)
    {
      SpawnRate -= difficultyIncreaseStep * steps;
      if (SpawnRate < minSpawnRate) SpawnRate = minSpawnRate;
    }

    public void StartSpawning()
    {
      StopSpawning();
      shouldBeSpawning = true;
      spawnCoroutine = StartCoroutine(SpawnObjects());
    }
    public void StopSpawning()
    {
      if (spawnCoroutine != null)
      {
        this?.StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
      }
      shouldBeSpawning = false;
    }

    private IEnumerator SpawnObjects()
    {
      while (shouldBeSpawning)
      {
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnObjectTest());
        yield return new WaitForSeconds(SpawnRate);
      }
    }

    protected IEnumerator SpawnObject()
    {
      if (shouldBeSpawning)
      {
        Instantiate(Prefab, new Vector2(Random.Range(spawnArea.x, spawnArea.x + spawnArea.width), Random.Range(spawnArea.y, spawnArea.y + spawnArea.height)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
      }
      yield return new WaitForSeconds(0);
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState newState)
    {
      switch (newState)
      {
        case GameManager.GameState.Playing:
          break;
        case GameManager.GameState.GameOver:
          StopSpawning();
          break;
        default:
          Debug.LogError("Unknown GameState on GameManagerOnGameStateChanged()");
          break;
      }
    }
  }
}
