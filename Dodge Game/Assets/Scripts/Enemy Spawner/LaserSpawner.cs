using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class LaserSpawner : MonoBehaviour
{
  public LaserEnemy LaserPrefab;

  public float spawnRate;
  public float randomness;

  private bool isActive = false;

  public bool StartActivated;

  void Start() {
    GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;

    if (StartActivated) StartSpawning();
  }

  public void StartSpawning() {
    if (!isActive) {
      StartCoroutine(SpawnObjects());
      isActive = true;
    }
  }

  public void StopSpawning() {
    if (isActive) {
      if (this != null)
        StopAllCoroutines();
      isActive = false;
    }
  }

  private IEnumerator SpawnObjects() {
    while (true) {
      yield return new WaitForSeconds(Random.Range(0, randomness) + spawnRate);

      if (!isActive) yield break;
      var laser = Instantiate(LaserPrefab);
      laser.SpawnAtRandom();
    }
  }

  private void GameManagerOnGameStateChanged(GameManager.GameState newState) {
    switch (newState) {
      case GameManager.GameState.Playing:
        break;
      case GameManager.GameState.GameOver:
        StopSpawning();
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }
  }
}
