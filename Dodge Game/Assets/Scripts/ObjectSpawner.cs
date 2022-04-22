using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

  public GameObject ObjectPrefab;

  public bool startActivated;
  public float spawnRate;
  public float randomness;

  public Vector2 TopLeftSpawnArea;
  public Vector2 BottomRightSpawnArea;

  private bool isActive;

  void Start() {
    if (startActivated) StartSpawning();
    isActive = startActivated;
  }

  public void StartSpawning() {
    if (!isActive) {
      StartCoroutine(SpawnObjects());
      isActive = true;
    }
  }

  public void StopSpawning() {
    if (isActive) {
      StopAllCoroutines();
      isActive = false;
    }
  }

  private IEnumerator SpawnObjects() {
    while (true) {
      Vector2 position = new Vector2(Random.Range(TopLeftSpawnArea.x, BottomRightSpawnArea.x), Random.Range(BottomRightSpawnArea.y, TopLeftSpawnArea.y));

      Instantiate(ObjectPrefab, position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
      yield return new WaitForSeconds(Random.Range(0, randomness) + spawnRate);
    }
  }
}
