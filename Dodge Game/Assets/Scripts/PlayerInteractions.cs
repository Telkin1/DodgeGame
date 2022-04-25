using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

  public HealthBarUI healthBarUI;
  public ObjectSpawner EnemySpawner;

  void OnCollisionEnter2D(Collision2D collision) {
    switch (collision.gameObject.tag) {
      case "Enemy":
        healthBarUI.AddLive(-1);
        collision.gameObject.GetComponent<FallingObject>()?.Kill();
        break;
      case "Gold":
        collision.gameObject.GetComponent<FallingObject>()?.Kill();
        ScoreManager.instance.AddScore(1);
        EnemySpawner.spawnRate -= 0.006f;
        if (EnemySpawner.spawnRate < 0.02) EnemySpawner.spawnRate = 0.02f;
        break;
    }

    if (collision.gameObject.name.StartsWith("HealthBox")) {
      collision.gameObject.GetComponent<FallingObject>()?.Kill();
      healthBarUI.AddLive(1);
    }
  }
}
