using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour {

  public GameObject ObjectPrefab;
  
  public float spawnRate;
  public float randomness;

  public Vector2 TopLeftSpawnArea;
  public Vector2 BottomRightSpawnArea;

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

      Vector2 position = new Vector2(Random.Range(TopLeftSpawnArea.x, BottomRightSpawnArea.x), Random.Range(BottomRightSpawnArea.y, TopLeftSpawnArea.y));

      if (!isActive) yield break;
      var o = Instantiate(ObjectPrefab, position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
      o.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -100));
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
