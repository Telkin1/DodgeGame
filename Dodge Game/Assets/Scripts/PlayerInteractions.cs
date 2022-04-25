using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

  public HealthBarUI healthBarUI;
  public ObjectSpawner EnemySpawner;

  private int invinciblePeriod = 0;

  void OnCollisionEnter2D(Collision2D collision) {
    switch (collision.gameObject.tag) {
      case "Enemy":
        if (invinciblePeriod == 0) {
          collision.gameObject.GetComponent<FallingObject>()?.Kill();
          healthBarUI.AddLive(-1);
          invinciblePeriod = 150;
          var sr = GetComponent<SpriteRenderer>();
          sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.7f);
        }
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

  void FixedUpdate() {
    if (invinciblePeriod <= 0) return;
    invinciblePeriod -= 1;
    if (invinciblePeriod == 0) {
      var sr = GetComponent<SpriteRenderer>();
      sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + 0.7f);
    }
  }

  void Update() {
    if (Input.GetKey(KeyCode.S)) {
      ScoreManager.instance.AddScore(1);
    }
  }
}
