using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

  public HealthBarUI healthBarUI;

  void OnCollisionEnter2D(Collision2D collision) {
    switch (collision.gameObject.tag) {
      case "Enemy":
        healthBarUI.AddLive(-1);
        collision.gameObject.GetComponent<FallingObject>()?.Kill();
        break;
      case "Gold":
        collision.gameObject.GetComponent<FallingObject>()?.Kill();
        ScoreManager.instance.AddScore(1);
        break;
    }

    if (collision.gameObject.name.StartsWith("HealthBox")) {
      collision.gameObject.GetComponent<FallingObject>()?.Kill();
      healthBarUI.AddLive(1);
    }
  }
}
