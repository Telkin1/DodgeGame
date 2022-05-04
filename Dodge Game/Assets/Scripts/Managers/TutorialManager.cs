using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

  public GameObject HealthPrefab;
  public GameObject GoldPrefab;
  public GameObject EnemyPrefab;

  void Start() {
    Physics2D.gravity = new Vector2(0, 0);

    var p = GameObject.Find("Player").GetComponent<PlayerInteractions>();
    p.OnPlayerHeal += delegate { Invoke("SpawnHealthBox", 1f); };
    p.OnPlayerScoreIncrease += delegate { Invoke("SpawnGold", 1f); };
    p.OnPlayerHit += delegate { Invoke("SpawnEnemy", 1f); };

    SpawnHealthBox();
    SpawnGold();
    SpawnEnemy();
  }

  private void SpawnHealthBox() {
    var o = Instantiate(HealthPrefab, new Vector3(-4.4f, 1f), Quaternion.identity);
  }

  private void SpawnGold() {
    var o = Instantiate(GoldPrefab, new Vector3(-4.4f, -0.8f), Quaternion.identity);
  }

  private void SpawnEnemy() {
    var o = Instantiate(EnemyPrefab, new Vector3(-4.4f, -2.8f), Quaternion.identity);
  }
}
